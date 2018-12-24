using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace _2k19Extractor
{
    [Serializable] 
    public partial class Lineup
    {
        public int HomeAway;
        public string Desc;
        public int PG;
        public int SG;
        public int SF;
        public int PF;
        public int C;

        public List<CheckinCheckout> InOut = new List<CheckinCheckout>();

        public bool Active;

        //All of the properties to hold stats
        public Single TimeInGame
        {
            get { return InOut.Sum(x=>x.TimeInGame); }
        }

        public int Points
        {
            get { return InOut.Sum(x => x.Points); }
        }

        public int DefRebounds
        {
            get { return InOut.Sum(x => x.DefRebounds); }
        }

        public int Assists
        {
            get { return InOut.Sum(x => x.Assists); }
        }

        public int Steals
        {
            get { return InOut.Sum(x => x.Steals); }
        }

        public int Blocks
        {
            get { return InOut.Sum(x => x.Blocks); }
        }

        public int Turnovers
        {
            get { return InOut.Sum(x => x.Turnovers); }
        }

        public int FGM
        {
            get { return InOut.Sum(x => x.FGM); }
        }

        public int FGA
        {
            get { return InOut.Sum(x => x.FGA); }
        }

        public int ThreePM
        {
            get { return InOut.Sum(x => x.ThreePM); }
        }

        public int ThreePA
        {
            get { return InOut.Sum(x => x.ThreePA); }
        }

        public int FTM
        {
            get { return InOut.Sum(x => x.FTM); }
        }

        public int FTA
        {
            get { return InOut.Sum(x => x.FTA); }
        }

        public int OffRebounds
        {
            get { return InOut.Sum(x => x.OffRebounds); }
        }

        public int Fouls
        {
            get { return InOut.Sum(x => x.Fouls); }
        }
        public int PointsInPaint
        {
            get { return InOut.Sum(x => x.PointsInPaint); }
        }
        public int SecondChancePoints
        {
            get { return InOut.Sum(x => x.SecondChancePoints); }
        }
        public int FastBreakPoints
        {
            get { return InOut.Sum(x => x.FastBreakPoints); }
        }
        public int PointsOffTurnovers
        {
            get { return InOut.Sum(x => x.PointsOffTurnovers); }
        }
        public int Dunks
        {
            get { return InOut.Sum(x => x.Dunks); }
        }

        public int Rebounds
        {
            get { return InOut.Sum(x => x.Rebounds); }
        }

        public int PlusMinus
        {
            get { return InOut.Sum(x => x.Points) - InOut.Sum(x => x.OppPoints); }
        }

        public Int64 OppPoints
        {
            get { return InOut.Sum(x => x.OppPoints); }
        }
        public Int64 OppDefRebounds
        {
            get { return InOut.Sum(x => x.OppDefRebounds); }
        }
        public Int64 OppAssists
        {
            get { return InOut.Sum(x => x.OppAssists); }
        }
        public Int64 OppSteals
        {
            get { return InOut.Sum(x => x.OppSteals); }
        }
        public Int64 OppBlocks
        {
            get { return InOut.Sum(x => x.OppBlocks); }
        }
        public Int64 OppTurnovers
        {
            get { return InOut.Sum(x => x.OppTurnovers); }
        }
        public Int64 OppFGM
        {
            get { return InOut.Sum(x => x.OppFGM); }
        }
        public Int64 OppFGA
        {
            get { return InOut.Sum(x => x.OppFGA); }
        }
        public Int64 OppThreePM
        {
            get { return InOut.Sum(x => x.OppThreePM); }
        }
        public Int64 OppThreePA
        {
            get { return InOut.Sum(x => x.OppThreePA); }
        }
        public Int64 OppFTM
        {
            get { return InOut.Sum(x => x.OppFTM); }
        }
        public Int64 OppFTA
        {
            get { return InOut.Sum(x => x.OppFTA); }
        }
        public Int64 OppOffRebounds
        {
            get { return InOut.Sum(x => x.OppOffRebounds); }
        }
        public Int64 OppFouls
        {
            get { return InOut.Sum(x => x.OppFouls); }
        }
        public Int64 OppPointsInPaint
        {
            get { return InOut.Sum(x => x.OppPointsInPaint); }
        }
        public Int64 OppSecondChancePoints
        {
            get { return InOut.Sum(x => x.OppSecondChancePoints); }
        }
        public Int64 OppFastBreakPoints
        {
            get { return InOut.Sum(x => x.OppFastBreakPoints); }
        }
        public Int64 OppPointsOffTurnovers
        {
            get { return InOut.Sum(x => x.OppPointsOffTurnovers); }
        }
        public Int64 OppDunks
        {
            get { return InOut.Sum(x => x.OppDunks); }
        }

        public int OppRebounds
        {
            get { return InOut.Sum(x => x.OppRebounds); }
        }

        public int OppPlusMinus
        {
            get { return InOut.Sum(x => x.OppPoints) - InOut.Sum(x => x.Points); }
        }

        public Lineup(int pg, int sg, int sf, int pf, int c, GameSituation checkInGame, string desc, int homeAway)
        {
            HomeAway = homeAway;
            Desc = desc;
            PG = pg;
            SG = sg;
            SF = sf;
            PF = pf;
            C = c;
            Active = true;
            InOut.Add(new CheckinCheckout(this.HomeAway, checkInGame));
        }

        public void CheckIn(int team, GameSituation game)
        {
            InOut.Add(new CheckinCheckout(team, game));
            Active = true;
        }

        public void Playing(int team, GameSituation game)
        {
            //if within the same quarter just extend the current check in. if not, check that one out and create a new one
            if (InOut.Last().CheckInGame.CurrentQuarter == game.CurrentQuarter)
            {
                if (game.SecondsRemaining > InOut.Last().CheckInGame.SecondsRemaining)
                    game.SecondsRemaining = 0;
                InOut.Last().CheckOutGame = game;
                Active = true;
            }
            else
            {
                CheckIn(team, game);
                Active = true;
            }
        }

        public int Appearances
        {
            get { return InOut.Count; }
        }

        private Single SecondsPlayed
        {
            get
            {
                { return InOut.Sum(x => x.TimeInGame); }
            }
        }
        public int MinutesPlayed
        {
            get
            {
                float secondsPlayed = this.SecondsPlayed;
                if (secondsPlayed < 60)
                    secondsPlayed = 60;
                return Convert.ToInt32(Math.Round(secondsPlayed / 60));
            }
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
            var pm = PlusMinus.ToString(CultureInfo.InvariantCulture);
            var pip = PointsInPaint.ToString(CultureInfo.InvariantCulture);
            var secChP = SecondChancePoints.ToString(CultureInfo.InvariantCulture);
            var fbPts = FastBreakPoints.ToString(CultureInfo.InvariantCulture);
            var ptsTO = PointsOffTurnovers.ToString(CultureInfo.InvariantCulture);
            var dunks = Dunks.ToString(CultureInfo.InvariantCulture);
            var app = Appearances.ToString(CultureInfo.InvariantCulture);

            string stats = string.Format(formatString, Desc, min, fg, threeP, ft, pts, oreb, dreb, reb, ast, stl, blk, to, pf, pm, pts, pip, secChP, fbPts, ptsTO, dunks, app);

            return stats;
        }

        public string OppTotalsToString(string formatString)
        {
            var min = MinutesPlayed.ToString(CultureInfo.InvariantCulture);
            var fg = OppFGM.ToString(CultureInfo.InvariantCulture) + "-" + OppFGA.ToString(CultureInfo.InvariantCulture);
            var threeP = OppThreePM.ToString(CultureInfo.InvariantCulture) + "-" + OppThreePA.ToString(CultureInfo.InvariantCulture);
            var ft = OppFTM.ToString(CultureInfo.InvariantCulture) + "-" + OppFTA.ToString(CultureInfo.InvariantCulture);
            var pts = OppPoints.ToString(CultureInfo.InvariantCulture);
            var oreb = OppOffRebounds.ToString(CultureInfo.InvariantCulture);
            var dreb = OppDefRebounds.ToString(CultureInfo.InvariantCulture);
            var reb = OppRebounds.ToString(CultureInfo.InvariantCulture);
            var ast = OppAssists.ToString(CultureInfo.InvariantCulture);
            var stl = OppSteals.ToString(CultureInfo.InvariantCulture);
            var blk = OppBlocks.ToString(CultureInfo.InvariantCulture);
            var to = OppTurnovers.ToString(CultureInfo.InvariantCulture);
            var pf = OppFouls.ToString(CultureInfo.InvariantCulture);
            var pm = OppPlusMinus.ToString(CultureInfo.InvariantCulture);
            var pip = OppPointsInPaint.ToString(CultureInfo.InvariantCulture);
            var secChP = OppSecondChancePoints.ToString(CultureInfo.InvariantCulture);
            var fbPts = OppFastBreakPoints.ToString(CultureInfo.InvariantCulture);
            var ptsTO = OppPointsOffTurnovers.ToString(CultureInfo.InvariantCulture);
            var dunks = OppDunks.ToString(CultureInfo.InvariantCulture);
            var app = Appearances.ToString(CultureInfo.InvariantCulture);

            string stats = string.Format(formatString, ">>Opponent", min, fg, threeP, ft, pts, oreb, dreb, reb, ast, stl, blk, to, pf, pm, pts, pip, secChP, fbPts, ptsTO, dunks, app);

            return stats;
        }
    }
}
