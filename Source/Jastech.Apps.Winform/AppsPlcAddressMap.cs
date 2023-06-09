using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jastech.Apps.Winform
{
    public class AppsPlcAddressMap
    {
        public AddressMapType AddressType { get; private set; }

        public WordType WordType { get; private set; }

        public int AddressNum { get; set; }

        public int WordSize { get; set; }

        public AppsPlcAddressMap(AddressMapType type, WordType wordType, int addressNum, int wordSize)
        {
            AddressType = type;
            WordType = wordType;
            AddressNum = addressNum;
            WordSize = wordSize;
        }
    }
    public enum AddressMapType
    {
        // Read
        VisionStatus,                   // 1 : Ready
        MotionStatus,                   // 1 : Ready, 2: Stop
        Alive,
        ErrorCode,
        RequestAxisZ,                   // 1 : ServoOn(#1), 2: ServoReset(#1), 
                                        // 3 : ServoOn(#1), 4: ServoReset(#1)

        MachineProcessComplete,         // 1 : EQP Mode & Reciepe Data Change Complete, 
                                        // 2 : Time Change Complete,
                                        // 3 : PPID Changed Complete,
                                        // 4 : PPID Copy Complete,
                                        // 5 : PPID Delete Complete,

        MotionMoveComplete,             // 1 : Motion Origin Complete.
                                        // 2 : XZ-Axis Standby Position Move Complete,
                                        // 3 : XZ-Axis Stage Left Align Position Move Complete,
                                        // 4 : XZ-Axis Stage Right Align Position Move Complete,
                                        // 5 : XZ-Axis Stage Scan Start Position Move Complete,

        Calibration_Move_Request,       // 1: Stage Move Request
        Calibration_PreAlignComplete,   // 1 : Stage PreAlign Calibration Complete
        Calibration_Move_LowX,          // mm
        Calibration_Move_HighX,         // mm
        Calibration_Move_LowY,          // mm
        Calibration_Move_HighY,         // mm
        Calibration_Move_LowT,          // mm
        Calibration_Move_HighT,         // mm

        PreAlignComplete,              // 1 : PreAlign Complete
        Inspection_Start_Complete,


    }

    public enum WordType
    {
        DEC, // 10
        HEX, // 16
    }
}
