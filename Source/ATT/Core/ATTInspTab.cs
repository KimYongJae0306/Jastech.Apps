using Emgu.CV;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform.Core;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
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

        public bool IsAddStart { get; set; }

        public void Dispose()
        {
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

                AddImage();

                if (TabScanBuffer.IsAddImageDone())
                {
                    TabScanBuffer.MakeMergeImage();
                    Console.WriteLine("Make Merge Image." + TabScanBuffer.TabNo);

                    Inspection();
                    TabScanBuffer.InspectionDone = true;
                }

                Thread.Sleep(50);
            }
        }

        private void AddImage()
        {
            if (GetData() is byte[] data)
            {
                IsAddStart = true;

                Mat mat = MatHelper.ByteArrayToMat(data, TabScanBuffer.SubImageWidth, TabScanBuffer.SubImageHeight, 1);
                Mat rotatedMat = MatHelper.Transpose(mat);
                TabScanBuffer.AddSubImage(rotatedMat);
                mat.Dispose();
            }

            if (GetDataCount() > 0)
                AddImage();
        }

        private void Inspection()
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            Tab tab = inspModel.GetUnit(UnitName.Unit0).GetTab(TabScanBuffer.TabNo);

            MainAlgorithmTool algorithmTool = new MainAlgorithmTool();

            TabInspResult inspResult = new TabInspResult();
            inspResult.TabNo = TabScanBuffer.TabNo;
            inspResult.Image = TabScanBuffer.MergeMatImage;
            inspResult.CogImage = TabScanBuffer.MergeCogImage;

            #region Mark 검사
            algorithmTool.MainMarkInspect(TabScanBuffer.MergeCogImage, tab, ref inspResult);

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
            inspResult.LeftAlignX = algorithmTool.RunMainLeftAlignX(TabScanBuffer.MergeCogImage, tab, fpcTheta, panelTheta, judgementX);
            if (inspResult.IsLeftAlignXGood() == false)
            {
                var leftAlignX = inspResult.LeftAlignX;
                string message = string.Format("Left AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignX.Fpc.Judgement, leftAlignX.Panel.Judgement);
                Logger.Debug(LogType.Inspection, message);
            }

            inspResult.LeftAlignY = algorithmTool.RunMainLeftAlignY(TabScanBuffer.MergeCogImage, tab, fpcTheta, panelTheta, judgementY);
            if (inspResult.IsLeftAlignYGood() == false)
            {
                var leftAlignY = inspResult.LeftAlignY;
                string message = string.Format("Left AlignY Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignY.Fpc.Judgement, leftAlignY.Panel.Judgement);
                Logger.Debug(LogType.Inspection, message);
            }
            #endregion

            #region Right Align
            inspResult.RightAlignX = algorithmTool.RunMainRightAlignX(TabScanBuffer.MergeCogImage, tab, fpcTheta, panelTheta, judgementX);
            if (inspResult.IsRightAlignXGood() == false)
            {
                var rightAlignX = inspResult.RightAlignX;
                string message = string.Format("Right AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, rightAlignX.Fpc.Judgement, rightAlignX.Panel.Judgement);
                Logger.Debug(LogType.Inspection, message);
            }

            inspResult.RightAlignY = algorithmTool.RunMainRightAlignY(TabScanBuffer.MergeCogImage, tab, fpcTheta, panelTheta, judgementY);
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



            Console.WriteLine("Inspection Completed." + TabScanBuffer.TabNo);
        }
    }
}
