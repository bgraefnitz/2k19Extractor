using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace _2k19Extractor
{
    [Serializable] 
    public class Game
    {
        public DateTime StartTime;
        public int SecondsRemaining;
        public bool GameEnded;
        private int _lastQuarterWithTime;
        private int _quarterFromGame;
        public int CurrentQuarter
        {
            get
            {
                return SecondsRemaining == 0 ? _lastQuarterWithTime : _quarterFromGame;
            }
            set
            {
                _quarterFromGame = value;
                if (SecondsRemaining > 0)
                    _lastQuarterWithTime = value;
            }
        }
        public const Int64 QuarterModifier = 0x51710F0;
        public const Int64 SecondsRemainingModifier = 0x5C30AF0;
        public const Int64 PlayerWithBallModifier = 0x5C29A90;

        public Int64 QuarterPointer;
        public Int64 SecondsRemainingPointer;
        public Int64 PlayerWithBallPointer;

        public const Int64 StartingQuarterTime = 720;
        public Int64 CurrentPlayerWithBall;
        public List<Team> Teams = new List<Team>();
        public List<GameEvent> GameEvents = new List<GameEvent>(); 

        public Game(DateTime startTime, Int64 baseAddress)
        {
            StartTime = startTime;
            Teams = new List<Team>();
            GameEnded = false;
            QuarterPointer = QuarterModifier + baseAddress;
            SecondsRemainingPointer = SecondsRemainingModifier + baseAddress;
            PlayerWithBallPointer = PlayerWithBallModifier + baseAddress;
        }

        public GameSituation ToGameSituation(int team)
        {
            var gameSituation = new GameSituation();
            
            var opponent = team == 0 ? 1 : 0;
            gameSituation.CurrentQuarter = CurrentQuarter;
            gameSituation.SecondsRemaining = SecondsRemaining;
            foreach (var prop in gameSituation.Team.GetType().GetProperties())
            {
                var gamProp = Teams[team].GetType().GetProperty(prop.Name);
                var gameValue = gamProp.GetValue(Teams[team], null);
                prop.SetValue(gameSituation.Team,gameValue,null);
            }

            foreach (var prop in gameSituation.OpposingTeam.GetType().GetProperties())
            {
                var gameProp = Teams[opponent].GetType().GetProperty(prop.Name);
                var gameValue = gameProp.GetValue(Teams[opponent], null);
                prop.SetValue(gameSituation.OpposingTeam, gameValue, null);
            }

            return gameSituation;
        }

        //This returns the current game time/quarter as a readable string
        public string GameTime
        {
            get
            {
                if (GameEnded)
                    return "FINAL";
                //if it is after the fourth quarter, no time left and the teams aren't tied, the game is over
                if (CurrentQuarter >= 4 && SecondsRemaining == 0 && Teams[0].Score.Final != Teams[1].Score.Final)
                    return "End of game check";
                var timeRemaining = TimeSpan.FromSeconds(SecondsRemaining);   
                //If it is after the fourth quarter, with time left or teams tied then we're in OT
                if (CurrentQuarter > 4)
                    return "OT" + (CurrentQuarter - 4).ToString(CultureInfo.InvariantCulture) + " - " + timeRemaining.ToString(@"mm\:ss");
                //Show end of quarter
                if (SecondsRemaining == 0)
                    return "End of Q" + CurrentQuarter.ToString(CultureInfo.InvariantCulture);
                //Default: display which quarter and how much time left
                return "Q" + CurrentQuarter.ToString(CultureInfo.InvariantCulture) + " - " + timeRemaining.ToString(@"mm\:ss");
            }
        }

        public Team WinningTeam
        {
            get
            {
                return Teams.OrderByDescending(t => t.Points).First();
            }
        }

        //This returns the current game time/quarter along with the score as a readable string 
        public string GameTimeScore
        {
            get
            {
                return GameTime + " " + Teams[0].Name + " " + Teams[0].Score.Final.ToString(CultureInfo.InvariantCulture) + " - " + Teams[1].Name + " " + Teams[1].Score.Final.ToString(CultureInfo.InvariantCulture);
            }
        }
        
        //Return the player of the game
        public string PlayerOfTheGame
        {
            get
            {
                if (!GameEnded)
                {
                    return "N/A";
                }
                else
                {
                    return WinningTeam.Players.OrderByDescending(p => p.FantasyPoints).First().FullName;
                }
            }
        }

        public List<GameEvent> GameChanges(Game prevGame)
        {
            //new game event object to hold new list of changes
            var gameEvents = new List<GameEvent>();

            //Check to make sure that teams existed in previous game object (necessary because on first run prevGame will not have teams/players populated)
            if (Teams.Count != prevGame.Teams.Count) return gameEvents;
            for (var t = 0; t < Teams.Count; t++)
            {
                //check team lineups to see if they changed from the lineup in the previous game
                var newLineup = Teams[t].ActiveLineup;
                var oldLineup = prevGame.Teams[t].ActiveLineup;
                if (newLineup != null && oldLineup != null)
                {
                    if (newLineup.PG != oldLineup.PG)
                        gameEvents.Add(new GameEvent(GameTimeScore, Teams[t].Name,
                            Teams[t].Players[newLineup.PG - 1].FullName,
                            "Now playing PG"));
                    if (newLineup.SG != oldLineup.SG)
                        gameEvents.Add(new GameEvent(GameTimeScore, Teams[t].Name,
                            Teams[t].Players[newLineup.SG - 1].FullName,
                            "Now playing SG"));
                    if (newLineup.SF != oldLineup.SF)
                        gameEvents.Add(new GameEvent(GameTimeScore, Teams[t].Name,
                            Teams[t].Players[newLineup.SF - 1].FullName,
                            "Now playing SF"));
                    if (newLineup.PF != oldLineup.PF)
                        gameEvents.Add(new GameEvent(GameTimeScore, Teams[t].Name,
                            Teams[t].Players[newLineup.PF - 1].FullName,
                            "Now playing PF"));
                    if (newLineup.C != oldLineup.C)
                        gameEvents.Add(new GameEvent(GameTimeScore, Teams[t].Name,
                            Teams[t].Players[newLineup.C - 1].FullName,
                            "Now playing C"));
                }
                for (var p = 0; p < Teams[t].Players.Count; p++)
                {
                    //setup variables to hold if there are any changes and what those changes are
                    var anyPlayerChange = false;
                    var allChanges = new List<string>();
                    //hard coding all of the stats and how we want to display
                    //may be a more elegant way to do this, but preferring this due to being able to state the order and display

                    if (Teams[t].Players[p].Points != prevGame.Teams[t].Players[p].Points)
                    {
                        anyPlayerChange = true;


                        //add this basic string regardless
                        var changeString = (Teams[t].Players[p].Points - prevGame.Teams[t].Players[p].Points).ToString(

                                CultureInfo.InvariantCulture) + " PTS";

                        //check for type of score and add description after the points
                        bool dunk = (Teams[t].Players[p].Dunks != prevGame.Teams[t].Players[p].Dunks);
                        bool inPaint = (Teams[t].Players[p].PointsInPaint != prevGame.Teams[t].Players[p].PointsInPaint);
                        
                        if (inPaint && !dunk) changeString += " in the paint";
                        if (dunk) changeString += " on a dunk";

                        //add total string regardless
                        changeString += ": "  + Teams[t].Players[p].Points + " PTS Total";
                        allChanges.Add(changeString);
                    }
                    if (Teams[t].Players[p].DefRebounds != prevGame.Teams[t].Players[p].DefRebounds)
                    {
                        anyPlayerChange = true;
                        allChanges.Add(
                            (Teams[t].Players[p].DefRebounds - prevGame.Teams[t].Players[p].DefRebounds).ToString(
                                CultureInfo.InvariantCulture) + " DREB: " + Teams[t].Players[p].DefRebounds +
                            " DREB Total");
                    }
                    if (Teams[t].Players[p].OffRebounds != prevGame.Teams[t].Players[p].OffRebounds)
                    {
                        anyPlayerChange = true;
                        allChanges.Add(
                            (Teams[t].Players[p].OffRebounds - prevGame.Teams[t].Players[p].OffRebounds).ToString(
                                CultureInfo.InvariantCulture) + " OREB: " + Teams[t].Players[p].OffRebounds +
                            " OREB Total");
                    }
                    if (Teams[t].Players[p].Assists != prevGame.Teams[t].Players[p].Assists)
                    {
                        anyPlayerChange = true;
                        allChanges.Add(
                            (Teams[t].Players[p].Assists - prevGame.Teams[t].Players[p].Assists).ToString(
                                CultureInfo.InvariantCulture) + " AST: " + Teams[t].Players[p].Assists +
                            " AST Total");
                    }
                    if (Teams[t].Players[p].Steals != prevGame.Teams[t].Players[p].Steals)
                    {
                        anyPlayerChange = true;
                        allChanges.Add(
                            (Teams[t].Players[p].Steals - prevGame.Teams[t].Players[p].Steals).ToString(
                                CultureInfo.InvariantCulture) + " STL: " + Teams[t].Players[p].Steals + " STL Total");
                    }
                    if (Teams[t].Players[p].Blocks != prevGame.Teams[t].Players[p].Blocks)
                    {
                        anyPlayerChange = true;
                        allChanges.Add(
                            (Teams[t].Players[p].Blocks - prevGame.Teams[t].Players[p].Blocks).ToString(
                                CultureInfo.InvariantCulture) + " BLK: " + Teams[t].Players[p].Blocks + " BLK Total");
                    }
                    if (Teams[t].Players[p].Turnovers != prevGame.Teams[t].Players[p].Turnovers)
                    {
                        anyPlayerChange = true;
                        allChanges.Add(
                            (Teams[t].Players[p].Turnovers - prevGame.Teams[t].Players[p].Turnovers).ToString(
                                CultureInfo.InvariantCulture) + " TO: " + Teams[t].Players[p].Turnovers +
                            " TO Total");
                    }
                    if (Teams[t].Players[p].Fouls != prevGame.Teams[t].Players[p].Fouls)
                    {
                        anyPlayerChange = true;
                        allChanges.Add(
                            (Teams[t].Players[p].Fouls - prevGame.Teams[t].Players[p].Fouls).ToString(
                                CultureInfo.InvariantCulture) + " PF: " + Teams[t].Players[p].Fouls + " PF Total");
                    }
                    if (Teams[t].Players[p].FGA != prevGame.Teams[t].Players[p].FGA)
                    {
                        anyPlayerChange = true;
                        allChanges.Add(
                            (Teams[t].Players[p].FGM - prevGame.Teams[t].Players[p].FGM).ToString(
                                CultureInfo.InvariantCulture) + "-" +
                            (Teams[t].Players[p].FGA - prevGame.Teams[t].Players[p].FGA).ToString(
                                CultureInfo.InvariantCulture) + " FG: " +
                            Teams[t].Players[p].FGM.ToString(CultureInfo.InvariantCulture) + "-" +
                            Teams[t].Players[p].FGA.ToString(CultureInfo.InvariantCulture) + " FG Total");
                    }
                    if (Teams[t].Players[p].ThreePA != prevGame.Teams[t].Players[p].ThreePA)
                    {
                        anyPlayerChange = true;
                        allChanges.Add(
                            (Teams[t].Players[p].ThreePM - prevGame.Teams[t].Players[p].ThreePM).ToString(
                                CultureInfo.InvariantCulture) + "-" +
                            (Teams[t].Players[p].ThreePA - prevGame.Teams[t].Players[p].ThreePA).ToString(
                                CultureInfo.InvariantCulture) + " 3P: " +
                            Teams[t].Players[p].ThreePM.ToString(CultureInfo.InvariantCulture) + "-" +
                            Teams[t].Players[p].ThreePA.ToString(CultureInfo.InvariantCulture) + " 3P Total");
                    }
                    if (Teams[t].Players[p].FTA != prevGame.Teams[t].Players[p].FTA)
                    {
                        anyPlayerChange = true;
                        allChanges.Add(
                            (Teams[t].Players[p].FTM - prevGame.Teams[t].Players[p].FTM).ToString(
                                CultureInfo.InvariantCulture) + "-" +
                            (Teams[t].Players[p].FTA - prevGame.Teams[t].Players[p].FTA).ToString(
                                CultureInfo.InvariantCulture) + " FT: " +
                            Teams[t].Players[p].FTM.ToString(CultureInfo.InvariantCulture) + "-" +
                            Teams[t].Players[p].FTA.ToString(CultureInfo.InvariantCulture) + " FT Total");
                    }


                    //If there were any changes detected then write a new game event with all of the changes
                    if (anyPlayerChange)
                        gameEvents.Add(new GameEvent(GameTimeScore, Teams[t].Name, Teams[t].Players[p].FullName,
                            string.Join(", ", allChanges.ToArray())));
                }
            }
            return gameEvents;
        }
    }
}
