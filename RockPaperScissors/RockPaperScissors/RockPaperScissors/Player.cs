using System;
using System.Collections.Generic;
using System.Text;

namespace RockPaperScissors
{
    public abstract class Player
    {
        private Choice _choice;

        public void SetChoice(Choice newChoice)
        {
            _choice = newChoice;
        }

        public Choice Choice { get { return _choice; } }

        public bool IsHuman { get; set; }

        public string Name { get; set; }

    }

   
    public enum MatchType
    {
        HumanVSComputer,
        ComputerVSComputer,
        HumanVSHuman
    }

    public enum Choice
    {
        Rock,
        Paper,
        Scissors,
        Lizard,
        Spock

    }
    public enum GameType
    {
       RPS,
       RPSLS
    }

}
