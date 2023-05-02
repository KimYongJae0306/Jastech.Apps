using Cognex.VisionPro;
using Emgu.CV;
using Jastech.Apps.Structure;
using Jastech.Apps.Structure.Data;
using Jastech.Apps.Structure.VisionTool;
using Jastech.Apps.Winform;
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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATT.Core
{
    public class ATTInspRunner
    {
        private AlgorithmTool AlgorithmTool = new AlgorithmTool();

        private Task SeqTask { get; set; }

        private CancellationTokenSource SeqTaskCancellationTokenSource { get; set; }

        private SeqStep SeqStep { get; set; } = SeqStep.SEQ_IDLE;

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

        private void SeqTaskAction()
        {
            var cancellationToken = SeqTaskCancellationTokenSource.Token;
            cancellationToken.ThrowIfCancellationRequested();
            SeqStep = SeqStep.SEQ_START;
            //카메라 그랩 시작 
            while (true)
            {
                // 작업 취소
                if (cancellationToken.IsCancellationRequested)
                {
                    SeqStep = SeqStep.SEQ_IDLE;
                    //InspDeviceService.LightTurnOff();
                    //InspDeviceService.CameraMutiGrab(false);
                    break;
                }
                //if (CurrentInspState == InspState.Idle)
                //{
                //    SeqStep = SeqStep.SEQ_IDIE_WAITING;
                //}
                SeqTaskLoop();
            }
        }

        private void SeqTaskLoop()
        {
            ICogImage cogImage = Jastech.Framework.Imaging.VisionPro.CogImageHelper.Load(@"D:\Tab1.bmp");

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
                    Logger.Write(LogType.Seq, "Start Sequence.", DateTime.Now);

                    SeqStep = SeqStep.SEQ_READY;
                    break;

                case SeqStep.SEQ_READY:

                    AppsMotionManager.Instance().MoveTo(UnitName.Unit0, AxisHandlerName.Handler0, AxisName.X, TeachingPositionType.Stage1_Scan_Start);

                    SeqStep = SeqStep.SEQ_WAITING;
                    break;

                case SeqStep.SEQ_WAITING:

                    if (!AppsMotionManager.Instance().IsMovingCompleted(UnitName.Unit0, AxisHandlerName.Handler0, AxisName.X, TeachingPositionType.Stage1_Scan_Start))
                        break;

                    SeqStep = SeqStep.SEQ_SCAN_START;
                    break;

                case SeqStep.SEQ_SCAN_START:

                    AppsLAFManager.Instance().AutoFocusOnOff(LAFName.Align.ToString(), true);
                    AppsLAFManager.Instance().AutoFocusOnOff(LAFName.Akkon.ToString(), true);
                    AppsLineCameraManager.Instance().StartGrab(CameraName.LinscanMIL0);

                    SeqStep = SeqStep.SEQ_WAITING_SCAN_COMPLETED;
                    break;

                case SeqStep.SEQ_WAITING_SCAN_COMPLETED:

                    if (!AppsLineCameraManager.Instance().IsGrabCompleted())
                        break;

                    AppsLineCameraManager.Instance().StopGrab(CameraName.LinscanMIL0);

                    SeqStep = SeqStep.SEQ_PATTERN_MATCH;
                    break;

                case SeqStep.SEQ_PATTERN_MATCH:

                    var fpcParam = tab.FpcMarkParamList;
                    var panelParam = tab.PanelMarkParamList;

                    var fpcMark = AlgorithmTool.RunPatternMatch(cogImage, fpcParam[0].InspParam);
                    var panelMark = AlgorithmTool.RunPatternMatch(cogImage, panelParam[0].InspParam);

                    if (fpcMark != null && panelMark != null)
                    {
                        if (fpcMark.Result == Result.OK)
                        {
                            message = "FPC Mark Search OK - Score : " + fpcMark.MaxScore.ToString() + " / T/T : " + fpcMark.TactTime.ToString();
                            Logger.Write(LogType.Seq, message, DateTime.Now);

                            SetFpcMarkPatternMatchResult(fpcMark);
                        }
                        else if (fpcMark.Result == Result.NG)
                        {
                            message = "FPC Mark Search NG - Score : " + fpcMark.MaxScore.ToString() + " / T/T : " + fpcMark.TactTime.ToString();
                            Logger.Write(LogType.Seq, message, DateTime.Now);
                        }
                        else if (fpcMark.Result == Result.Fail)
                        {
                            message = "FPC Mark Search FAIL";
                            Logger.Write(LogType.Seq, message, DateTime.Now);
                        }
                        else { }

                        if (panelMark.Result == Result.OK)
                        {
                            message = "Panel Mark Search - Score : " + panelMark.MaxScore.ToString() + " / T/T : " + panelMark.TactTime.ToString();
                            Logger.Write(LogType.Seq, message, DateTime.Now);

                            SetPanelMarkPatternMatchResult(panelMark);
                        }
                        else if (panelMark.Result == Result.NG)
                        {
                            message = "Panel Mark Search NG - Score : " + panelMark.MaxScore.ToString() + " / T/T : " + panelMark.TactTime.ToString();
                            Logger.Write(LogType.Seq, message, DateTime.Now);
                        }
                        else if (panelMark.Result == Result.Fail)
                        {
                            message = "Panel Mark Search FAIL";
                            Logger.Write(LogType.Seq, message, DateTime.Now);
                        }
                        else { }

                        // Insp result == null 일 때 모션 초기 위치 이동이 필요함
                        SeqStep = SeqStep.SEQ_IDLE;
                    }

                    SeqStep = SeqStep.SEQ_ALIGN_INSPECTION;
                    break;

                case SeqStep.SEQ_ALIGN_INSPECTION:

                    var alignParam = tab.AlignParamList;

                    var alignResultX = AlgorithmTool.RunAlignX(cogImage, alignParam[0].CaliperParams, alignParam[0].LeadCount);
                    var alignResultY = AlgorithmTool.RunAlignY(cogImage, alignParam[0].CaliperParams);

                    if (alignResultX.Result == Result.OK)
                    {
                        message = "Align X Search OK - Result : " + alignResultX.Result.ToString() + " / T/T : " + alignResultX.CogAlignResult[0].TactTime.ToString();
                        Logger.Write(LogType.Seq, message, DateTime.Now);

                        SetAlignXResult(alignResultX);
                    }
                    else if (alignResultY.Result == Result.Fail)
                    {
                        message = "Align X Search FAIL";
                        Logger.Write(LogType.Seq, message, DateTime.Now);
                    }
                    else { }

                    if (alignResultY.Result == Result.OK)
                    {
                        message = "Align Y Search OK - Result : " + alignResultY.Result.ToString() + " / T/T : " + alignResultY.CogAlignResult[0].TactTime.ToString();
                        Logger.Write(LogType.Seq, message, DateTime.Now);

                        SetAlignYResult(alignResultY);
                    }
                    else if (alignResultY.Result == Result.Fail)
                    {
                        message = "Align Y Search FAIL";
                        Logger.Write(LogType.Seq, message, DateTime.Now);
                    }
                    else { }

                    SeqStep = SeqStep.SEQ_ALIGN_INSPECTION_COMPLETED;
                    break;

                case SeqStep.SEQ_ALIGN_INSPECTION_COMPLETED:

                    SeqStep = SeqStep.SEQ_AKKON_INSPECTION;
                    break;

                case SeqStep.SEQ_AKKON_INSPECTION:

                    Mat mat = null;
                    var akkonParam = tab.AkkonParam;
                    int stageNo = 0, tabNo = 0;

                    var akkonResult = AlgorithmTool.RunAkkon(mat, akkonParam, stageNo, tabNo);

                    if (akkonResult != null)
                        SetAkkonResult(akkonResult);
                    else
                        SeqStep = SeqStep.SEQ_IDLE;


                    SeqStep = SeqStep.SEQ_AKKON_INSPECTION_COMPLETED;
                    break;

                case SeqStep.SEQ_AKKON_INSPECTION_COMPLETED:

                    SeqStep = SeqStep.SEQ_UI_RESULT_UPDATE;
                    break;

                case SeqStep.SEQ_UI_RESULT_UPDATE:

                    //if (true)
                    //    break;
                    
                    AppsLAFManager.Instance().AutoFocusOnOff(LAFName.Align.ToString(), false);
                    AppsLAFManager.Instance().AutoFocusOnOff(LAFName.Akkon.ToString(), false);
                    AppsMotionManager.Instance().MoveTo(UnitName.Unit0, AxisHandlerName.Handler0, AxisName.X, TeachingPositionType.Standby);

                    SeqStep = SeqStep.SEQ_SAVE_IMAGE;
                    break;

                case SeqStep.SEQ_SAVE_IMAGE:

                    SeqStep = SeqStep.SEQ_DELETE_DATA;
                    break;

                case SeqStep.SEQ_DELETE_DATA:

                    Dispose();
                    SeqStep = SeqStep.SEQ_CHECK_STANDBY;
                    break;

                case SeqStep.SEQ_CHECK_STANDBY:

                    if (!AppsMotionManager.Instance().IsMovingCompleted(UnitName.Unit0, AxisHandlerName.Handler0, AxisName.X, TeachingPositionType.Standby))
                        break;

                    SeqStep = SeqStep.SEQ_IDLE;
                    break;

                default:
                    break;
            }
        }

        private void TabInspectionSeq()
        {

        }

        private CogPatternMatchingResult PanelMarkPatternMatchResult = null;
        private void SetPanelMarkPatternMatchResult(CogPatternMatchingResult result)
        {
            PanelMarkPatternMatchResult = new CogPatternMatchingResult();
            PanelMarkPatternMatchResult = result;
        }

        private CogPatternMatchingResult GetPanelMarkPatternMatchResult()
        {
            return PanelMarkPatternMatchResult;
        }

        private CogPatternMatchingResult FpcMarkPatternMatchResult = null;
        private void SetFpcMarkPatternMatchResult(CogPatternMatchingResult result)
        {
            FpcMarkPatternMatchResult = new CogPatternMatchingResult();
            FpcMarkPatternMatchResult = result;
        }

        private CogPatternMatchingResult GetFpcMarkPatternMatchResult()
        {
            return FpcMarkPatternMatchResult;
        }

        private CogAlignCaliperResult AlignXResult = null;
        private void SetAlignXResult(CogAlignCaliperResult result)
        {
            AlignXResult = new CogAlignCaliperResult();
            AlignXResult = result;
        }

        private CogAlignCaliperResult GetAlignXResult()
        {
            return AlignXResult;
        }

        private CogAlignCaliperResult AlignYResult = null;
        private void SetAlignYResult(CogAlignCaliperResult result)
        {
            AlignYResult = new CogAlignCaliperResult();
            AlignYResult = result;
        }

        private CogAlignCaliperResult GetAlignYResult()
        {
            return AlignXResult;
        }

        private List<AkkonResult> AkkonResultList = null;

        private void SetAkkonResult(List<AkkonResult> result)
        {
            AkkonResultList.Clear();
            AkkonResultList = new List<AkkonResult>();
            AkkonResultList.AddRange(result);
        }

        private List<AkkonResult> GetAkkonResult()
        {
            return AkkonResultList;
        }

        private void Dispose()
        {
            PanelMarkPatternMatchResult = null;
            FpcMarkPatternMatchResult = null;

            AlignXResult = null;
            AlignYResult = null;

            if (AkkonResultList != null)
                AkkonResultList.Clear();
        }
    }

    public enum SeqStep
    {
        SEQ_IDLE,
        SEQ_START,
        SEQ_READY,
        SEQ_WAITING,
        SEQ_SCAN_START,
        SEQ_WAITING_SCAN_COMPLETED,
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
