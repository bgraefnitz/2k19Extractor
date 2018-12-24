using System;

namespace _2k19Extractor
{
    public partial class Lineup
    {
        [Serializable] 
        public class CheckinCheckout
        {
            public GameSituation CheckInGame;
            public GameSituation CheckOutGame;
            private int ThisTeam;

            private int Opponent
            {
                get {return ThisTeam == 0 ? 1 : 0;}
            }

            public CheckinCheckout(int team, GameSituation checkInGame)
            {
                CheckInGame = checkInGame;
                CheckOutGame = checkInGame;
                ThisTeam = team;
            }

            public Single TimeInGame
            {
                get { return CheckInGame.SecondsRemaining - CheckOutGame.SecondsRemaining; }
            }

            public int Points
            {
                get { return CheckOutGame.Team.Points - CheckInGame.Team.Points; }
            }

            public int DefRebounds
            {
                get { return CheckOutGame.Team.DefRebounds - CheckInGame.Team.DefRebounds; }
            }

            public int Assists
            {
                get { return CheckOutGame.Team.Assists - CheckInGame.Team.Assists; }
            }

            public int Steals
            {
                get {return CheckOutGame.Team.Steals - CheckInGame.Team.Steals;}
            }

            public int Blocks
            {
                get { return CheckOutGame.Team.Blocks - CheckInGame.Team.Blocks; }
            }

            public int Turnovers
            {
                get { return CheckOutGame.Team.Turnovers - CheckInGame.Team.Turnovers; }
            }

            public int FGM
            {
                get { return CheckOutGame.Team.FGM - CheckInGame.Team.FGM; }
            }

            public int FGA
            {
                get { return CheckOutGame.Team.FGA - CheckInGame.Team.FGA; }
            }

            public int ThreePM
            {
                get { return CheckOutGame.Team.ThreePM - CheckInGame.Team.ThreePM; }
            }

            public int ThreePA
            {
                get { return CheckOutGame.Team.ThreePA - CheckInGame.Team.ThreePA; }
            }

            public int FTM
            {
                get { return CheckOutGame.Team.FTM - CheckInGame.Team.FTM; }
            }

            public int FTA
            {
                get { return CheckOutGame.Team.FTA - CheckInGame.Team.FTA; }
            }

            public int OffRebounds
            {
                get { return CheckOutGame.Team.OffRebounds - CheckInGame.Team.OffRebounds; }
            }

            public int Fouls 
            {
                get { return CheckOutGame.Team.Fouls - CheckInGame.Team.Fouls; }
            }
            public int PointsInPaint 
            {
                get { return CheckOutGame.Team.PointsInPaint - CheckInGame.Team.PointsInPaint; }
            }
            public int SecondChancePoints 
            {
                get { return CheckOutGame.Team.SecondChancePoints - CheckInGame.Team.SecondChancePoints; }
            }
            public int FastBreakPoints 
            {
                get { return CheckOutGame.Team.FastBreakPoints - CheckInGame.Team.FastBreakPoints; }
            }
            public int PointsOffTurnovers 
            {
                get { return CheckOutGame.Team.PointsOffTurnovers - CheckInGame.Team.PointsOffTurnovers; }
            }
            public int Dunks
            {
                get { return CheckOutGame.Team.Dunks - CheckInGame.Team.Dunks; }
            }

            public int Rebounds
            {
                get { return OffRebounds + DefRebounds; }
            }

            public int OppPoints
            {
                get { return CheckOutGame.OpposingTeam.Points - CheckInGame.OpposingTeam.Points; }
            }
            public int OppDefRebounds
            {
                get { return CheckOutGame.OpposingTeam.DefRebounds - CheckInGame.OpposingTeam.DefRebounds; }
            }
            public int OppAssists
            {
                get { return CheckOutGame.OpposingTeam.Assists - CheckInGame.OpposingTeam.Assists; }
            }
            public int OppSteals
            {
                get { return CheckOutGame.OpposingTeam.Steals - CheckInGame.OpposingTeam.Steals; }
            }
            public int OppBlocks
            {
                get { return CheckOutGame.OpposingTeam.Blocks - CheckInGame.OpposingTeam.Blocks; }
            }
            public int OppTurnovers
            {
                get { return CheckOutGame.OpposingTeam.Turnovers - CheckInGame.OpposingTeam.Turnovers; }
            }
            public int OppFGM
            {
                get { return CheckOutGame.OpposingTeam.FGM - CheckInGame.OpposingTeam.FGM; }
            }
            public int OppFGA
            {
                get { return CheckOutGame.OpposingTeam.FGA - CheckInGame.OpposingTeam.FGA; }
            }
            public int OppThreePM 
            {
                get { return CheckOutGame.OpposingTeam.ThreePM - CheckInGame.OpposingTeam.ThreePM; }
            }
            public int OppThreePA  
            {
                get { return CheckOutGame.OpposingTeam.ThreePA - CheckInGame.OpposingTeam.ThreePA; }
            }
            public int OppFTM
            {
                get { return CheckOutGame.OpposingTeam.FTM - CheckInGame.OpposingTeam.FTM; }
            }
            public int OppFTA 
            {
                get { return CheckOutGame.OpposingTeam.FTA - CheckInGame.OpposingTeam.FTA; }
            }
            public int OppOffRebounds 
            {
                get { return CheckOutGame.OpposingTeam.OffRebounds - CheckInGame.OpposingTeam.OffRebounds; }
            }
            public int OppFouls 
            {
                get { return CheckOutGame.OpposingTeam.Fouls - CheckInGame.OpposingTeam.Fouls; }
            }
            public int OppPointsInPaint 
            {
                get { return CheckOutGame.OpposingTeam.PointsInPaint - CheckInGame.OpposingTeam.PointsInPaint; }
            }
            public int OppSecondChancePoints
            {
                get { return CheckOutGame.OpposingTeam.SecondChancePoints - CheckInGame.OpposingTeam.SecondChancePoints; }
            }
            public int OppFastBreakPoints 
            {
                get { return CheckOutGame.OpposingTeam.FastBreakPoints - CheckInGame.OpposingTeam.FastBreakPoints; }
            }
            public int OppPointsOffTurnovers
            {
                get { return CheckOutGame.OpposingTeam.PointsOffTurnovers - CheckInGame.OpposingTeam.PointsOffTurnovers; }
            }
            public int OppDunks
            {
                get { return CheckOutGame.OpposingTeam.Dunks - CheckInGame.OpposingTeam.Dunks; }
            }

            public int OppRebounds
            {
                get { return OppOffRebounds + OppDefRebounds; }
            }
        }
    }
}
