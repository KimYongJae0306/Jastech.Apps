using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Algorithms.Akkon;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Util.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace ATT.Core
{
    public partial class ATTInspRunner
    {
        #region 필드
        private Axis _axis { get; set; } = null;
        #endregion

        #region 속성
        private Task SeqTask { get; set; }

        private CancellationTokenSource SeqTaskCancellationTokenSource { get; set; }

        private SeqStep SeqStep { get; set; } = SeqStep.SEQ_IDLE;

        private Task _movingTask { get; set; } = null;

        public bool IsPanelIn { get; set; } = false;

        private bool IsGrabDone { get; set; } = false;

        private AppsInspResult AppsInspResult { get; set; } = null;

        private Stopwatch LastInspSW { get; set; } = new Stopwatch();

        public Task AkkonInspTask { get; set; }

        public CancellationTokenSource CancelAkkonInspTask { get; set; }

        public Queue<AkkonThreadParam> AkkonInspQueue = new Queue<AkkonThreadParam>();

        public MacronAkkonAlgorithmTool MacronAkkonAlgorithmTool { get; set; } = null;

        public AkkonAlgorithm AkkonAlgorithm { get; set; } = null;
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public ATTInspRunner()
        {
            if(AppsConfig.Instance().AkkonAlgorithmType == AkkonAlgorithmType.Macron)
                MacronAkkonAlgorithmTool = new MacronAkkonAlgorithmTool();
            else
                AkkonAlgorithm = new AkkonAlgorithm();
        }
        #endregion

        #region 메서드
        private void ATTSeqRunner_TabImageGrabCompletedEventHandler(string cameraName, TabScanImage tabScanImage)
        {
            Mat matImage = tabScanImage.GetMergeImage();
            ICogImage cogImage = tabScanImage.ConvertCogGrayImage(matImage);

            Console.WriteLine("Run Inspection. " + tabScanImage.TabNo.ToString());
            Task task = new Task(() => Run(tabScanImage, matImage, cogImage));
            task.Start();
        }

        public void Run(TabScanImage ScanImage, Mat mergeMat, ICogImage cogMergeImage)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            Tab tab = inspModel.GetUnit(UnitName.Unit0).GetTab(ScanImage.TabNo);

            MainAlgorithmTool algorithmTool = new MainAlgorithmTool();

            TabInspResult inspResult = new TabInspResult();
            inspResult.TabNo = tab.Index;
            inspResult.Image = mergeMat;
            inspResult.CogImage = cogMergeImage;

            #region Mark 검사
            algorithmTool.MainMarkInspect(cogMergeImage, tab, ref inspResult);

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
            inspResult.LeftAlignX = algorithmTool.RunMainLeftAlignX(cogMergeImage, tab, fpcTheta, panelTheta, judgementX);
            if (inspResult.IsLeftAlignXGood() == false)
            {
                var leftAlignX = inspResult.LeftAlignX;
                string message = string.Format("Left AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignX.Fpc.Judgement, leftAlignX.Panel.Judgement);
                Logger.Debug(LogType.Inspection, message);
            }

            inspResult.LeftAlignY = algorithmTool.RunMainLeftAlignY(cogMergeImage, tab, fpcTheta, panelTheta, judgementY);
            if (inspResult.IsLeftAlignYGood() == false)
            {
                var leftAlignY = inspResult.LeftAlignY;
                string message = string.Format("Left AlignY Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignY.Fpc.Judgement, leftAlignY.Panel.Judgement);
                Logger.Debug(LogType.Inspection, message);
            }
            #endregion

            #region Right Align
            inspResult.RightAlignX = algorithmTool.RunMainRightAlignX(cogMergeImage, tab, fpcTheta, panelTheta, judgementX);
            if (inspResult.IsRightAlignXGood() == false)
            {
                var rightAlignX = inspResult.RightAlignX;
                string message = string.Format("Right AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, rightAlignX.Fpc.Judgement, rightAlignX.Panel.Judgement);
                Logger.Debug(LogType.Inspection, message);
            }

            inspResult.RightAlignY = algorithmTool.RunMainRightAlignY(cogMergeImage, tab, fpcTheta, panelTheta, judgementY);
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

            AkkonThreadParam param = new AkkonThreadParam();
            param.Tab = tab;
            param.TabInspResult = inspResult;
            AkkonInspQueue.Enqueue(param);
        }

        private void ATTSeqRunner_GrabDoneEventHanlder(string cameraName, bool isGrabDone)
        {
            IsGrabDone = isGrabDone; 
        }

        private void AkkonInspection()
        {
            while(true)
            {
                if (CancelAkkonInspTask.IsCancellationRequested)
                {
                    break;
                }

                if(AkkonInspQueue.Count > 0)
                {
                    var akkon = AkkonInspQueue.Dequeue();

                    var result = akkon.TabInspResult;
                    Mat image = akkon.TabInspResult.Image;
                    Tab tab = akkon.Tab;

                    if(AppsConfig.Instance().AkkonAlgorithmType == AkkonAlgorithmType.Macron)
                    {
                        var akkonResult = MacronAkkonAlgorithmTool.RunMultiAkkon(image, tab.StageIndex, tab.Index);
                        result.MacronAkkonResult = akkonResult;
                        AppsInspResult.TabResultList.Add(result);
                    }
                    else
                    {
                        var roiList = tab.AkkonParam.GetAkkonROIList();
                        var akkonResult = AkkonAlgorithm.Run(image, roiList, tab.AkkonParam.AkkonAlgoritmParam);
                        result.AkkonResultList.AddRange(akkonResult);
                        AppsInspResult.TabResultList.Add(result);
                    }

                    //AppsInspResult.TabResultList.Add(new TabInspResult());

                    Console.WriteLine("Add Akkon Result");
                }
                Thread.Sleep(0);
            }
        }

        public void StartAkkonInspTask()
        {
            if (AkkonInspTask != null)
                return;

            CancelAkkonInspTask = new CancellationTokenSource();
            AkkonInspTask = new Task(AkkonInspection, CancelAkkonInspTask.Token);
            AkkonInspTask.Start();
        }

        public void StopAkkonInspTask()
        {
            if (AkkonInspTask == null)
                return;

            CancelAkkonInspTask.Cancel();
            AkkonInspTask.Wait();
            AkkonInspTask = null;
        }

        public void ClearResult()
        {
            if (AppsInspResult == null)
                AppsInspResult = new AppsInspResult();

            if (AppsInspResult != null)
                AppsInspResult.Dispose();

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            SystemManager.Instance().InitializeResult(inspModel.TabCount);
        }

        public bool IsInspectionDone()
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (AppsConfig.Instance().Operation.VirtualMode)
            {
                RunVirtual();
                return true;
            }
            lock (AppsInspResult)
            {
                if (AppsInspResult.TabResultList.Count() == inspModel.TabCount)
                    return true;
            }
            return false;
        }

        public void SeqRun()
        {
            if (ModelManager.Instance().CurrentModel == null)
                return;

            var appsLineCamera = AppsLineCameraManager.Instance().GetAppsCamera(CameraName.LinscanMIL0.ToString());
            appsLineCamera.TabImageGrabCompletedEventHandler += ATTSeqRunner_TabImageGrabCompletedEventHandler;
            appsLineCamera.GrabDoneEventHanlder += ATTSeqRunner_GrabDoneEventHanlder;
            AppsLineCameraManager.Instance().GetLineCamera(CameraName.LinscanMIL0).StartMainGrabTask();
            AppsLineCameraManager.Instance().GetLineCamera(CameraName.LinscanMIL0).StartMergeTask();
            StartAkkonInspTask();


            Logger.Write(LogType.Seq, "Start Sequence.");

            if (SeqTask != null)
            {
                SeqStep = SeqStep.SEQ_START;
                return;
            }

            SeqTaskCancellationTokenSource = new CancellationTokenSource();
            SeqTask = new Task(SeqTaskAction, SeqTaskCancellationTokenSource.Token);
            SeqTask.Start();
        }

        public void SeqStop()
        {
            var appsLineCamera = AppsLineCameraManager.Instance().GetAppsCamera(CameraName.LinscanMIL0.ToString());
            appsLineCamera.TabImageGrabCompletedEventHandler -= ATTSeqRunner_TabImageGrabCompletedEventHandler;
            appsLineCamera.GrabDoneEventHanlder -= ATTSeqRunner_GrabDoneEventHanlder;
            AppsLineCameraManager.Instance().GetLineCamera(CameraName.LinscanMIL0).StopMainGrabTask();
            AppsLineCameraManager.Instance().GetLineCamera(CameraName.LinscanMIL0).StopGrab();
            StopAkkonInspTask();
            Logger.Write(LogType.Seq, "Stop Sequence.");

            if (SeqTask == null)
                return;

            // 조명 off
            AppsLAFManager.Instance().AutoFocusOnOff(LAFName.Akkon.ToString(), false);
            Logger.Write(LogType.Seq, "AutoFocus Off.");

            AppsLineCameraManager.Instance().Stop(CameraName.LinscanMIL0);
            Logger.Write(LogType.Seq, "Stop Grab.");

        }

        private void SeqTaskAction()
        {
            var cancellationToken = SeqTaskCancellationTokenSource.Token;
            cancellationToken.ThrowIfCancellationRequested();
            SeqStep = SeqStep.SEQ_START;

            while (true)
            {
                // 작업 취소
                if (cancellationToken.IsCancellationRequested)
                {
                    SeqStep = SeqStep.SEQ_IDLE;
                    //조명 Off
                    AppsLineCameraManager.Instance().Stop(CameraName.LinscanMIL0);
                    break;
                }
                SeqTaskLoop();
                Thread.Sleep(50);
            }
        }

        private void SeqTaskLoop()
        {
            //ICogImage cogImage = Jastech.Framework.Imaging.VisionPro.CogImageHelper.Load(@"D:\Tab1.bmp");

         
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (inspModel == null)
                return;

            var unit = inspModel.GetUnit(UnitName.Unit0);
            if (unit == null)
                return;

            var tab = unit.GetTab(0);
            if (tab == null)
                return;

            string message = string.Empty;

            switch (SeqStep)
            {
                case SeqStep.SEQ_IDLE:

                    SeqStep = SeqStep.SEQ_START;
                    break;

                case SeqStep.SEQ_START:

                    // break;
                    SeqStep = SeqStep.SEQ_READY;
                    break;

                case SeqStep.SEQ_READY:

                    if (MoveTo(TeachingPosType.Stage1_Scan_Start, out string error1) == false)
                    {
                        // Alarm
                        break;
                    }
                    SeqStep = SeqStep.SEQ_WAITING;
                    break;

                case SeqStep.SEQ_WAITING:
                    //if (IsPanelIn == false)
                    //    break;
                    SeqStep = SeqStep.SEQ_SCAN_READY;
                    break;

                case SeqStep.SEQ_SCAN_READY:

                    ClearResult();
                    Logger.Write(LogType.Seq, "Clear Result.");

                    InitializeBuffer();
                    Logger.Write(LogType.Seq, "Initialize Buffer.");

                    AppsInspResult.StartInspTime = DateTime.Now;
                    AppsInspResult.Cell_ID = DateTime.Now.ToString("yyyyMMddHHmmss");

                    AppsLAFManager.Instance().AutoFocusOnOff(LAFName.Akkon.ToString(), true);
                    Logger.Write(LogType.Seq, "AutoFocus On.");

                    SeqStep = SeqStep.SEQ_SCAN_START;
                    break;

                case SeqStep.SEQ_SCAN_START:

                    IsGrabDone = false;
                    // 조명 코드 작성 요망
                    var appsLineCamera = AppsLineCameraManager.Instance().GetLineCamera(CameraName.LinscanMIL0);
                    appsLineCamera.SetOperationMode(TDIOperationMode.TDI);
                    appsLineCamera.StartGrab();
                    Logger.Write(LogType.Seq, "Start Grab.");

                    if (MoveTo(TeachingPosType.Stage1_Scan_End, out string error2) == false)
                    {
                        // Alarm
                        // 조명 Off
                        AppsLineCameraManager.Instance().Stop(CameraName.LinscanMIL0);
                        Logger.Write(LogType.Seq, "Stop Grab.");
                        break;
                    }

                    SeqStep = SeqStep.SEQ_WAITING_SCAN_COMPLETED;
                    break;

                case SeqStep.SEQ_WAITING_SCAN_COMPLETED:
                    if (AppsConfig.Instance().Operation.VirtualMode == false)
                    {
                        if (IsGrabDone == false)
                            break;
                    }

                    Logger.Write(LogType.Seq, "Scan Grab Completed.");

                    //AppsLAFManager.Instance().AutoFocusOnOff(LAFName.Akkon.ToString(), false);
                    //Logger.Write(LogType.Seq, "AutoFocus Off.");

                    AppsLineCameraManager.Instance().Stop(CameraName.LinscanMIL0);
                    Logger.Write(LogType.Seq, "Stop Grab.");

                    LastInspSW.Restart();


                    SeqStep = SeqStep.SEQ_WAITING_INSPECTION_DONE;
                    break;

                case SeqStep.SEQ_WAITING_INSPECTION_DONE:
                    if (IsInspectionDone() == false)
                        break;

                    LastInspSW.Stop();
                    AppsInspResult.EndInspTime = DateTime.Now;
                    AppsInspResult.LastInspTime = LastInspSW.ElapsedMilliseconds.ToString();
                    Console.WriteLine("Total Tact Time : " + LastInspSW.ElapsedMilliseconds.ToString());
                    SeqStep = SeqStep.SEQ_UI_RESULT_UPDATE;
                    break;

                case SeqStep.SEQ_UI_RESULT_UPDATE:
                    GetAkkonResultImage();
                    SystemManager.Instance().UpdateMainResult(AppsInspResult);
                    Console.WriteLine("Scan End to Insp Complete : " + LastInspSW.ElapsedMilliseconds.ToString());
                    SeqStep = SeqStep.SEQ_SAVE_RESULT_DATA;
                    break;

                case SeqStep.SEQ_SAVE_RESULT_DATA:
                    SaveInspectionResult(AppsInspResult);
                    SeqStep = SeqStep.SEQ_SAVE_IMAGE;
                    break;

                case SeqStep.SEQ_SAVE_IMAGE:

                    SaveImage(AppsInspResult);

                    SeqStep = SeqStep.SEQ_DELETE_DATA;
                    break;

                case SeqStep.SEQ_DELETE_DATA:

                    Thread.Sleep(1000);
                    SeqStep = SeqStep.SEQ_IDLE;
                    break;

                case SeqStep.SEQ_CHECK_STANDBY:

                    //if (!AppsMotionManager.Instance().IsMotionInPosition(UnitName.Unit0, AxisHandlerName.Handler0, AxisName.X, TeachingPosType.Standby))
                    //    break;

                    SeqStep = SeqStep.SEQ_IDLE;
                    break;
                default:
                    break;
            }
        }

        private void GetAkkonResultImage()
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();

            if(AppsConfig.Instance().AkkonAlgorithmType == AkkonAlgorithmType.Macron)
            {
                AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
                var unit = inspModel.GetUnit(UnitName.Unit0);
                for (int i = 0; i < AppsInspResult.TabResultList.Count(); i++)
                {
                    var tabResult = AppsInspResult.TabResultList[i];
                    Tab tab = unit.GetTab(tabResult.TabNo);
                    tabResult.AkkonResultImage = MacronAkkonAlgorithmTool.GetResultImage(tabResult.Image.Width, tabResult.Image.Height, tab, AppsConfig.Instance().AkkonResizeRatio);
                }
            }
            else
            {
                AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
                var unit = inspModel.GetUnit(UnitName.Unit0);
                for (int i = 0; i < AppsInspResult.TabResultList.Count(); i++)
                {
                    var tabResult = AppsInspResult.TabResultList[i];
                    Tab tab = unit.GetTab(tabResult.TabNo);
                   
                    Mat resultMat = GetResultImage(tabResult.Image, tabResult.AkkonResultList, tab.AkkonParam.AkkonAlgoritmParam);
                    ICogImage cogImage = ConvertCogColorImage(resultMat);
                    tabResult.AkkonResultImage = cogImage;
                    resultMat.Dispose();
                }
            }
            sw.Stop();
            Console.WriteLine("Get Akkon Result Image : " + sw.ElapsedMilliseconds.ToString() + "ms");
        }

        public ICogImage ConvertCogColorImage(Mat mat)
        {
            Mat matR = MatHelper.ColorChannelSprate(mat, MatHelper.ColorChannel.R);
            Mat matG = MatHelper.ColorChannelSprate(mat, MatHelper.ColorChannel.G);
            Mat matB = MatHelper.ColorChannelSprate(mat, MatHelper.ColorChannel.B);

            byte[] dataR = new byte[matR.Width * matR.Height];
            Marshal.Copy(matR.DataPointer, dataR, 0, matR.Width * matR.Height);

            byte[] dataG = new byte[matG.Width * matG.Height];
            Marshal.Copy(matG.DataPointer, dataG, 0, matG.Width * matG.Height);

            byte[] dataB = new byte[matB.Width * matB.Height];
            Marshal.Copy(matB.DataPointer, dataB, 0, matB.Width * matB.Height);

            var cogImage = VisionProImageHelper.CovertImage(dataR, dataG, dataB, matB.Width, matB.Height);

            matR.Dispose();
            matG.Dispose();
            matB.Dispose();

            return cogImage;
        }

        public Mat GetResultImage(Mat mat, List<AkkonBlob> resultList, AkkonAlgoritmParam AkkonParameters)
        {
            if (mat == null)
                return null;

            Mat resizeMat = new Mat();
            Size newSize = new Size((int)(mat.Width * AkkonParameters.ResizeRatio), (int)(mat.Height * AkkonParameters.ResizeRatio));
            CvInvoke.Resize(mat, resizeMat, newSize);
            Mat colorMat = new Mat();
            CvInvoke.CvtColor(resizeMat, colorMat, ColorConversion.Gray2Bgr);
            resizeMat.Dispose();

            foreach (var result in resultList)
            {
                var lead = result.Lead;
                var startPoint = new Point((int)result.OffsetToWorldX, (int)result.OffsetToWorldY);

                Point leftTop = new Point((int)lead.LeftTopX + startPoint.X, (int)lead.LeftTopY + startPoint.Y);
                Point leftBottom = new Point((int)lead.LeftBottomX + startPoint.X, (int)lead.LeftBottomY + startPoint.Y);
                Point rightTop = new Point((int)lead.RightTopX + startPoint.X, (int)lead.RightTopY + startPoint.Y);
                Point rightBottom = new Point((int)lead.RightBottomX + startPoint.X, (int)lead.RightBottomY + startPoint.Y);

                if (AkkonParameters.DrawOption.ContainLeadROI)
                {
                    CvInvoke.Line(colorMat, leftTop, leftBottom, new MCvScalar(50, 230, 50, 255), 1);
                    CvInvoke.Line(colorMat, leftTop, rightTop, new MCvScalar(50, 230, 50, 255), 1);
                    CvInvoke.Line(colorMat, rightTop, rightBottom, new MCvScalar(50, 230, 50, 255), 1);
                    CvInvoke.Line(colorMat, rightBottom, leftBottom, new MCvScalar(50, 230, 50, 255), 1);
                }

                int blobCount = 0;
                foreach (var blob in result.BlobList)
                {
                    Rectangle rectRect = new Rectangle();
                    rectRect.X = (int)(blob.BoundingRect.X + result.OffsetToWorldX + result.LeadOffsetX);
                    rectRect.Y = (int)(blob.BoundingRect.Y + result.OffsetToWorldY + result.LeadOffsetY);
                    rectRect.Width = blob.BoundingRect.Width;
                    rectRect.Height = blob.BoundingRect.Height;

                    Point center = new Point(rectRect.X + (rectRect.Width / 2), rectRect.Y + (rectRect.Height / 2));
                    int radius = rectRect.Width > rectRect.Height ? rectRect.Width : rectRect.Height;

                    int size = blob.BoundingRect.Width * blob.BoundingRect.Height;
                    if (AkkonParameters.ResultFilter.MinSize <= size && size <= AkkonParameters.ResultFilter.MaxSize)
                    {
                        blobCount++;
                        CvInvoke.Circle(colorMat, center, radius / 2, new MCvScalar(255), 1);
                    }
                    else
                    {
                        if (AkkonParameters.DrawOption.ContainNG)
                            CvInvoke.Circle(colorMat, center, radius / 2, new MCvScalar(0), 1);
                    }

                }

                if (AkkonParameters.DrawOption.ContainLeadCount)
                {
                    string leadIndexString = result.LeadIndex.ToString();
                    string blobCountString = string.Format("[{0}]", blobCount);

                    Point centerPt = new Point((int)((leftBottom.X + rightBottom.X) / 2.0), leftBottom.Y);

                    int baseLine = 0;
                    Size textSize = CvInvoke.GetTextSize(leadIndexString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                    int textX = centerPt.X - (textSize.Width / 2);
                    int textY = centerPt.Y + (baseLine / 2);
                    CvInvoke.PutText(colorMat, leadIndexString, new Point(textX, textY + 30), FontFace.HersheyComplex, 0.3, new MCvScalar(50, 230, 50, 255));

                    textSize = CvInvoke.GetTextSize(blobCountString, FontFace.HersheyComplex, 0.3, 1, ref baseLine);
                    textX = centerPt.X - (textSize.Width / 2);
                    textY = centerPt.Y + (baseLine / 2);
                    CvInvoke.PutText(colorMat, blobCountString, new Point(textX, textY + 60), FontFace.HersheyComplex, 0.3, new MCvScalar(50, 230, 50, 255));
                }
            }
            return colorMat;
        }

        private void InitializeBuffer()
        {
            var appsLineCamera = AppsLineCameraManager.Instance().GetLineCamera(CameraName.LinscanMIL0);
            appsLineCamera.InitGrabSettings();

            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (AppsConfig.Instance().AkkonAlgorithmType == AkkonAlgorithmType.Macron)
                MacronAkkonAlgorithmTool.PrepareMultiInspection(inspModel, appsLineCamera.TabScanImageList, AppsConfig.Instance().AkkonResizeRatio);
        }

        public void RunVirtual()
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            Tab tab = inspModel.GetUnit(UnitName.Unit0).GetTab(0);

            Mat tabMatImage = new Mat(@"D:\Tab1.bmp", Emgu.CV.CvEnum.ImreadModes.Grayscale);

            // ICogImage tabCogImage = ConvertCogImage(tabMatImage);
            // MainAlgorithmTool tool = new MainAlgorithmTool();

            //var result = tool.MainRunInspect(tab, tabMatImage, 30.0f, 80.0f);

            // AppsInspResult.TabResultList.Add(result);
        }
        #endregion
    }

    public partial class ATTInspRunner
    {
        #region 메서드
        private void SaveImage(AppsInspResult inspResult)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            DateTime currentTime = inspResult.StartInspTime;

            string month = currentTime.ToString("MM");
            string day = currentTime.ToString("dd");
            string folderPath = inspResult.Cell_ID;

            string path = Path.Combine(AppsConfig.Instance().Path.Result, inspModel.Name, month, day, folderPath);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            SaveOrgImage(path, inspResult.TabResultList);
        }

        private void SaveOrgImage(string resultPath, List<TabInspResult> insTabResultList)
        {
            if (AppsConfig.Instance().Operation.VirtualMode)
                return;

            string path = Path.Combine(resultPath, "Orgin");
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            foreach (var result in insTabResultList)
            {
                string imageName = "Tab_" + result.TabNo.ToString() + ".bmp";
                string imagePath = Path.Combine(path, imageName);
                result.Image.Save(imagePath);
            }
        }

        private void SaveInspectionResult(AppsInspResult inspResult)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            DateTime currentTime = inspResult.StartInspTime;

            string month = currentTime.ToString("MM");
            string day = currentTime.ToString("dd");
            string folderPath = inspResult.Cell_ID;

            string path = Path.Combine(AppsConfig.Instance().Path.Result, inspModel.Name, month, day);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            SaveAlignResult(path, inspResult);
            SaveAkkonResult(path, inspResult);
            SaveUPHResult(path, inspResult);
        }

        private void SaveAlignResult(string resultPath, AppsInspResult inspResult)
        {
            string filename = string.Format("Align.csv");
            string csvFile = Path.Combine(resultPath, filename);
            if (File.Exists(csvFile) == false)
            {
                List<string> header = new List<string>
                {
                    "Inspection Time",
                    "Panel ID",
                };

                for (int tabNo = 0; tabNo < inspResult.TabResultList.Count; tabNo++)
                {
                    header.Add("Tab");
                    header.Add("Judge");
                    header.Add("Lx");
                    header.Add("Ly");
                    header.Add("Cx");
                    header.Add("Rx");
                    header.Add("Ry");
                }

                CSVHelper.WriteHeader(csvFile, header);
            }

            List<string> dataList = new List<string>
            {
                inspResult.EndInspTime.ToString("HH:mm:ss"),
                inspResult.Cell_ID.ToString()
            };

            foreach (var tabResult in inspResult.TabResultList)
            {
                int tabNo = tabResult.TabNo;
                var judge = tabResult.AlignJudgment;
                float lx = tabResult.LeftAlignX.ResultValue;
                float ly = tabResult.LeftAlignY.ResultValue;
                float rx = tabResult.RightAlignX.ResultValue;
                float ry = tabResult.RightAlignY.ResultValue;
                float cx = (lx + rx) / 2.0f;

                dataList.Add(tabNo.ToString());
                dataList.Add(judge.ToString());
                dataList.Add(lx.ToString("F3"));
                dataList.Add(ly.ToString("F3"));
                dataList.Add(cx.ToString("F3"));
                dataList.Add(rx.ToString("F3"));
                dataList.Add(ry.ToString("F3"));
            }

            CSVHelper.WriteData(csvFile, dataList);
        }

        private void SaveAkkonResult(string resultPath, AppsInspResult inspResult)
        {
            string filename = string.Format("Akkon.csv");
            string csvFile = Path.Combine(resultPath, filename);
            if (File.Exists(csvFile) == false)
            {
                List<string> header = new List<string>
                {
                    "Inspection Time",
                    "Panel ID",
                };

                for (int tabNo = 0; tabNo < inspResult.TabResultList.Count; tabNo++)
                {
                    header.Add("Tab");
                    header.Add("Judge");
                    header.Add("Count");
                    header.Add("Length");
                    header.Add("Strength");
                    header.Add("STD");
                }

                CSVHelper.WriteHeader(csvFile, header);
            }

            List<string> dataList = new List<string>
            {
                inspResult.EndInspTime.ToString("HH:mm:ss"),
                inspResult.Cell_ID.ToString()
            };

            foreach (var tabResult in inspResult.TabResultList)
            {
                if(AppsConfig.Instance().AkkonAlgorithmType == AkkonAlgorithmType.Macron)
                {
                    int tabNo = tabResult.TabNo;
                    var judge = tabResult.MacronAkkonResult.Judgement;
                    int count = tabResult.MacronAkkonResult.AvgBlobCount;
                    float length = tabResult.MacronAkkonResult.AvgLength;
                    float strength = tabResult.MacronAkkonResult.AvgStrength;
                    float std = tabResult.MacronAkkonResult.AvgStd;
                    dataList.Add(tabNo.ToString());
                    dataList.Add(judge.ToString());
                    dataList.Add(count.ToString());
                    dataList.Add(length.ToString("F3"));
                    dataList.Add(strength.ToString("F3"));
                    dataList.Add(std.ToString("F3"));
                }
                else
                {
                    int tabNo = tabResult.TabNo;
                    var judge = "OK";
                    int count = 10;
                    float length = 0.0f;
                    float strength = 0.0f;
                    float std = 0.0f;

                    dataList.Add(tabNo.ToString());
                    dataList.Add(judge.ToString());
                    dataList.Add(count.ToString());
                    dataList.Add(length.ToString("F3"));
                    dataList.Add(strength.ToString("F3"));
                    dataList.Add(std.ToString("F3"));
                }
            }

            CSVHelper.WriteData(csvFile, dataList);
        }

        private void SaveUPHResult(string resultPath, AppsInspResult inspResult)
        {
            string filename = string.Format("UPH.csv");
            string csvFile = Path.Combine(resultPath, filename);
            if (File.Exists(csvFile) == false)
            {
                List<string> header = new List<string>
                {
                    "Inspection Time",
                    "Panel ID",
                    "Tab No.",

                    "Count Avg",
                    "Length Avg",
                    "Strength Avg",
                    "Std Avg",

                    "Left Align X",
                    "Left Align Y",
                    "Center Align X",
                    "Right Align X",
                    "Right Align Y",
                };

                CSVHelper.WriteHeader(csvFile, header);
            }

            List<List<string>> dataList = new List<List<string>>();
            for (int tabNo = 0; tabNo < inspResult.TabResultList.Count; tabNo++)
            {
                List<string> tabData = new List<string>
                {
                    inspResult.LastInspTime,
                    inspResult.Cell_ID,
                    tabNo.ToString(),

                    //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgBlobCount.ToString(),
                    //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgLength.ToString("F3"),
                    //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgStrength.ToString("F3"),
                    //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgStd.ToString("F3"),
                    10.ToString(),
                    0.ToString("F3"),
                    0.ToString("F3"),
                    0.ToString("F3"),

                    inspResult.TabResultList[tabNo].LeftAlignX.ResultValue.ToString("F3"),
                    inspResult.TabResultList[tabNo].LeftAlignY.ResultValue.ToString("F3"),
                    inspResult.TabResultList[tabNo].CenterX.ToString("F3"),
                    inspResult.TabResultList[tabNo].RightAlignX.ResultValue.ToString("F3"),
                    inspResult.TabResultList[tabNo].RightAlignY.ResultValue.ToString("F3")
                };

                dataList.Add(tabData);
            }

            CSVHelper.WriteData(csvFile, dataList);
        }

        private string GetExtensionOKImage()
        {
            return "." + AppsConfig.Instance().Operation.ExtensionOKImage;
        }

        private string GetExtensionNGImage()
        {
            return "." + AppsConfig.Instance().Operation.ExtensionNGImage;
        }

        private Axis GetAxis(AxisHandlerName axisHandlerName, AxisName axisName)
        {
            return AppsMotionManager.Instance().GetAxis(axisHandlerName, axisName);
        }

        public bool IsAxisInPosition(UnitName unitName, TeachingPosType teachingPos, Axis axis)
        {
            return AppsMotionManager.Instance().IsAxisInPosition(unitName, teachingPos, axis);
        }

        public bool MoveTo(TeachingPosType teachingPos, out string error)
        {
            error = "";

            if (AppsConfig.Instance().Operation.VirtualMode)
                return true;

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            AppsMotionManager manager = AppsMotionManager.Instance();

            var teachingInfo = inspModel.GetUnit(UnitName.Unit0).GetTeachingInfo(teachingPos);

            Axis axisX = GetAxis(AxisHandlerName.Handler0, AxisName.X);
            Axis axisY = GetAxis(AxisHandlerName.Handler0, AxisName.Y);
            //Axis axisZ = GetAxis(AxisHandlerName.Handler0, AxisName.Z);

            var movingParamX = teachingInfo.GetMovingParam(AxisName.X.ToString());
            var movingParamY = teachingInfo.GetMovingParam(AxisName.Y.ToString());
            var movingParamZ = teachingInfo.GetMovingParam(AxisName.Z.ToString());

            //if (MoveAxis(teachingPos, axisZ, movingParamZ) == false)
            //{
            //    error = string.Format("Move To Axis Z TimeOut!({0})", movingParamZ.MovingTimeOut.ToString());
            //    Logger.Write(LogType.Seq, error);
            //    return false;
            //}
            if (MoveAxis(teachingPos, axisX, movingParamX) == false)
            {
                error = string.Format("Move To Axis X TimeOut!({0})", movingParamX.MovingTimeOut.ToString());
                Logger.Write(LogType.Seq, error);
                return false;
            }
            if (MoveAxis(teachingPos, axisY, movingParamY) == false)
            {
                error = string.Format("Move To Axis Y TimeOut!({0})", movingParamY.MovingTimeOut.ToString());
                Logger.Write(LogType.Seq, error);
                return false;
            }

            string message = string.Format("Move Completed.(Teaching Pos : {0})", teachingPos.ToString());
            Logger.Write(LogType.Seq, message);

            return true;
        }

        private bool MoveAxis(TeachingPosType teachingPos, Axis axis, AxisMovingParam movingParam)
        {
            AppsMotionManager manager = AppsMotionManager.Instance();
            if (manager.IsAxisInPosition(UnitName.Unit0, teachingPos, axis) == false)
            {
                Stopwatch sw = new Stopwatch();
                sw.Restart();

                manager.MoveTo(UnitName.Unit0, teachingPos, axis);

                while (manager.IsAxisInPosition(UnitName.Unit0, teachingPos, axis) == false)
                {

                    if (sw.ElapsedMilliseconds >= movingParam.MovingTimeOut)
                    {
                        return false;
                    }
                    Thread.Sleep(10);
                }
            }

            return true;
        }
        #endregion
    }

    public enum SeqStep
    {
        SEQ_IDLE,
        SEQ_START,
        SEQ_READY,
        SEQ_WAITING,
        SEQ_SCAN_READY,
        SEQ_SCAN_START,
        SEQ_WAITING_SCAN_COMPLETED,
        SEQ_WAITING_INSPECTION_DONE,
        SEQ_PATTERN_MATCH,
        SEQ_ALIGN_INSPECTION,
        SEQ_ALIGN_INSPECTION_COMPLETED,
        SEQ_AKKON_INSPECTION,
        SEQ_AKKON_INSPECTION_COMPLETED,
        SEQ_UI_RESULT_UPDATE,
        SEQ_SAVE_RESULT_DATA,
        SEQ_SAVE_IMAGE,
        SEQ_DELETE_DATA,
        SEQ_CHECK_STANDBY,
    }

    public class AkkonThreadParam
    {
        public TabInspResult TabInspResult { get; set; } = null;

        public Tab Tab { get; set; } = null;
    }
}
