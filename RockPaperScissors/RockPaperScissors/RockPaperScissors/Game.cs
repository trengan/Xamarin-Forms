using System;
using System.Collections.Generic;
using System.Text;

namespace RockPaperScissors
{
    public class Game
    {
        public TwoPlayerMatch Match { get; private set; }

        public GameType GameType { get; set; } 

        public void Play(MatchType matchType)
        {
            Match = GetMatch(matchType);
            SetChoiceForComputer(matchType);
        }

        public string GetResult()
        {
            string strResultValue;
            Match.CalculateResult();
            if(Match.IsTie)
            {
                strResultValue = "Match Draw";
            }
            else
            {
                strResultValue = Match.Winner.Name + " Won";
            }

            return strResultValue;
        }

        private TwoPlayerMatch GetMatch(MatchType matchType)
        {
            TwoPlayerMatch match = null;
            switch (matchType)
            {
                case MatchType.HumanVSComputer:
                    {
                        switch (GameType)
                        {
                            case GameType.RPS:
                                {
                                    match = new TwoPlayerMatchRPS(new Human() { Name = "You", IsHuman = true }, new Computer() { Name = "Computer" });
                                    break;
                                }
                            case GameType.RPSLS:
                                {
                                    match = new TwoPlayerMatchRPSLS(new Human() { Name = "You", IsHuman = true }, new Computer() { Name = "Computer" });
                                    break;
                                }
                        }
                        break;
                    }
                case MatchType.ComputerVSComputer:
                    {
                        switch (GameType)
                        {
                            case GameType.RPS:
                                {
                                    match = new TwoPlayerMatchRPS(new Computer() { Name = "Computer1" }, new Computer() { Name = "Computer2" });
                                    break;
                                }
                            case GameType.RPSLS:
                                {
                                    match = new TwoPlayerMatchRPSLS(new Computer() { Name = "Computer1" }, new Computer() { Name = "Computer2" });
                                    break;
                                }
                        }
                        break;
                    }
            }
            return match;
        }

        private void SetChoiceForComputer(MatchType matchType)
        {
            switch (matchType)
            {
                case MatchType.HumanVSComputer:
                    {
                        Match.SetChoiceForPlayer2(Match.SelectChoiceForComputer());
                        break;
                    }
                case MatchType.ComputerVSComputer:
                    {
                        Match.SetChoiceForPlayer1(Match.SelectChoiceForComputer());
                        Match.SetChoiceForPlayer2(Match.SelectChoiceForComputer());
                        break;
                    }
            }
        }
    }

    

}



