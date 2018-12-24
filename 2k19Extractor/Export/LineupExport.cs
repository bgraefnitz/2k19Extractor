using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2k19Extractor.Export
{
    class LineupExport
    {
        public int HomeAway;
        public string Desc;
        
        public int Points;
        public int DefRebounds;
        public int Assists;
        public int Steals;
        public int Blocks;
        public int Turnovers;
        public int FGM;
        public int FGA;
        public int ThreePM;
        public int ThreePA;
        public int FTM;
        public int FTA;
        public int OffRebounds;
        public int Fouls;
        public int PointsInPaint;
        public int SecondChancePoints;
        public int FastBreakPoints;
        public int PointsOffTurnovers;
        public int Dunks;
        public int Rebounds;
        public int PlusMinus;

        public Int64 OppPoints;
        public Int64 OppDefRebounds;
        public Int64 OppAssists;
        public Int64 OppSteals;
        public Int64 OppBlocks;
        public Int64 OppTurnovers;
        public Int64 OppFGM;
        public Int64 OppFGA;
        public Int64 OppThreePM;
        public Int64 OppThreePA;
        public Int64 OppFTM;
        public Int64 OppFTA;
        public Int64 OppOffRebounds;
        public Int64 OppFouls;
        public Int64 OppPointsInPaint;
        public Int64 OppSecondChancePoints;
        public Int64 OppFastBreakPoints;
        public Int64 OppPointsOffTurnovers;
        public Int64 OppDunks;
        public int OppRebounds;

        public int OppPlusMinus;

        public int Appearances;
        public int MinutesPlayed;

        public LineupExport(Lineup lineup)
        {
            HomeAway = lineup.HomeAway;
            Desc = lineup.Desc;
            Points = lineup.Points;
            DefRebounds = lineup.DefRebounds;
            Assists = lineup.Assists;
            Steals = lineup.Steals;
            Blocks = lineup.Blocks;
            Turnovers = lineup.Turnovers;
            FGM = lineup.FGM;
            FGA = lineup.FGA;
            ThreePM = lineup.ThreePM;
            ThreePA = lineup.ThreePA;
            FTM = lineup.FTM;
            FTA = lineup.FTA;
            OffRebounds = lineup.OffRebounds;
            Fouls = lineup.Fouls;
            PointsInPaint = lineup.PointsInPaint;
            SecondChancePoints = lineup.SecondChancePoints;
            FastBreakPoints = lineup.FastBreakPoints;
            PointsOffTurnovers = lineup.PointsOffTurnovers;
            Dunks = lineup.Dunks;
            Rebounds = lineup.Rebounds;
            PlusMinus = lineup.PlusMinus;
            OppPoints = lineup.OppPoints;
            OppDefRebounds = lineup.OppDefRebounds;
            OppAssists = lineup.OppAssists;
            OppSteals = lineup.OppSteals;
            OppBlocks = lineup.OppBlocks;
            OppTurnovers = lineup.OppTurnovers;
            OppFGM = lineup.OppFGM;
            OppFGA = lineup.OppFGA;
            OppThreePM = lineup.OppThreePM;
            OppThreePA = lineup.OppThreePA;
            OppFTM = lineup.OppFTM;
            OppFTA = lineup.OppFTA;
            OppOffRebounds = lineup.OppOffRebounds;
            OppFouls = lineup.OppFouls;
            OppPointsInPaint = lineup.OppPointsInPaint;
            OppSecondChancePoints = lineup.OppSecondChancePoints;
            OppFastBreakPoints = lineup.OppFastBreakPoints;
            OppPointsOffTurnovers = lineup.OppPointsOffTurnovers;
            OppDunks = lineup.OppDunks;
            OppRebounds = lineup.OppRebounds;
            OppPlusMinus = lineup.OppPlusMinus;
            Appearances = lineup.Appearances;
            MinutesPlayed = lineup.MinutesPlayed;
        }
    }
}
