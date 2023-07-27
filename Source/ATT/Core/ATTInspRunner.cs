using Cognex.VisionPro;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Service;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Algorithms.Akkon;
using Jastech.Framework.Algorithms.Akkon.Parameters;
using Jastech.Framework.Algorithms.Akkon.Results;
using Jastech.Framework.Config;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging;
using Jastech.Framework.Imaging.Helper;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Util;
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

        private object _akkonLock = new object();

        private object _inspLock = new object();
        #endregion

        #region 속성
        private Task SeqTask { get; set; }

        private CancellationTokenSource SeqTaskCancellationTokenSource { get; set; }

        private SeqStep SeqStep { get; set; } = SeqStep.SEQ_IDLE;

        public bool IsPanelIn { get; set; } = false;

        private bool IsGrabDone { get; set; } = false;

        private AppsInspResult AppsInspResult { get; set; } = null;

        private Stopwatch LastInspSW { get; set; } = new Stopwatch();

        public Task AkkonInspTask { get; set; }

        public CancellationTokenSource CancelAkkonInspTask { get; set; }

        public Queue<AkkonThreadParam> AkkonInspQueue = new Queue<AkkonThreadParam>();

       public Queue<ATTInspTab> InspTabQueue = new Queue<ATTInspTab>();

        public AkkonAlgorithm AkkonAlgorithm { get; set; } = new AkkonAlgorithm();

        public List<ATTInspTab> InspTabList { get; set; } = new List<ATTInspTab>();
        #endregion

        #region 이벤트
        #endregion

        #region 델리게이트
        #endregion

        #region 생성자
        public ATTInspRunner()
        {
            //if(AppsConfig.Instance().AkkonAlgorithmType == AkkonAlgorithmType.Macron)
            //    MacronAkkonAlgorithmTool = new MacronAkkonAlgorithmTool();
            //else
            //    AkkonAlgorithm = new AkkonAlgorithm();
        }
        #endregion

        #region 메서드

        private void Run(ATTInspTab inspTab)
        {
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            string unitName = UnitName.Unit0.ToString();

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            Tab tab = inspModel.GetUnit(unitName).GetTab(inspTab.TabScanBuffer.TabNo);

            MainAlgorithmTool algorithmTool = new MainAlgorithmTool();

            TabInspResult inspResult = new TabInspResult();
            inspResult.TabNo = inspTab.TabScanBuffer.TabNo;
            inspResult.Image = inspTab.MergeMatImage;
            inspResult.CogImage = inspTab.MergeCogImage;

            // Create Coordinate Object
            CoordinateTransform fpcCoordinate = new CoordinateTransform();
            CoordinateTransform panelCoordinate = new CoordinateTransform();

            #region Mark 검사
            algorithmTool.MainMarkInspect(inspTab.MergeCogImage, tab, ref inspResult, false);

            if (inspResult.MarkResult.Judgement != Judgement.OK)
            {
                // 검사 실패
                string message = string.Format("Mark Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, inspResult.MarkResult.FpcMark.Judgement, inspResult.MarkResult.PanelMark.Judgement);
                Logger.Debug(LogType.Inspection, message);
                //return;
            }
            else
            {
                #region Add mark data
                // Set Coordinage Params
                SetCoordinateData(fpcCoordinate, inspResult);
                SetCoordinateData(panelCoordinate, inspResult);

                // Excuete Coordinate
                fpcCoordinate.ExecuteCoordinate();
                panelCoordinate.ExecuteCoordinate();
                #endregion
            }
            #endregion

            var camera = LineCameraManager.Instance().GetLineCamera("Camera0").Camera;
            float resolution_um = camera.PixelResolution_um / camera.LensScale;
            float judgementX = resolution_um * tab.AlignSpec.LeftSpecX_um;
            float judgementY = resolution_um* tab.AlignSpec.LeftSpecY_um;

            #region Left Align
            if(AppsConfig.Instance().EnableAlign)
            {
                inspResult.AlignResult.LeftX = algorithmTool.RunMainLeftAlignX(inspTab.MergeCogImage, tab, fpcCoordinate, panelCoordinate, judgementX);
                if (inspResult.AlignResult.LeftX?.Judgement != Judgement.OK)
                {
                    var leftAlignX = inspResult.AlignResult.LeftX;
                    string message = string.Format("Left AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignX.Fpc.Judgement, leftAlignX.Panel.Judgement);
                    Logger.Debug(LogType.Inspection, message);
                }

                inspResult.AlignResult.LeftY = algorithmTool.RunMainLeftAlignY(inspTab.MergeCogImage, tab, fpcCoordinate, panelCoordinate, judgementY);
                if (inspResult.AlignResult.LeftY?.Judgement != Judgement.OK)
                {
                    var leftAlignY = inspResult.AlignResult.LeftY;
                    string message = string.Format("Left AlignY Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, leftAlignY.Fpc.Judgement, leftAlignY.Panel.Judgement);
                    Logger.Debug(LogType.Inspection, message);
                }
            }
            else
            {
                inspResult.AlignResult.LeftX = new AlignResult();
                inspResult.AlignResult.LeftY = new AlignResult();
            }
            #endregion

            #region Right Align
            if (AppsConfig.Instance().EnableAlign)
            {
                inspResult.AlignResult.RightX = algorithmTool.RunMainRightAlignX(inspTab.MergeCogImage, tab, fpcCoordinate, panelCoordinate, judgementX);
                if (inspResult.AlignResult.RightX?.Judgement != Judgement.OK)
                {
                    var rightAlignX = inspResult.AlignResult.RightX;
                    string message = string.Format("Right AlignX Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, rightAlignX.Fpc.Judgement, rightAlignX.Panel.Judgement);
                    Logger.Debug(LogType.Inspection, message);
                }

                inspResult.AlignResult.RightY = algorithmTool.RunMainRightAlignY(inspTab.MergeCogImage, tab, fpcCoordinate, panelCoordinate, judgementY);
                if (inspResult.AlignResult.RightY?.Judgement != Judgement.OK)
                {
                    var rightAlignY = inspResult.AlignResult.RightY;
                    string message = string.Format("Right AlignY Inspection NG !!! Tab_{0} / Fpc_{1}, Panel_{2}", tab.Index, rightAlignY.Fpc.Judgement, rightAlignY.Panel.Judgement);
                    Logger.Debug(LogType.Inspection, message);
                }
            }
            else
            {
                inspResult.AlignResult.RightX = new AlignResult();
                inspResult.AlignResult.RightY = new AlignResult();
            }
            #endregion

            #region Center Align
            // EnableAlign false 일때 구조 생각
            inspResult.AlignResult.CenterX = Math.Abs(inspResult.AlignResult.LeftX.ResultValue_pixel - inspResult.AlignResult.RightX.ResultValue_pixel);
            #endregion

            if (AppsConfig.Instance().EnableAkkon)
            {
                var roiList = tab.AkkonParam.GetAkkonROIList();
                var coordinateList = RenewalAkkonRoi(roiList, panelCoordinate);
                var leadResultList = AkkonAlgorithm.Run(inspTab.MergeMatImage, roiList, tab.AkkonParam.AkkonAlgoritmParam, resolution_um);

                inspResult.AkkonResult = CreateAkkonResult(unitName, tab.Index, leadResultList);
            }
            //AppsInspResult.TabResultList.Add(inspResult);

            sw.Stop();
            string resultMessage = string.Format("Inspection Completed. {0}({1}ms)", (inspTab.TabScanBuffer.TabNo + 1), sw.ElapsedMilliseconds);

            Console.WriteLine(resultMessage);
        }

        private AkkonResult CreateAkkonResult(string unitName, int tabNo, List<AkkonLeadResult> leadResultList)
        {
            AkkonResult akkonResult = new AkkonResult();
            akkonResult.UnitName = unitName;
            akkonResult.TabNo = tabNo;
            akkonResult.LeadResultList = leadResultList;

            List<int> leftCountList = new List<int>();
            List<int> rightCountList = new List<int>();

            List<double> leftLengthList = new List<double>();
            List<double> rightLengthList = new List<double>();

            bool leftCountNG = false;
            bool leftLengthNG = false;
            bool rightCountNG = false;
            bool rightLengthNG = false;

            foreach (var leadResult in leadResultList)
            {
                if (leadResult.ContainPos == LeadContainPos.Left)
                {
                    leftCountNG |= leadResult.Judgement == Judgement.NG ? true : false;
                    leftCountList.Add(leadResult.AkkonCount);

                    leftLengthNG |= leadResult.Judgement == Judgement.NG ? true : false;
                    leftLengthList.Add(leadResult.LengthY_um);
                }
                else
                {
                    rightCountNG |= leadResult.Judgement == Judgement.NG ? true : false;
                    rightCountList.Add(leadResult.AkkonCount);

                    rightLengthNG |= leadResult.Judgement == Judgement.NG ? true : false;
                    rightLengthList.Add(leadResult.LengthY_um);
                }
            }

            akkonResult.CountJudgement = (leftCountNG || rightCountNG) == true ? Judgement.NG : Judgement.OK;
            akkonResult.LeftCount_Avg = (int)leftCountList.Average();
            akkonResult.LeftCount_Min = (int)leftCountList.Min();
            akkonResult.LeftCount_Max = (int)leftCountList.Max();
            akkonResult.RightCount_Avg = (int)rightCountList.Average();
            akkonResult.RightCount_Min = (int)rightCountList.Min();
            akkonResult.RightCount_Max = (int)rightCountList.Max();

            akkonResult.LengthJudgement = (leftLengthNG || rightLengthNG) == true ? Judgement.NG : Judgement.OK;
            akkonResult.Length_Left_Avg_um = (float)leftLengthList.Average();
            akkonResult.Length_Left_Min_um = (float)leftLengthList.Min();
            akkonResult.Length_Left_Max_um = (float)leftLengthList.Max();
            akkonResult.Length_Right_Avg_um = (float)rightLengthList.Average();
            akkonResult.Length_Right_Min_um = (float)rightLengthList.Min();
            akkonResult.Length_Right_Max_um = (float)rightLengthList.Max();

            akkonResult.LeadResultList = leadResultList;

            return akkonResult;
        }

        public void InitalizeInspTab(List<TabScanBuffer> bufferList)
        {
            DisposeInspTabList();
            var inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            foreach (var buffer in bufferList)
            {
                ATTInspTab inspTab = new ATTInspTab();
                inspTab.TabScanBuffer = buffer;
                inspTab.InspectEvent += AddInspectEventFuction;
                inspTab.StartInspTask();
                InspTabList.Add(inspTab);
            }
        }

        private void AddInspectEventFuction(ATTInspTab inspTab)
        {
            lock(_inspLock)
            {
                InspTabQueue.Enqueue(inspTab);
            }
        }

        public void DisposeInspTabList()
        {
            foreach (var inspTab in InspTabList)
            {
                inspTab.StopInspTask();
                inspTab.InspectEvent -= AddInspectEventFuction;
                inspTab.Dispose();
            }
            InspTabList.Clear();
        }

        private void ATTSeqRunner_GrabDoneEventHandler(string cameraName, bool isGrabDone)
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

                if(GetInspTab() is ATTInspTab inspTab)
                {
                    Run(inspTab);
                }
                
                Thread.Sleep(50);
            }
        }
        
        private ATTInspTab GetInspTab()
        {
           lock(_inspLock)
           {
                if (InspTabQueue.Count() > 0)
                    return InspTabQueue.Dequeue();
                else
                    return null;
           }
        }

        private AkkonThreadParam GetAkkonThreadParam()
        {
            lock (_akkonLock)
            {
                if (AkkonInspQueue.Count > 0)
                {
                    return AkkonInspQueue.Dequeue();
                }
                else
                    return null;
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
           
            while(InspTabQueue.Count>0)
            {
                var data = InspTabQueue.Dequeue();
                data.Dispose();
            }
            CancelAkkonInspTask.Cancel();
            AkkonInspTask.Wait();
            AkkonInspTask = null;
        }

        public void ClearResult()
        {
            //if (AppsInspResult == null)
            //    AppsInspResult = new Jastech.Apps.Structure.Data.AppsInspResult();

            //if (AppsInspResult != null)
            //    AppsInspResult.Dispose();

            //AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            //SystemManager.Instance().InitializeResult(inspModel.TabCount);
        }

        public bool IsInspectionDone()
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if (ConfigSet.Instance().Operation.VirtualMode)
            {
                RunVirtual();
                return true;
            }
            //lock (AppsInspResult)
            //{
            //    if (AppsInspResult.TabResultList.Count() == inspModel.TabCount)
            //        return true;
            //}
            return false;
        }

        public void SeqRun()
        {
            if (ModelManager.Instance().CurrentModel == null)
                return;
            //SeqStop();
           SystemManager.Instance().MachineStatus = MachineStatus.RUN;

            var lineCamera = LineCameraManager.Instance().GetAppsCamera("Camera0");

            lineCamera.GrabDoneEventHandler += ATTSeqRunner_GrabDoneEventHandler;
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
            SystemManager.Instance().MachineStatus = MachineStatus.STOP;

            var lineCamera = LineCameraManager.Instance().GetAppsCamera("Camera0");
            lineCamera.StopGrab();
            lineCamera.GrabDoneEventHandler -= ATTSeqRunner_GrabDoneEventHandler;
            LineCameraManager.Instance().GetLineCamera("Camera0").StopGrab();
            StopAkkonInspTask();
            
            // 조명 off
            LAFManager.Instance().TrackingOnOff("Laf", false);
            Logger.Write(LogType.Seq, "AutoFocus Off.");

            LineCameraManager.Instance().Stop("Camera0");
            Logger.Write(LogType.Seq, "Stop Grab.");

            if (SeqTask == null)
                return;

            //foreach (var item in AppsInspResult.TabResultList)
            //{
            //    item.Dispose();
            //}
            //AppsInspResult.TabResultList.Clear();
            SeqTaskCancellationTokenSource.Cancel();
            SeqTask.Wait();
            SeqTask = null;

            Logger.Write(LogType.Seq, "Stop Sequence.");
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
                    LineCameraManager.Instance().Stop("Camera0");
                    DisposeInspTabList();
                    break;
                }
                SeqTaskLoop();
                Thread.Sleep(50);
            }
        }

        private void SeqTaskLoop()
        {
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
                    LineCameraManager.Instance().GetLineCamera("Camera0").IsLive = false;
                    ClearResult();
                    Logger.Write(LogType.Seq, "Clear Result.");

                    InitializeBuffer();
                    Logger.Write(LogType.Seq, "Initialize Buffer.");

                    AppsInspResult.StartInspTime = DateTime.Now;
                    AppsInspResult.Cell_ID = DateTime.Now.ToString("yyyyMMddHHmmss");

                    LAFManager.Instance().TrackingOnOff("Laf", true);
                    Logger.Write(LogType.Seq, "AutoFocus On.");

                    SeqStep = SeqStep.SEQ_SCAN_START;
                    break;

                case SeqStep.SEQ_SCAN_START:
                    IsGrabDone = false;
                    // 조명 코드 작성 요망
                    var lineCamera = LineCameraManager.Instance().GetLineCamera("Camera0");
                    lineCamera.SetOperationMode(TDIOperationMode.TDI);
                    lineCamera.StartGrab();
                    Logger.Write(LogType.Seq, "Start Grab.");

                    if (MoveTo(TeachingPosType.Stage1_Scan_End, out string error2) == false)
                    {
                        // Alarm
                        // 조명 Off
                        LineCameraManager.Instance().Stop("Camera0");
                        Logger.Write(LogType.Seq, "Stop Grab.");
                        break;
                    }

                    SeqStep = SeqStep.SEQ_WAITING_SCAN_COMPLETED;
                    break;

                case SeqStep.SEQ_WAITING_SCAN_COMPLETED:
                    if (ConfigSet.Instance().Operation.VirtualMode == false)
                    {
                        if (IsGrabDone == false)
                            break;
                    }

                    Logger.Write(LogType.Seq, "Scan Grab Completed.");

                    //AppsLAFManager.Instance().AutoFocusOnOff(LAFName.Akkon.ToString(), false);
                    //Logger.Write(LogType.Seq, "AutoFocus Off.");

                    LineCameraManager.Instance().Stop("Camera0");
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
                    UpdateDailyInfo(AppsInspResult);
                    SystemManager.Instance().UpdateMainResult(AppsInspResult);
                    Console.WriteLine("Scan End to Insp Complete : " + LastInspSW.ElapsedMilliseconds.ToString());
                    SeqStep = SeqStep.SEQ_SAVE_RESULT_DATA;
                    break;

                case SeqStep.SEQ_SAVE_RESULT_DATA:
                    DailyInfoService.Save();

                    SaveInspectionResult(AppsInspResult);
                    SeqStep = SeqStep.SEQ_SAVE_IMAGE;
                    break;

                case SeqStep.SEQ_SAVE_IMAGE:

                    SaveImage(AppsInspResult);

                    SeqStep = SeqStep.SEQ_DELETE_DATA;
                    break;

                case SeqStep.SEQ_DELETE_DATA:

                    SeqStep = SeqStep.SEQ_DELETE_DATA;
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

            //AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            //var unit = inspModel.GetUnit(UnitName.Unit0);
            //for (int i = 0; i < AppsInspResult.TabResultList.Count(); i++)
            //{
            //    var tabResult = AppsInspResult.TabResultList[i];
            //    Tab tab = unit.GetTab(tabResult.TabNo);

            //    // Overlay Image
            //    Mat resultMat = GetResultImage(tabResult.Image, tabResult.AkkonResult.LeadResultList, tab.AkkonParam.AkkonAlgoritmParam);
            //    ICogImage cogImage = ConvertCogColorImage(resultMat);
            //    tabResult.AkkonResultImage = cogImage;
            //    resultMat.Dispose();

            //    // Resize Image
            //    Mat resizeMat = MatHelper.Resize(tabResult.Image, tab.AkkonParam.AkkonAlgoritmParam.ImageFilterParam.ResizeRatio);
            //    tabResult.AkkonInspImage = ConvertCogGrayImage(resizeMat);
            //    resizeMat.Dispose();
            //}

            sw.Stop();
            Console.WriteLine("Get Akkon Result Image : " + sw.ElapsedMilliseconds.ToString() + "ms");
        }

        private CogImage8Grey ConvertCogGrayImage(Mat mat)
        {
            if (mat == null)
                return null;

            int size = mat.Width * mat.Height * mat.NumberOfChannels;
            var cogImage = VisionProImageHelper.CovertImage(mat.DataPointer, mat.Width, mat.Height, mat.Step, ColorFormat.Gray) as CogImage8Grey;
            return cogImage;
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

        public Mat GetResultImage(Mat mat, List<AkkonLeadResult> resultList, AkkonAlgoritmParam AkkonParameters, float resolution_um)
        {
            if (mat == null)
                return null;

            Mat resizeMat = new Mat();
            Size newSize = new Size((int)(mat.Width * AkkonParameters.ImageFilterParam.ResizeRatio), (int)(mat.Height * AkkonParameters.ImageFilterParam.ResizeRatio));
            CvInvoke.Resize(mat, resizeMat, newSize);
            Mat colorMat = new Mat();
            CvInvoke.CvtColor(resizeMat, colorMat, ColorConversion.Gray2Bgr);
            resizeMat.Dispose();

            DrawParam autoDrawParam = new DrawParam();
            autoDrawParam.ContainLeadCount = true;
            foreach (var result in resultList)
            {
                var lead = result.Roi;
                var startPoint = new Point((int)result.Offset.ToWorldX, (int)result.Offset.ToWorldY);

                Point leftTop = new Point((int)lead.LeftTopX + startPoint.X, (int)lead.LeftTopY + startPoint.Y);
                Point leftBottom = new Point((int)lead.LeftBottomX + startPoint.X, (int)lead.LeftBottomY + startPoint.Y);
                Point rightTop = new Point((int)lead.RightTopX + startPoint.X, (int)lead.RightTopY + startPoint.Y);
                Point rightBottom = new Point((int)lead.RightBottomX + startPoint.X, (int)lead.RightBottomY + startPoint.Y);

                if (autoDrawParam.ContainLeadROI)
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
                    rectRect.X = (int)(blob.BoundingRect.X + result.Offset.ToWorldX + result.Offset.X);
                    rectRect.Y = (int)(blob.BoundingRect.Y + result.Offset.ToWorldY + result.Offset.Y);
                    rectRect.Width = blob.BoundingRect.Width;
                    rectRect.Height = blob.BoundingRect.Height;

                    Point center = new Point(rectRect.X + (rectRect.Width / 2), rectRect.Y + (rectRect.Height / 2));
                    int radius = rectRect.Width > rectRect.Height ? rectRect.Width : rectRect.Height;

                    int size = blob.BoundingRect.Width * blob.BoundingRect.Height;
                    double calcMinArea = AkkonParameters.ShapeFilterParam.MinArea_um * resolution_um;
                    double calcMaxArea = AkkonParameters.ShapeFilterParam.MaxArea_um * resolution_um;
                    if (calcMinArea <= size && size <= calcMaxArea)
                    {
                        blobCount++;
                        CvInvoke.Circle(colorMat, center, radius / 2, new MCvScalar(255), 1);
                    }
                    else
                    {
                        //if (AkkonParameters.DrawOption.ContainNG)
                        //    CvInvoke.Circle(colorMat, center, radius / 2, new MCvScalar(0), 1);
                    }

                }

                if (autoDrawParam.ContainLeadCount)
                {
                    string leadIndexString = result.Roi.Index.ToString();
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
            var lineCamera = LineCameraManager.Instance().GetLineCamera("Camera0");
            lineCamera.InitGrabSettings();
            InitalizeInspTab(lineCamera.TabScanBufferList);
        }

        public void RunVirtual()
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            Tab tab = inspModel.GetUnit(UnitName.Unit0).GetTab(0);

            //Mat tabMatImage = new Mat(@"D:\Tab1.bmp", Emgu.CV.CvEnum.ImreadModes.Grayscale);

            // ICogImage tabCogImage = ConvertCogImage(tabMatImage);
            // MainAlgorithmTool tool = new MainAlgorithmTool();

            //var result = tool.MainRunInspect(tab, tabMatImage, 30.0f, 80.0f);

            // AppsInspResult.TabResultList.Add(result);
        }

        private void SetCoordinateData(CoordinateTransform coordinate, TabInspResult tabInspResult)
        {
            PointF teachedLeftPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.ReferencePos;
            PointF teachedRightPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.ReferencePos;
            PointF searchedLeftPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Left.MaxMatchPos.FoundPos;
            PointF searchedRightPoint = tabInspResult.MarkResult.FpcMark.FoundedMark.Right.MaxMatchPos.FoundPos;
            coordinate.SetReferenceData(teachedLeftPoint, teachedRightPoint);
            coordinate.SetTargetData(searchedLeftPoint, searchedRightPoint);
        }

        private List<AkkonROI> RenewalAkkonRoi(List<AkkonROI> roiList, CoordinateTransform panelCoordinate)
        {
            List<AkkonROI> newList = new List<AkkonROI>();

            foreach (var item in roiList)
            {
                PointF leftTop = item.GetLeftTopPoint();
                PointF rightTop = item.GetRightTopPoint();
                PointF leftBottom = item.GetLeftBottomPoint();
                PointF rightBottom = item.GetRightBottomPoint();

                var newLeftTop = panelCoordinate.GetCoordinate(leftTop);
                var newRightTop = panelCoordinate.GetCoordinate(rightTop);
                var newLeftBottom = panelCoordinate.GetCoordinate(leftBottom);
                var newRightBottom = panelCoordinate.GetCoordinate(rightBottom);

                AkkonROI akkonRoi = new AkkonROI();

                akkonRoi.SetLeftTopPoint(newLeftTop);
                akkonRoi.SetLeftTopPoint(newRightTop);
                akkonRoi.SetLeftTopPoint(newLeftBottom);
                akkonRoi.SetLeftTopPoint(newRightBottom);

                newList.Add(akkonRoi);
            }

            return newList;
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

            string path = Path.Combine(ConfigSet.Instance().Path.Result, inspModel.Name, month, day, folderPath);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            //SaveResultImage(path, inspResult.TabResultList);
        }

        private void SaveResultImage(string resultPath, List<TabInspResult> insTabResultList)
        {
            //if (ConfigSet.Instance().Operation.VirtualMode)
            //    return;

            //string path = Path.Combine(resultPath, "Orgin");
            //if (Directory.Exists(path) == false)
            //    Directory.CreateDirectory(path);

            //string okExtension = ".bmp";

            //if(ConfigSet.Instance().Operation.ExtensionOKImage == ImageExtension.Bmp)
            //    okExtension = ".bmp";
            //else if (ConfigSet.Instance().Operation.ExtensionOKImage == ImageExtension.Jpg)
            //    okExtension = ".jpg";
            //else if (ConfigSet.Instance().Operation.ExtensionOKImage == ImageExtension.Png)
            //    okExtension = ".png";

            //string ngExtension = ".bmp";

            //if (ConfigSet.Instance().Operation.ExtensionNGImage == ImageExtension.Bmp)
            //    ngExtension = ".bmp";
            //else if (ConfigSet.Instance().Operation.ExtensionNGImage == ImageExtension.Jpg)
            //    ngExtension = ".jpg";
            //else if (ConfigSet.Instance().Operation.ExtensionNGImage == ImageExtension.Png)
            //    ngExtension = ".png";


            //foreach (var result in insTabResultList)
            //{
            //    if (result.Judgement == Judgement.OK)
            //    {
            //        if(ConfigSet.Instance().Operation.SaveImageOK)
            //        {
            //            string imageName = "Tab_" + result.TabNo.ToString() +"_OK_" + okExtension;
            //            string imagePath = Path.Combine(path, imageName);
            //            result.Image.Save(imagePath);
            //        }
            //    }
            //    else
            //    {
            //        if (ConfigSet.Instance().Operation.SaveImageNG)
            //        {
            //            string imageName = "Tab_" + result.TabNo.ToString() + "_NG_" + ngExtension;
            //            string imagePath = Path.Combine(path, imageName);
            //            result.Image.Save(imagePath);
            //        }
            //    }
            //}
        }

        private void UpdateDailyInfo(AppsInspResult inspResult)
        {
            var dailyData = new DailyData();
            UpdateAlignDailyInfo(inspResult, ref dailyData);
            UpdateAkkonDailyInfo(inspResult, ref dailyData);

            AddDailyInfo(dailyData);
        }

        private void UpdateAlignDailyInfo(AppsInspResult inspResult, ref DailyData dailyData)
        {
            //foreach (var item in inspResult.TabResultList)
            //{
            //    AlignDailyInfo alignInfo = new AlignDailyInfo();

            //    alignInfo.InspectionTime = inspResult.EndInspTime.ToString("HH:mm:ss");
            //    alignInfo.PanelID = inspResult.Cell_ID;
            //    alignInfo.TabNo = item.TabNo;
            //    alignInfo.Judgement = item.Judgement;
            //    alignInfo.LX = item.AlignResult.LeftX.ResultValue_pixel;
            //    alignInfo.LY = item.AlignResult.LeftY.ResultValue_pixel;
            //    alignInfo.RX = item.AlignResult.RightX.ResultValue_pixel;
            //    alignInfo.RY = item.AlignResult.RightY.ResultValue_pixel;
            //    alignInfo.CX = item.AlignResult.CenterX;

            //    dailyData.AddAlignInfo(alignInfo);
            //}
        }

        private void UpdateAkkonDailyInfo(AppsInspResult inspResult, ref DailyData dailyData)
        {
            //foreach (var item in inspResult.TabResultList)
            //{
            //    AkkonDailyInfo akkonInfo = new AkkonDailyInfo();

            //    akkonInfo.InspectionTime = inspResult.EndInspTime.ToString("HH:mm:ss");
            //    akkonInfo.PanelID = inspResult.Cell_ID;
            //    akkonInfo.TabNo = item.TabNo;

            //    var countJudgement = item.AkkonResult.CountJudgement;
            //    var lengthJudgement = item.AkkonResult.LengthJudgement;

            //    if (countJudgement == Judgement.OK || lengthJudgement == Judgement.OK)
            //        akkonInfo.Judgement = Judgement.OK;
            //    else
            //        akkonInfo.Judgement = Judgement.NG;
            //    var akkonResult = item.AkkonResult;

            //    int minCount = akkonResult.LeftCount_Avg > akkonResult.RightCount_Min ? akkonResult.RightCount_Min : akkonResult.LeftCount_Avg;
            //    float minLength = akkonResult.Length_Left_Min_um > akkonResult.Length_Right_Min_um ? akkonResult.Length_Right_Min_um : akkonResult.Length_Left_Min_um;

            //    akkonInfo.MinBlobCount = minCount;
            //    akkonInfo.MinLength = minLength;

            //    dailyData.AddAkkonInfo(akkonInfo);
            //}
        }

        private void AddDailyInfo(DailyData dailyData)
        {
            var dailyInfo = DailyInfoService.GetDailyInfo();

            if (dailyInfo == null)
                return;

            dailyInfo.AddDailyDataList(dailyData);
        }

        private void SaveInspectionResult(AppsInspResult inspResult)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            DateTime currentTime = inspResult.StartInspTime;

            string month = currentTime.ToString("MM");
            string day = currentTime.ToString("dd");
            string folderPath = inspResult.Cell_ID;

            string path = Path.Combine(ConfigSet.Instance().Path.Result, inspModel.Name, month, day);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            SaveAlignResult(path, inspResult);
            SaveAkkonResult(path, inspResult);
            SaveUPHResult(path, inspResult);
        }

        private void SaveAlignResult(string resultPath, AppsInspResult inspResult)
        {
            //string filename = string.Format("Align.csv");
            //string csvFile = Path.Combine(resultPath, filename);
            //if (File.Exists(csvFile) == false)
            //{
            //    List<string> header = new List<string>
            //    {
            //        "Inspection Time",
            //        "Panel ID",
            //        "Tab",
            //        "Judge",
            //        "Lx",
            //        "Ly",
            //        "Cx",
            //        "Rx",
            //        "Ry"
            //    };

            //    CSVHelper.WriteHeader(csvFile, header);
            //}

            //List<List<string>> dataList = new List<List<string>>();
            //for (int tabNo = 0; tabNo < inspResult.TabResultList.Count; tabNo++)
            //{
            //    var alignResult = inspResult.TabResultList[tabNo].AlignResult;
            //    Judgement judgement = alignResult.Judgement;

            //    List<string> tabData = new List<string>
            //    {
            //        inspResult.EndInspTime.ToString("HH:mm:ss"),                                    // Insp Time
            //        inspResult.Cell_ID,                                                             // Panel ID
            //        tabNo.ToString(),                                                               // Tab
            //        judgement.ToString(),                       // Judge
            //        alignResult.LeftX.ResultValue_pixel.ToString("F4"),          // Left Align X
            //        alignResult.LeftY.ResultValue_pixel.ToString("F4"),          // Left Align Y
            //        alignResult.CenterX.ToString("F4"),                         // Center Align X
            //        alignResult.RightX.ResultValue_pixel.ToString("F4"),         // Right Align X
            //        alignResult.RightY.ResultValue_pixel.ToString("F4"),         // Right Align Y     // Right Align Y
            //    };

            //    dataList.Add(tabData);
            //}

            //CSVHelper.WriteData(csvFile, dataList);
        }

        private void SaveAkkonResult(string resultPath, AppsInspResult inspResult)
        {
            //string filename = string.Format("Akkon.csv");
            //string csvFile = Path.Combine(resultPath, filename);
            //if (File.Exists(csvFile) == false)
            //{
            //    List<string> header = new List<string>
            //    {
            //         "Inspection Time",
            //        "Panel ID",
            //        "Tab",
            //        "Judge",
            //        "Avg Count",
            //        "Avg Length",
            //    };

            //    CSVHelper.WriteHeader(csvFile, header);
            //}

            //List<List<string>> dataList = new List<List<string>>();
            //for (int tabNo = 0; tabNo < inspResult.TabResultList.Count; tabNo++)
            //{
            //    var akkonResult = inspResult.TabResultList[tabNo].AkkonResult;
            //    Judgement judgement = akkonResult.IsAkkonGood() == true ? Judgement.OK : Judgement.NG;

            //    int avgCount = (akkonResult.LeftCount_Avg + akkonResult.RightCount_Avg) / 2;
            //    float avgLength = (akkonResult.Length_Left_Avg_um + akkonResult.Length_Right_Avg_um) / 2.0F;

            //    List<string> tabData = new List<string>
            //    {
            //        inspResult.EndInspTime.ToString("HH:mm:ss"),
            //        inspResult.Cell_ID,
            //        tabNo.ToString(),
            //        judgement.ToString(),
            //        avgCount.ToString(),
            //        avgLength.ToString("F4"),
            //    };

            //    dataList.Add(tabData);
            //}

            //CSVHelper.WriteData(csvFile, dataList);
        }

        private void SaveUPHResult(string resultPath, AppsInspResult inspResult)
        {
            //string filename = string.Format("UPH.csv");
            //string csvFile = Path.Combine(resultPath, filename);
            //if (File.Exists(csvFile) == false)
            //{
            //    List<string> header = new List<string>
            //    {
            //        "Inspection Time",
            //        "Panel ID",
            //        "Stage No.",
            //        "Tab No.",

            //        "Count Min",
            //        "Count Avg",
            //        "Length Min",
            //        "Length Avg",
            //        "Strength Min",
            //        "Strength Avg",

            //        "Left Align X",
            //        "Left Align Y",
            //        "Center Align X",
            //        "Right Align X",
            //        "Right Align Y",

            //        "ACF Head",
            //        "Pre Head",
            //        "Main Head",

            //        "Judge",
            //        "Cause",
            //        "Op Judge"
            //    };

            //    CSVHelper.WriteHeader(csvFile, header);
            //}

            //List<List<string>> dataList = new List<List<string>>();
            //for (int tabNo = 0; tabNo < inspResult.TabResultList.Count; tabNo++)
            //{
            //    List<string> tabData = new List<string>
            //    {
            //        inspResult.EndInspTime.ToString("HH:mm:ss"),                                    // Insp Time
            //        inspResult.Cell_ID,                                                             // Panel ID
            //        1.ToString(),                                                                   // Stage
            //        tabNo.ToString(),                                                               // Tab

            //        //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgBlobCount.ToString(),
            //        //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgLength.ToString("F3"),
            //        //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgStrength.ToString("F3"),
            //        //inspResult.TabResultList[tabNo].MacronAkkonResult.AvgStd.ToString("F3"),
            //        (tabNo + 1).ToString(),                                                         // Count Min
            //        (tabNo + 2).ToString("F4"),                                                     // Count Avg
            //        (tabNo + 3).ToString(),                                                         // Length Min
            //        (tabNo + 4).ToString("F4"),                                                     // Length Avg
            //        (tabNo + 5).ToString(),                                                         // Strength Min
            //        (tabNo + 6).ToString("F4"),                                                     // Strength Avg

            //        inspResult.TabResultList[tabNo].AlignResult.LeftX.ResultValue_pixel.ToString("F4"),          // Left Align X
            //        inspResult.TabResultList[tabNo].AlignResult.LeftY.ResultValue_pixel.ToString("F4"),          // Left Align Y
            //        inspResult.TabResultList[tabNo].AlignResult.CenterX.ToString("F4"),                         // Center Align X
            //        inspResult.TabResultList[tabNo].AlignResult.RightX.ResultValue_pixel.ToString("F4"),         // Right Align X
            //        inspResult.TabResultList[tabNo].AlignResult.RightY.ResultValue_pixel.ToString("F4"),         // Right Align Y

            //        (tabNo + 7).ToString(),                                                         // ACF Head
            //        (tabNo + 8).ToString(),                                                         // Pre Head
            //        (tabNo + 9).ToString(),                                                         // Main Head

            //        inspResult.TabResultList[tabNo].Judgement.ToString(),                           // Judge
            //        "Count",                                                                        // Cause
            //        "OP_OK"                                                                         // OP Judge
            //    };

            //    dataList.Add(tabData);
            //}

            //CSVHelper.WriteData(csvFile, dataList);
        }

        private string GetExtensionOKImage()
        {
            return "." + ConfigSet.Instance().Operation.ExtensionOKImage;
        }

        private string GetExtensionNGImage()
        {
            return "." + ConfigSet.Instance().Operation.ExtensionNGImage;
        }

        private Axis GetAxis(AxisHandlerName axisHandlerName, AxisName axisName)
        {
            return MotionManager.Instance().GetAxis(axisHandlerName, axisName);
        }

        public bool IsAxisInPosition(UnitName unitName, TeachingPosType teachingPos, Axis axis)
        {
            return MotionManager.Instance().IsAxisInPosition(unitName, teachingPos, axis);
        }

        public bool MoveTo(TeachingPosType teachingPos, out string error)
        {
            error = "";

            if (ConfigSet.Instance().Operation.VirtualMode)
                return true;

            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            MotionManager manager = MotionManager.Instance();

            var teachingInfo = inspModel.GetUnit(UnitName.Unit0).GetTeachingInfo(teachingPos);

            Axis axisX = GetAxis(AxisHandlerName.Handler0, AxisName.X);
            Axis axisY = GetAxis(AxisHandlerName.Handler0, AxisName.Y);
            //Axis axisZ = GetAxis(AxisHandlerName.Handler0, AxisName.Z);

            var movingParamX = teachingInfo.GetMovingParam(AxisName.X.ToString());
            var movingParamY = teachingInfo.GetMovingParam(AxisName.Y.ToString());
            var movingParamZ = teachingInfo.GetMovingParam(AxisName.Z0.ToString());

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
            MotionManager manager = MotionManager.Instance();
            if (manager.IsAxisInPosition(UnitName.Unit0, teachingPos, axis) == false)
            {
                Stopwatch sw = new Stopwatch();
                sw.Restart();

                manager.StartAbsoluteMove(UnitName.Unit0, teachingPos, axis);

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
