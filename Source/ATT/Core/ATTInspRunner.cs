using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform;
using Jastech.Apps.Winform.Settings;
using Jastech.Framework.Device.Motions;
using Jastech.Framework.Imaging.Result;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Parameters;
using Jastech.Framework.Imaging.VisionPro.VisionAlgorithms.Results;
using Jastech.Framework.Macron.Akkon.Results;
using Jastech.Framework.Structure;
using Jastech.Framework.Util.Helper;
using Jastech.Framework.Winform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        private List<AppsInspResult> AppsInspResultList { get; set; } = new List<AppsInspResult>();

        public ATTInspRunner()
        {
            AppsLineCameraManager.Instance().TabImageGrabCompletedEventHandler += ATTSeqRunner_TabImageGrabCompletedEventHandler;
            AppsLineCameraManager.Instance().GrabDoneEventHanlder += ATTSeqRunner_GrabDoneEventHanlder;
        }

        private void ATTSeqRunner_TabImageGrabCompletedEventHandler(TabScanImage image)
        {
            Task task = new Task(() => Run(image));
            task.Start();
        }

        private void ATTSeqRunner_GrabDoneEventHanlder(bool isGrabDone)
        {
            IsGrabDone = isGrabDone;
        }

        public void ClearResult()
        {
            for (int i = 0; i < AppsInspResultList.Count(); i++)
            {
                AppsInspResultList[i].Dispose();
                AppsInspResultList[i] = null;
            }
            AppsInspResultList.Clear();
        }

        public bool IsInspectionDone()
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;
            if(AppsConfig.Instance().Operation.VirtualMode)
            {
                RunVirtual();
                return true;
            }
            lock (AppsInspResultList)
            {
                if (AppsInspResultList.Count() == inspModel.TabCount)
                    return true;
            }
            return false;
        }

        public void SeqRun()
        {
            if (ModelManager.Instance().CurrentModel == null)
                return;

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

            AppsLineCameraManager.Instance().StopGrab();
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
                    AppsLineCameraManager.Instance().StopGrab();
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

                    AppsLAFManager.Instance().AutoFocusOnOff(LAFName.Akkon.ToString(), true);
                    Logger.Write(LogType.Seq, "AutoFocus On.");

                    SeqStep = SeqStep.SEQ_SCAN_START;
                    break;

                case SeqStep.SEQ_SCAN_START:

                    IsGrabDone = false;
                    // 조명 코드 작성 요망

                    AppsLineCameraManager.Instance().StartGrab(CameraName.LinscanMIL0);
                    Logger.Write(LogType.Seq, "Start Grab.");

                    if (MoveTo(TeachingPosType.Stage1_Scan_End, out string error2) == false)
                    {
                        // Alarm
                        // 조명 Off
                        AppsLineCameraManager.Instance().StopGrab(CameraName.LinscanMIL0);
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
       
                    Logger.Write(LogType.Seq, "Scan Grab Completed.");

                    AppsLAFManager.Instance().AutoFocusOnOff(LAFName.Akkon.ToString(), false);
                    Logger.Write(LogType.Seq, "AutoFocus Off.");

                    AppsLineCameraManager.Instance().StopGrab(CameraName.LinscanMIL0);
                    Logger.Write(LogType.Seq, "Stop Grab.");

                    SeqStep = SeqStep.SEQ_WAITING_INSPECTION_DONE;
                    break;

                case SeqStep.SEQ_WAITING_INSPECTION_DONE:
                    if(IsInspectionDone() == false)
                        break;

                    SeqStep = SeqStep.SEQ_UI_RESULT_UPDATE;
                    break;

                case SeqStep.SEQ_UI_RESULT_UPDATE:


                    SeqStep = SeqStep.SEQ_SAVE_IMAGE;
                    break;

                case SeqStep.SEQ_SAVE_IMAGE:

                    SaveImage(AppsInspResultList);

                    SeqStep = SeqStep.SEQ_DELETE_DATA;
                    break;

                case SeqStep.SEQ_DELETE_DATA:

                    SeqStep = SeqStep.SEQ_CHECK_STANDBY;
                    break;

                case SeqStep.SEQ_CHECK_STANDBY:

                    //if (!AppsMotionManager.Instance().IsMotionInPosition(UnitName.Unit0, AxisHandlerName.Handler0, AxisName.X, TeachingPosType.Standby))
                    //    break;

                    SeqStep = SeqStep.SEQ_IDLE;
                    break;

                //case SeqStep.SEQ_ALIGN_INSPECTION:

                //    var alignParam = tab.AlignParamList;

                //    var alignResultX = AlgorithmTool.RunAlignX(cogImage, alignParam[0].CaliperParams, alignParam[0].LeadCount);
                //    var alignResultY = AlgorithmTool.RunAlignY(cogImage, alignParam[0].CaliperParams);

                //    if (alignResultX.Result == Result.OK)
                //    {
                //        message = "Align X Search OK - Result : " + alignResultX.Result.ToString() + " / T/T : " + alignResultX.CogAlignResult[0].TactTime.ToString();
                //        Logger.Write(LogType.Seq, message, DateTime.Now);

                //        SetAlignXResult(alignResultX);
                //    }
                //    else if (alignResultY.Result == Result.Fail)
                //    {
                //        message = "Align X Search FAIL";
                //        Logger.Write(LogType.Seq, message, DateTime.Now);
                //    }
                //    else { }

                //    if (alignResultY.Result == Result.OK)
                //    {
                //        message = "Align Y Search OK - Result : " + alignResultY.Result.ToString() + " / T/T : " + alignResultY.CogAlignResult[0].TactTime.ToString();
                //        Logger.Write(LogType.Seq, message, DateTime.Now);

                //        SetAlignYResult(alignResultY);
                //    }
                //    else if (alignResultY.Result == Result.Fail)
                //    {
                //        message = "Align Y Search FAIL";
                //        Logger.Write(LogType.Seq, message, DateTime.Now);
                //    }
                //    else { }

                //    SeqStep = SeqStep.SEQ_ALIGN_INSPECTION_COMPLETED;
                //    break;




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
            Axis axisZ = GetAxis(AxisHandlerName.Handler0, AxisName.Z);

            var movingParamX = teachingInfo.GetMovingParam(AxisName.X.ToString());
            var movingParamY = teachingInfo.GetMovingParam(AxisName.Y.ToString());
            var movingParamZ = teachingInfo.GetMovingParam(AxisName.Z.ToString());

            if (MoveAxis(teachingPos, axisZ, movingParamZ) == false)
            {
                error = string.Format("Move To Axis Z TimeOut!({0})", movingParamZ.MovingTimeOut.ToString());
                Logger.Write(LogType.Seq, error);
                return false;
            }
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

        public void Run(TabScanImage ScanImage)
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            MainAlgorithmTool tool = new MainAlgorithmTool();
            Tab tab = inspModel.GetUnit(UnitName.Unit0).GetTab(ScanImage.TabNo);
            tool.MainRunInspect(tab, ScanImage.GetMergeImage(), 10.0f, 10.0f);
            //

            //Tab tab = inspModel.GetUnit(UnitName.Unit0).GetTab(ScanImage.TabNo);

            //Mat tabMatImage = ScanImage.GetMergeImage();
            //ICogImage tabCogImage = AlgorithmTool.ConvertCogImage(tabMatImage);

            //AppsInspResult result = new AppsInspResult();

            //result.TabNo = tab.Index;
            //result.Image = tabMatImage;
            //result.PatternMatching = AlgorithmTool.RunPatternMatching(tab, tabCogImage);

            //if (result.PatternMatching == null)
            //{
            //    string message = string.Format("Pattern Matching Fail.");
            //    Logger.Debug(LogType.Inspection, message);

            //    lock (AppsInspResultList)
            //        AppsInspResultList.Add(result);

            //    return;
            //}

            //result.Akkon = AlgorithmTool.RunAkkon(tabMatImage, tab.AkkonParam, tab.StageIndex, tab.Index);

            //lock (AppsInspResultList)
            //    AppsInspResultList.Add(result);
        }

        public void RunVirtual()
        {
            AppsInspModel inspModel = ModelManager.Instance().CurrentModel as AppsInspModel;

            Tab tab = inspModel.GetUnit(UnitName.Unit0).GetTab(0);

            Mat tabMatImage = new Mat(@"D:\Tab1.bmp", Emgu.CV.CvEnum.ImreadModes.Grayscale);

            //ICogImage tabCogImage = AlgorithmTool.ConvertCogImage(tabMatImage);
            MainAlgorithmTool tool = new MainAlgorithmTool();

           var result = tool.MainRunInspect(tab, tabMatImage, 30.0f, 80.0f);

            SystemManager.Instance().UpdateMainResult(result);
        }

        private void SaveImage(List<AppsInspResult> insResultList)
        {
            //foreach (var item in collection)
            //{

            //}
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
