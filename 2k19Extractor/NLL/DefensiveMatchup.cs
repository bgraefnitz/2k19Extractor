using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2k19Extractor.NLL
{
    class DefensiveMatchup
    {
        public string DefendingPlayer { get; set; }
        public string OpposingPlayer { get; set; }
        public int OnBallPressure { get; set; }
        public int OffBallPressure { get; set; }
        public int ForceDirection { get; set; }
        public int SwitchRules { get; set; }
        public int PreRotate { get; set; }
        public int ScreenHelp { get; set; }
        public int DriveHelp { get; set; }
        public int Post { get; set; }
        public int DoublePerimeter { get; set; }
        public int DoublePost { get; set; }
        public int OnBallScreen { get; set; }
        public int OnBallScreenCenter { get; set; }
        public int OffBallScreen { get; set; }
        public int OffBallScreenCenter { get; set; }
        public int Hedge { get; set; }
        public int StayAttached { get; set; }
        public int HedgeCenter { get; set; }
        public int ExtendPressure { get; set; }
    }
}
