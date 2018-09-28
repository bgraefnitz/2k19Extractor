using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace _2k19Extractor
{
    [Serializable] 
    public class Player
    {
        //starting center = 1, starting pg = 5, sixth man = 6, etc...
        public int DepthChartPos;
        //this is the base level static pointer for this position
        public Int64 StatsPointer;
        //this is the dynamic address of the player (used to check other pointers against - like for who is in the game)
        public Int64 DynamicPlayerPointer;
        //this is the static pointer for the last name
        public readonly Int64 LastNamePointer;

        //property for if the player is currently in the game
        public string InGame;

        //property for if the player is the player of the game
        public bool PlayerOfTheGame;

        //Properties to hold names
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //Property to get full name is calculated to avoid having to set both
        public string FullName
        {
            get 
            {
                if (FirstName == "")
                    return LastName;
                if (LastName == "")
                    return FirstName;
                return FirstName + " " + LastName; 
            }
        }

        public string ToString()
        {
            return FullName;
        }

        //All of the properties to hold stats
        public float SecondsPlayed { get; set; }
        public int Points { get; set; }
        public int DefRebounds { get; set; }
        public int Assists { get; set; }
        public int Steals { get; set; }
        public int Blocks { get; set; }
        public int Turnovers { get; set; }
        public int TwoPM { get; set; }
        public int TwoPA { get; set; }
        public int ThreePM { get; set; }
        public int ThreePA { get; set; }
        public int FTM { get; set; }
        public int FTA { get; set; }
        public int OffRebounds { get; set; }
        public int Fouls { get; set; }
        public int PlusMinus { get; set; }
        public int PointsAssisted { get; set; }
        public int PointsInPaint { get; set; }
        public int SecondChancePoints { get; set; }
        public int FastBreakPoints { get; set; }
        public int PointsOffTurnovers { get; set; }
        public int Dunks { get; set; }

        public bool Possession { get; set; }

        public List<Touch> TouchList;

        public int Touches
        {
            get { return TouchList.Count; }
        }

        public Single TouchTime
        {
            get { return TouchList.Sum(x => x.TimeOfTouch); }
        }

        public void GotBall(int quarter, Single timeRemaining)
        {
            TouchList.Add(new Touch(quarter, timeRemaining));
            TouchList.Last().EndTime = timeRemaining;
        }

        public void HasBall(Single endTime)
        {
            if (endTime > TouchList.Last().StartTime)
                endTime = 0;
            TouchList.Last().EndTime = endTime;
        }

        //All of the calculated properties for the stats that we don't get straight from memory
        public int MinutesPlayed 
        {
            get
            {
                float secondsPlayed = this.SecondsPlayed;
                if (secondsPlayed > 0 && secondsPlayed < 60)
                    secondsPlayed = 60;
                return Convert.ToInt32(Math.Round(secondsPlayed/60));
            }
        }
        public int FGM
        {
            get { return TwoPM + ThreePM; }
        }
        public int FGA
        {
            get { return TwoPA + ThreePA; }
        }
        public int Rebounds
        {
            get { return OffRebounds + DefRebounds; }
        }
        public int PointsResponsibleFor
        {
            get { return Points + PointsAssisted; }
        }
        public int FantasyPoints
        {
            get { return Points + Rebounds + Assists + Steals + Blocks - Fouls - (FGA - FGM) - (FTA - FTM) - Turnovers; }
        }

        //Constructor to initialize 
        //the pointer provided is that of the player's last name from the depth chart and the player stats pointer is derived from that
        public Player(int depthChartPos, Int64 pointer)
        {
            DepthChartPos = depthChartPos;
            LastNamePointer = pointer;
            StatsPointer = pointer;
            InGame = "";
            TouchList = new List<Touch>();
        }

        public void OnFloor(int position)
        {
            switch (position)
            {
                case 1:
                    InGame = "*PG*-";
                    break;
                case 2:
                    InGame = "*SG*-";
                    break;
                case 3:
                    InGame = "*SF*-";
                    break;
                case 4:
                    InGame = "*PF*-";
                    break;
                case 5:
                    InGame = "*C*-";
                    break;
                default:
                    InGame = "";
                    break;
            }
        }

        public string ToString(string formatString, string gameTime)
        {
            //Concatenate the in game prefix (should show *SG* if in at SG) prior to the name
            var name = FullName;
            if (gameTime != "FINAL")
                name = InGame + name;
            var min = MinutesPlayed.ToString(CultureInfo.InvariantCulture);
            var fg = FGM.ToString(CultureInfo.InvariantCulture) + "-" + FGA.ToString(CultureInfo.InvariantCulture);
            var threeP = ThreePM.ToString(CultureInfo.InvariantCulture) + "-" + ThreePA.ToString(CultureInfo.InvariantCulture);
            var ft = FTM.ToString(CultureInfo.InvariantCulture) + "-" + FTA.ToString(CultureInfo.InvariantCulture);
            var pts = Points.ToString(CultureInfo.InvariantCulture);
            var oreb = OffRebounds.ToString(CultureInfo.InvariantCulture);
            var dreb = DefRebounds.ToString(CultureInfo.InvariantCulture);
            var reb = Rebounds.ToString(CultureInfo.InvariantCulture);
            var ast = Assists.ToString(CultureInfo.InvariantCulture);
            var stl = Steals.ToString(CultureInfo.InvariantCulture);
            var blk = Blocks.ToString(CultureInfo.InvariantCulture);
            var to = Turnovers.ToString(CultureInfo.InvariantCulture);
            var pf = Fouls.ToString(CultureInfo.InvariantCulture);
            var pm = PlusMinus.ToString(CultureInfo.InvariantCulture);
            var prf = PointsResponsibleFor.ToString(CultureInfo.InvariantCulture);
            var pip = PointsInPaint.ToString(CultureInfo.InvariantCulture);
            var secChP = SecondChancePoints.ToString(CultureInfo.InvariantCulture);
            var fbPts = FastBreakPoints.ToString(CultureInfo.InvariantCulture);
            var ptsTO = PointsOffTurnovers.ToString(CultureInfo.InvariantCulture);
            var dunks = Dunks.ToString(CultureInfo.InvariantCulture);
            var touches = Touches.ToString(CultureInfo.InvariantCulture);
            var touchTime = Math.Round(TouchTime,0,MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture);

            var stats = string.Format(formatString, name, min, fg, threeP, ft, pts, oreb, dreb, reb, ast, stl, blk, to, pf, pm, prf,pip,secChP,fbPts,ptsTO,dunks,touches,touchTime);

            return stats;
        }
    }
}
