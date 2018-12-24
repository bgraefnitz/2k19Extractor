using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2k19Extractor.Export
{
    class TeamExport
    {
        public string HomeAway;
        public string Name;

        public List<PlayerExport> Players;
        public Score Score;
        public List<LineupExport> Lineups;
        
        public bool Possession;
        public int Points;
        public int DefRebounds;
        public int Assists;
        public int Steals;
        public int Blocks;
        public int Turnovers;
        public int MinutesPlayed;
        public int FGM;
        public int FGA;
        public decimal FGPercent;
        public int Rebounds;
        public int ThreePM;
        public int ThreePA;
        public decimal ThreePercent;
        public int FTM;
        public int FTA;
        public decimal FTPercent;
        public int OffRebounds;
        public int Fouls;
        public int PointsInPaint;
        public int SecondChancePoints;
        public int FastBreakPoints;
        public int PointsOffTurnovers;
        public int PointsAssisted;
        public int Dunks;

        public TeamExport(Team team)
        {
            HomeAway = team.HomeAway;
            Name = team.Name;
            Score = team.Score;
            Possession = team.Possession;
            Points = team.Points;
            DefRebounds = team.DefRebounds;
            Assists = team.Assists;
            Steals = team.Steals;
            Blocks = team.Blocks;
            Turnovers = team.Turnovers;
            MinutesPlayed = team.MinutesPlayed;
            FGM = team.FGM;
            FGA = team.FGA;
            FGPercent = team.FGPercent;
            Rebounds = team.Rebounds;
            ThreePM = team.ThreePM;
            ThreePA = team.ThreePA;
            ThreePercent = team.ThreePercent;
            FTM = team.FTM;
            FTA = team.FTA;
            FTPercent = team.FTPercent;
            OffRebounds = team.OffRebounds;
            Fouls = team.Fouls;
            PointsInPaint = team.PointsInPaint;
            SecondChancePoints = team.SecondChancePoints;
            FastBreakPoints = team.FastBreakPoints;
            PointsOffTurnovers = team.PointsOffTurnovers;
            PointsAssisted = team.PointsAssisted;
            Dunks = team.Dunks;

            Players = new List<PlayerExport>();
            foreach (var player in team.Players)
                Players.Add(new PlayerExport(player));

            Lineups = new List<LineupExport>();
            foreach (var lineup in team.Lineups)
                Lineups.Add(new LineupExport(lineup));

        }
    }
}
