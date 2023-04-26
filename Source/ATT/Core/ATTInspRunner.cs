using Jastech.Apps.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATT.Core
{
    public class ATTInspRunner
    {
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
            //var cancellationToken = SeqTaskCancellationTokenSource.Token;
            //cancellationToken.ThrowIfCancellationRequested();
            //SeqStep = SeqStep.SEQ_START;
            ////카메라 그랩 시작 
            //while (true)
            //{
            //    // 작업 취소
            //    if (cancellationToken.IsCancellationRequested)
            //    {
            //        SeqStep = SeqStep.SEQ_IDIE;
            //        InspDeviceService.LightTurnOff();
            //        InspDeviceService.CameraMutiGrab(false);
            //        break;
            //    }
            //    if (CurrentInspState == InspState.Idle)
            //    {
            //        SeqStep = SeqStep.SEQ_IDIE_WAITING;
            //    }
            //    SeqTaskLoop();
            //}
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
        SEQ_ALING_INSPECTION,
        SEQ_ALING_INSPECTION_COMPLETED,
        SEQ_AKKON_INSPECTION,
        SEQ_AKKON_INSPECTION_COMPLETED,
        SEQ_UI_RESULT_UPDATE,
        SEQ_SAVE_IMAGE,
        SEQ_DELETE_DATA,
    }
}
