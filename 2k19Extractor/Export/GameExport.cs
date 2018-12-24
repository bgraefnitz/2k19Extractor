using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace _2k19Extractor.Export
{
    class GameExport
    {
        public DateTime StartTime;
        public int SecondsRemaining;
        public bool GameEnded;
        public int CurrentQuarter;

        public List<TeamExport> Teams = new List<TeamExport>();
        public List<GameEvent> GameEvents = new List<GameEvent>();

        public string GameTime;

        public string PlayerOfTheGame;

        public GameExport(Game game)
        {
            StartTime = game.StartTime;
            SecondsRemaining = game.SecondsRemaining;
            GameTime = game.GameTime;
            GameEnded = game.GameEnded;
            CurrentQuarter = game.CurrentQuarter;
            GameEvents = game.GameEvents;
            PlayerOfTheGame = game.PlayerOfTheGame;

            foreach (var team in game.Teams)
                Teams.Add(new TeamExport(team));
        }

    }
}
