using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Algorithms.Akkon;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATT.Core
{
    public class ATTInspTab
    {
        private object _lock = new object();

        public Queue<byte[]> DataQueue = new Queue<byte[]>();

        public Task InspTask { get; set; }

        public CancellationTokenSource CancelInspTask { get; set; }

        public TabScanBuffer TabScanBuffer { get; set; } = null;

        public List<Mat> SubImageList { get; set; } = new List<Mat>();

        public Mat MergeMatImage { get; set; } = null;

        public CogImage8Grey MergeCogImage { get; set; } = null;

        public bool IsAddStart { get; set; }

        public AkkonAlgorithm AkkonAlgorithm { get; set; } = new AkkonAlgorithm();

        public void Dispose()
        {
            for (int i = 0; i < SubImageList.Count(); i++)
            {
                SubImageList[i].Dispose();
                SubImageList[i] = null;
            }
            SubImageList.Clear();

            MergeMatImage?.Dispose();
            MergeMatImage = null;

            MergeCogImage?.Dispose();
            MergeCogImage = null;

            DataQueue.Clear();
            TabScanBuffer?.Dispose();
        }

        public void AddData(byte[] data)
        {
            lock (_lock)
                DataQueue.Enqueue(data);
        }

        public int GetDataCount()
        {
            lock (_lock)
                return DataQueue.Count();
        }

        public byte[] GetData()
        {
            lock(_lock)
            {
                if (DataQueue.Count > 0)
                    return DataQueue.Dequeue();
                else
                    return null;
            }
        }

        public void StartInspTask()
        {
            if (TabScanBuffer == null)
                return;

            if (InspTask != null)
                return;

            CancelInspTask = new CancellationTokenSource();
            InspTask = new Task(InspectionTask, CancelInspTask.Token);
            InspTask.Start();
        }

        public void StopInspTask()
        {
            if (InspTask == null)
                return;

            CancelInspTask.Cancel();
            InspTask.Wait();
            InspTask = null;
        }

        private void InspectionTask()
        {
            while (true)
            {
                if (CancelInspTask.IsCancellationRequested)
                {
                    break;
                }
                if (TabScanBuffer.InspectionDone)
                    Thread.Sleep(1000); // CPU 점유율 낮추려고...
                else
                {
                    AddImage();

                    if (SubImageList.Count() == TabScanBuffer.TotalGrabCount)
                    {
                        MakeMergeImage();
                        Console.WriteLine("Make Merge Image." + TabScanBuffer.TabNo);

                       Inspection();

                        TabScanBuffer.InspectionDone = true;
                    }

                    if (IsAddStart)
                        Thread.Sleep(0);
                    else
                        Thread.Sleep(50);
                }
            }
        }

        private void MakeMergeImage()
        {
            MakeMergeMatImage();
            MakeMergeCogImage();
        }

        private void AddImage()
        {
            if (TabScanBuffer.GetData() is byte[] data)
            {
                IsAddStart = true;

                Mat mat = MatHelper.ByteArrayToMat(data, TabScanBuffer.SubImageWidth, TabScanBuffer.SubImageHeight, 1);
                Mat rotatedMat = MatHelper.Transpose(mat);
                SubImageList.Add(rotatedMat);
                mat.Dispose();
            }

            if (GetDataCount() > 0)
                AddImage();
        }

        private void MakeMergeCogImage()
        {
            if (MergeCogImage == null)
            {
                MakeMergeMatImage();
                if (MergeMatImage != null)
                    MergeCogImage = ConvertCogGrayImage(MergeMatImage);
            }
        }

        private void MakeMergeMatImage()
        {
            if (MergeMatImage == null)
            {
                if (SubImageList.Count > 0)
                {
                    MergeMatImage = new Mat();
                    CvInvoke.HConcat(SubImageList.ToArray(), MergeMatImage);
                }
            }
        }

        private CogImage8Grey ConvertCogGrayImage(Mat mat)
        {
            if (mat == null)
                return null;

            int size = mat.Width * mat.Height * mat.NumberOfChannels;
            var cogImage = VisionProImageHelper.CovertImage(mat.DataPointer, mat.Width, mat.Height, ColorFormat.Gray) as CogImage8Grey;
            return cogImage;
        }

        private void Inspection()
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            Tab tab = inspModel.GetUnit(UnitName.Unit0).GetTab(TabScanBuffer.TabNo);

            MainAlgorithmTool algorithmTool = new MainAlgorithmTool();

            TabInspResult inspResult = new TabInspResult();
            inspResult.TabNo = TabScanBuffer.TabNo;
            inspResult.Image = MergeMatImage;
            inspResult.CogImage = MergeCogImage;

            #region Mark 검사
            algorithmTool.MainMarkInspect(MergeCogImage, tab, ref inspResult);

            if (inspResult.IsMarkGood() == false)
            {
                // 검사 실패
                string message = string.Format("Mark Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, inspResult.FpcMark.Judgement, inspResult.PanelMark.Judgement);
                Logger.Debug(LogType.Inspection, message);
                //return;
            }
            #endregion

            double fpcTheta = 0.0;
            double panelTheta = 0.0;

            #region 보정 값 계산
            if (inspResult.FpcMark.Judgement == Judgement.OK)
            {
                PointF point1 = inspResult.FpcMark.FoundedMark.Left.MaxMatchPos.FoundPos;
                PointF point2 = inspResult.FpcMark.FoundedMark.Right.MaxMatchPos.FoundPos;
                fpcTheta = MathHelper.GetTheta(point1, point2);
            }

            if (inspResult.PanelMark.Judgement == Judgement.OK)
            {
                PointF point1 = inspResult.PanelMark.FoundedMark.Left.MaxMatchPos.FoundPos;
                PointF point2 = inspResult.PanelMark.FoundedMark.Right.MaxMatchPos.FoundPos;
                panelTheta = MathHelper.GetTheta(point1, point2);
            }
            #endregion

            double judgementX = 100.0;
            double judgementY = 100.0;

            #region Left Align
            inspResult.LeftAlignX = algorithmTool.RunMainLeftAlignX(MergeCogImage, tab, fpcTheta, panelTheta, judgementX);
            if (inspResult.IsLeftAlignXGood() == false)
            {
                var leftAlignX = inspResult.LeftAlignX;
                string message = string.Format("Left AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignX.Fpc.Judgement, leftAlignX.Panel.Judgement);
                Logger.Debug(LogType.Inspection, message);
            }

            inspResult.LeftAlignY = algorithmTool.RunMainLeftAlignY(MergeCogImage, tab, fpcTheta, panelTheta, judgementY);
            if (inspResult.IsLeftAlignYGood() == false)
            {
                var leftAlignY = inspResult.LeftAlignY;
                string message = string.Format("Left AlignY Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignY.Fpc.Judgement, leftAlignY.Panel.Judgement);
                Logger.Debug(LogType.Inspection, message);
            }
            #endregion

            #region Right Align
            inspResult.RightAlignX = algorithmTool.RunMainRightAlignX(MergeCogImage, tab, fpcTheta, panelTheta, judgementX);
            if (inspResult.IsRightAlignXGood() == false)
            {
                var rightAlignX = inspResult.RightAlignX;
                string message = string.Format("Right AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, rightAlignX.Fpc.Judgement, rightAlignX.Panel.Judgement);
                Logger.Debug(LogType.Inspection, message);
            }

            inspResult.RightAlignY = algorithmTool.RunMainRightAlignY(MergeCogImage, tab, fpcTheta, panelTheta, judgementY);
            if (inspResult.IsRightAlignYGood() == false)
            {
                var rightAlignY = inspResult.RightAlignY;
                string message = string.Format("Right AlignY Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, rightAlignY.Fpc.Judgement, rightAlignY.Panel.Judgement);
                Logger.Debug(LogType.Inspection, message);
            }
            #endregion

            #region Center Align
            inspResult.CenterX = Math.Abs(inspResult.LeftAlignX.ResultValue - inspResult.RightAlignX.ResultValue);
            #endregion


            var roiList = tab.AkkonParam.GetAkkonROIList();
            var akkonResult = AkkonAlgorithm.Run(MergeMatImage, roiList, tab.AkkonParam.AkkonAlgoritmParam);

            //result.AkkonResultList.AddRange(akkonResult);
            //AppsInspResult.TabResultList.Add(result);

            sw.Stop();
            string resultMessage = string.Format("Inspection Completed. {0}({1}ms)", TabScanBuffer.TabNo, sw.ElapsedMilliseconds);
            Console.WriteLine(resultMessage);
        }
    }
}
