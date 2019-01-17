using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace _2k19Extractor
{
    [Serializable] 
    public class Team
    {
        public string HomeAway;
        public string Name;
        public Int64 NumPlayersPointer;
        public Int64 BasePlayerPointer;
        public Int64 FinalScorePointer;
        public Int64 DefensiveSettingsPointer;
        public Int64 NamePointer;
        public Int64 PlayersOnFloorPointer;
        public int NumPlayers;
        public List<Player> Players;
        public Score Score;
        public List<Lineup> Lineups;

        public Lineup ActiveLineup
        {
            get { return Lineups.Find(x => x.Active); }
        }
                
        //Team statistic parameters derived from player stats
        public bool Possession
        {
            get
            {
                //if any of the players on this team have the ball, then the team has the ball
                return Players.Exists(x => x.Possession == true);
            }
        }
        public int Points
        {
            get { return Players.Sum(x => x.Points); }
        }
        public int DefRebounds
        {
            get { return Players.Sum(x => x.DefRebounds); }
        }
        public int Assists
        {
            get { return Players.Sum(x => x.Assists); }
        }
        public int Steals
        {
            get { return Players.Sum(x => x.Steals); }
        }
        public int Blocks
        {
            get { return Players.Sum(x => x.Blocks); }
        }
        public int Turnovers
        {
            get { return Players.Sum(x => x.Turnovers); }
        }
        public int MinutesPlayed
        {
            get { return Players.Sum(x => x.MinutesPlayed); }
        }
        public int FGM
        {
            get { return Players.Sum(x => x.FGM); }
        }
        public int FGA
        {
            get { return Players.Sum(x => x.FGA); }
        }
        public decimal FGPercent
        {
            get
            {
                if (FGA == 0)
                    return 0;
                return (decimal)FGM / (decimal)FGA;
            }
        }
        public int Rebounds
        {
            get { return Players.Sum(x => x.Rebounds); }
        }
        public int ThreePM
        {
            get { return Players.Sum(x => x.ThreePM); }
        }
        public int ThreePA
        {
            get { return Players.Sum(x => x.ThreePA); }
        }
        public decimal ThreePercent
        {
            get
            {
                if (ThreePA == 0)
                    return 0; 
                return (decimal)ThreePM / (decimal)ThreePA;
            }
        }
        public int FTM
        {
            get { return Players.Sum(x => x.FTM); }
        }
        public int FTA
        {
            get { return Players.Sum(x => x.FTA); }
        }
        public decimal FTPercent
        {
            get
            {
                if (FTA == 0)
                    return 0; 
                return (decimal)FTM / (decimal)FTA;
            }
        }
        public int OffRebounds
        {
            get { return Players.Sum(x => x.OffRebounds); }
        }
        public int Fouls
        {
            get { return Players.Sum(x => x.Fouls); }
        }
        public int PointsInPaint
        {
            get { return Players.Sum(x => x.PointsInPaint); }
        }
        public int SecondChancePoints
        {
            get { return Players.Sum(x => x.SecondChancePoints); }
        }
        public int FastBreakPoints
        {
            get { return Players.Sum(x => x.FastBreakPoints); }
        }
        public int PointsOffTurnovers
        {
            get { return Players.Sum(x => x.PointsOffTurnovers); }
        }
        public int PointsAssisted
        {
            get { return Players.Sum(x => x.PointsAssisted); }
        }
        public int Dunks
        {
            get { return Players.Sum(x => x.Dunks); }
        }


        public Team(string homeAway, Int64 finalScorePointer, Int64 playersOnFloorPointer, Int64 namePointer, Int64 numPlayersPointer, Int64 basePlayerPointer, Int64 defensiveSettingsPointer)
        {
            HomeAway = homeAway;
            FinalScorePointer = finalScorePointer;
            PlayersOnFloorPointer = playersOnFloorPointer;
            NamePointer = namePointer;
            NumPlayersPointer = numPlayersPointer;
            BasePlayerPointer = basePlayerPointer;
            DefensiveSettingsPointer = defensiveSettingsPointer;
            Players = new List<Player>();
            Score = new Score();
            Lineups = new List<Lineup>();
        }

        public string ScoreToString(string formatString, string gameTime)
        {
            var name = Name;
            if (Possession && gameTime != "FINAL")
                name = ">" + name;
            var q1 = Score.Q1.ToString(CultureInfo.InvariantCulture);
            var q2 = Score.Q2.ToString(CultureInfo.InvariantCulture);
            var q3 = Score.Q3.ToString(CultureInfo.InvariantCulture);
            var q4 = Score.Q4.ToString(CultureInfo.InvariantCulture);
            var ot = Score.OT.ToString(CultureInfo.InvariantCulture);
            var final = Score.Final.ToString(CultureInfo.InvariantCulture);
            
            string stats = string.Format(formatString, name, q1, q2, q3, q4, ot, final, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
            return stats;
        }
        
        public string TotalsToString(string formatString)
        {
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
            var pip = PointsInPaint.ToString(CultureInfo.InvariantCulture);
            var secChP = SecondChancePoints.ToString(CultureInfo.InvariantCulture);
            var fbPts = FastBreakPoints.ToString(CultureInfo.InvariantCulture);
            var ptsTO = PointsOffTurnovers.ToString(CultureInfo.InvariantCulture);
            var dunks = Dunks.ToString(CultureInfo.InvariantCulture);

            string stats = string.Format(formatString, "Totals", min, fg, threeP, ft, pts, oreb, dreb, reb, ast, stl, blk, to, pf, "", "", pip, secChP, fbPts, ptsTO, dunks, "", "");

            return stats;
        }

        public string PercentagesToString(string formatString)
        {
            var fg = Math.Round(FGPercent, 3).ToString(CultureInfo.InvariantCulture);
            var threeP = Math.Round(ThreePercent, 3).ToString(CultureInfo.InvariantCulture);
            var ft = Math.Round(FTPercent, 3).ToString(CultureInfo.InvariantCulture);

            //opted to send blank strings to the same formatted string as the rest of the box score instead of having a different format string for just the percentages
            string stats = string.Format(formatString, "", "", fg, threeP, ft, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");

            return stats;
        }
    }
}
