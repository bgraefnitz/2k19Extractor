using System;


namespace _2k19Extractor
{
    [Serializable] 
    public class GameSituation
    {
        public int SecondsRemaining;
        public int CurrentQuarter;
        public TeamSituation Team = new TeamSituation();
        public TeamSituation OpposingTeam = new TeamSituation();

        [Serializable] 
        public class TeamSituation
        {
            public Score Score;

            //All of the properties to hold stats
            public int Points { get; set; }
            public int DefRebounds { get; set; }
            public int Assists { get; set; }
            public int Steals { get; set; }
            public int Blocks { get; set; }
            public int Turnovers { get; set; }
            public int ThreePM { get; set; }
            public int ThreePA { get; set; }
            public int FTM { get; set; }
            public int FTA { get; set; }
            public int FGM { get; set; }
            public int FGA { get; set; }
            public int OffRebounds { get; set; }
            public int Fouls { get; set; }
            public int PointsAssisted { get; set; }
            public int PointsInPaint { get; set; }
            public int SecondChancePoints { get; set; }
            public int FastBreakPoints { get; set; }
            public int PointsOffTurnovers { get; set; }
            public int Dunks { get; set; }
            public int Rebounds { get; set; }
        }
    }
}
