using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2k19Extractor.Export
{
    class PlayerExport
    {
        public int DepthChartPos;        
        public string InGame;        
        public bool PlayerOfTheGame;        
        public string FullName;                
        public float SecondsPlayed;
        public int Points;
        public int DefRebounds;
        public int Assists;
        public int Steals;
        public int Blocks;
        public int Turnovers;
        public int TwoPM;
        public int TwoPA;
        public int ThreePM;
        public int ThreePA;
        public int FTM;
        public int FTA;
        public int OffRebounds;
        public int Fouls;
        public int PlusMinus;
        public int PointsAssisted;
        public int PointsInPaint;
        public int SecondChancePoints;
        public int FastBreakPoints;
        public int PointsOffTurnovers;
        public int Dunks;
        public bool Possession;
        public Single TouchTime;
        public int Touches;
        public int MinutesPlayed;
        public int FGM;
        public int FGA;
        public int Rebounds;
        public int PointsResponsibleFor;
        public int FantasyPoints;
        
        public PlayerExport(Player player)
        {
            DepthChartPos = player.DepthChartPos;
            InGame = player.InGame;
            PlayerOfTheGame = player.PlayerOfTheGame;
            FullName = player.FullName;
            SecondsPlayed = player.SecondsPlayed;
            Points = player.Points;
            DefRebounds = player.DefRebounds;
            Assists = player.Assists;
            Steals = player.Steals;
            Blocks = player.Blocks;
            Turnovers = player.Turnovers;
            TwoPM = player.TwoPM;
            TwoPA = player.TwoPA;
            ThreePM = player.ThreePM;
            ThreePA = player.ThreePA;
            FTM = player.FTM;
            FTA = player.FTA;
            OffRebounds = player.OffRebounds;
            Fouls = player.Fouls;
            PlusMinus = player.PlusMinus;
            PointsAssisted = player.PointsAssisted;
            PointsInPaint = player.PointsInPaint;
            SecondChancePoints = player.SecondChancePoints;
            FastBreakPoints = player.FastBreakPoints;
            PointsOffTurnovers = player.PointsOffTurnovers;
            Dunks = player.Dunks;
            Possession = player.Possession;
            Touches = player.Touches;
            TouchTime = player.TouchTime;
            MinutesPlayed = player.MinutesPlayed;
            FGM = player.FGM;
            FGA = player.FGA;
            Rebounds = player.Rebounds;
            PointsResponsibleFor = player.PointsResponsibleFor;
            FantasyPoints = player.FantasyPoints;
        }
    }
}
