using Cognex.VisionPro;
using Cognex.VisionPro.Implementation.Internal;
using Emgu.CV;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Core;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Device.Cameras;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Macron.Akkon.Results;
using Jastech.Framework.Structure;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATT.Core
{
    public class ATTInspRunner
    {
        private Axis _axis { get; set; } = null;

        private AlgorithmTool AlgorithmTool = new AlgorithmTool();

        private Task SeqTask { get; set; }

        private CancellationTokenSource SeqTaskCancellationTokenSource { get; set; }

        private SeqStep SeqStep { get; set; } = SeqStep.SEQ_IDLE;

        private Task _movingTask { get; set; } = null;

        public bool IsPanelIn { get; set; } = false;

        private bool IsGrabDone { get; set; } = false;

        private AppsInspResult AppsInspResult { get; set; } = null;

        private Stopwatch LastInspSW { get; set; } = new Stopwatch();

        public ATTInspRunner()
        {
          
        }
        private void ATTSeqRunner_TabImageGrabCompletedEventHandler(string cameraName, TabScanImage tabScanImage)
        {
            Mat matImage = tabScanImage.GetMergeImage();
            ICogImage cogImage = tabScanImage.ConvertCogGrayImage(matImage);
            //AppsInspResult.TabResultList.Add(new TabInspResult());
            //if (tabScanImage.TabNo == 0)
            //{
                Console.WriteLine("Run Inspection. " + tabScanImage.TabNo.ToString());
                Task task = new Task(() => Run(tabScanImage, matImage, cogImage));
                task.Start();
            //}
            //else if (tabScanImage.TabNo == 4)
            //{
            //    AppsInspResult.TabResultList.Add(new TabInspResult());
            //    AppsInspResult.TabResultList.Add(new TabInspResult());
            //    AppsInspResult.TabResultList.Add(new TabInspResult());
            //    AppsInspResult.TabResultList.Add(new TabInspResult());
            //}

        }

        private void ATTSeqRunner_GrabDoneEventHanlder(string cameraName, bool isGrabDone)
        {
            IsGrabDone = isGrabDone;
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
            if(AppsConfig.Instance().Operation.VirtualMode)
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
            Logger.Write(LogType.Seq, "Stop Sequence.");

            if (SeqTask == null)
                return;

            //if (CurrentInspState == InspState.Idle)
            //{
            //    PlcService.CheckHeartBeat = false;
            //    return;
            //}

            // 조명 off
            AppsLAFManager.Instance().AutoFocusOnOff(LAFName.Akkon.ToString(), false);
            Logger.Write(LogType.Seq, "AutoFocus Off.");

            AppsLineCameraManager.Instance().Stop(CameraName.LinscanMIL0);
            Logger.Write(LogType.Seq, "Stop Grab.");

        }


        private void SeqTaskAction()
        {
            Logger.Write(LogType.Seq, "Start Sequence.");

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

                    //Thread.Sleep(1000);
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
                    if(AppsConfig.Instance().Operation.VirtualMode == false)
                    {
                        if (IsGrabDone == false)
                            break;
                    }

                    LastInspSW.Restart();

                    Logger.Write(LogType.Seq, "Scan Grab Completed.");

                    AppsLAFManager.Instance().AutoFocusOnOff(LAFName.Akkon.ToString(), false);
                    Logger.Write(LogType.Seq, "AutoFocus Off.");

                    AppsLineCameraManager.Instance().Stop(CameraName.LinscanMIL0);
                    Logger.Write(LogType.Seq, "Stop Grab.");

                    SeqStep = SeqStep.SEQ_WAITING_INSPECTION_DONE;
                    break;

                case SeqStep.SEQ_WAITING_INSPECTION_DONE:
                    if(IsInspectionDone() == false)
                        break;


                    LastInspSW.Stop();
                    AppsInspResult.EndInspTime = DateTime.Now;
                    AppsInspResult.LastInspTime = LastInspSW.ElapsedMilliseconds.ToString();

                    SeqStep = SeqStep.SEQ_UI_RESULT_UPDATE;
                    break;

                case SeqStep.SEQ_UI_RESULT_UPDATE:
                    SystemManager.Instance().UpdateMainResult(AppsInspResult);
                    SeqStep = SeqStep.SEQ_SAVE_IMAGE;
                    break;

                case SeqStep.SEQ_SAVE_IMAGE:

                    //SaveImage(AppsInspResult);

                    SeqStep = SeqStep.SEQ_DELETE_DATA;
                    break;

                case SeqStep.SEQ_DELETE_DATA:

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
            if(MoveAxis(teachingPos, axisX, movingParamX) == false)
            {
                error = string.Format("Move To Axis X TimeOut!({0})", movingParamX.MovingTimeOut.ToString());
                Logger.Write(LogType.Seq, error);
                return false;
            }
            if(MoveAxis(teachingPos, axisY, movingParamY) == false)
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

                    if(sw.ElapsedMilliseconds >= movingParam.MovingTimeOut)
                    {
                        return false;
                    }
                    Thread.Sleep(10);
                }
            }

            return true;
        }

        public void Run(TabScanImage ScanImage, Mat mergeMat, ICogImage cogMergeImage)
        {
            Console.WriteLine("In Run Thread  : " + ScanImage.TabNo.ToString());
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            MainAlgorithmTool tool = new MainAlgorithmTool();
            Tab tab = inspModel.GetUnit(UnitName.Unit0).GetTab(ScanImage.TabNo);
           
            var result = tool.MainRunInspect(tab, mergeMat, cogMergeImage, 100.0f, 100.0f);

            AppsInspResult.TabResultList.Add(result);

            Console.WriteLine("Out Run Thread  : " + ScanImage.TabNo.ToString());
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

        private void SaveImage(AppsInspResult inspResult)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            DateTime currentTime = inspResult.StartInspTime;
            string today = currentTime.ToString("yyyyMMdd");
            string time = currentTime.ToString("yyyyMMddHHmmss");

            string folderPath = inspResult.Cell_ID + "_" + time;

            string path = Path.Combine(AppsConfig.Instance().Path.Result, inspModel.Name, today, folderPath);

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            // OrgImage
            SaveOrgImage(path, inspResult.TabResultList);
            SaveAlignResult(path, inspResult.Cell_ID, inspResult.TabResultList);
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

        private void SaveAlignResult(string resultPath, string panelId, List<TabInspResult> inspTabResultList)
        {
            string csvFile = Path.Combine(resultPath, "Align.csv");
            if(File.Exists(csvFile) == false)
            {
                List<string> header = new List<string>();
                header.Add("Panel ID");
                header.Add("Tab No");
                header.Add("Judge");
                header.Add("Lx");
                header.Add("Ly");
                header.Add("Rx");
                header.Add("Ry");
                header.Add("Cx");

                CSVHelper.WriteHeader(csvFile, header);
            }
          
            foreach (var tabResult in inspTabResultList)
            {
                List<string> dataList = new List<string>();
                dataList.Add(panelId);
                dataList.Add(tabResult.TabNo.ToString());
                dataList.Add(tabResult.AlignJudgement.ToString());

                float lx = tabResult.LeftAlignX.X;
                float ly = tabResult.LeftAlignY.Y;
                float rx = tabResult.RightAlignX.X;
                float ry = tabResult.RightAlignY.Y;
                float cx = (lx + rx) / 2.0f;

                dataList.Add(lx.ToString("F3"));
                dataList.Add(ly.ToString("F3"));
                dataList.Add(rx.ToString("F3"));
                dataList.Add(ry.ToString("F3"));
                dataList.Add(cx.ToString("F3"));


            }
        }

        private string GetExtensionOKImage()
        {
            return "." + AppsConfig.Instance().Operation.ExtensionOKImage;
        }

        private string GetExtensionNGImage()
        {
            return "." + AppsConfig.Instance().Operation.ExtensionNGImage;
        }
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
        SEQ_SAVE_IMAGE,
        SEQ_DELETE_DATA,
        SEQ_CHECK_STANDBY,
    }
}
