using System;
using System.Collections.Generic;
using System.Text;

namespace RockPaperScissors
{
    public abstract class TwoPlayerMatch
    {
        protected Player Player1 { get; set; }

        protected Player Player2 { get; set; }

        public bool IsPlayer1Human
        {
            get
            {
                return Player1.IsHuman;
            }
        }

        public bool IsPlayer2Human
        {
            get
            {
                return Player2.IsHuman;
            }
        }

        public void SetChoiceForPlayer1(Choice choice)
        {
            Player1.SetChoice(choice);
        }
        public void SetChoiceForPlayer2(Choice choice)
        {
            Player2.SetChoice(choice);
        }

        public Choice GetChoiceFromPalyer1()
        {
            return Player1.Choice;
        }

        public Choice GetChoiceFromPalyer2()
        {
            return Player2.Choice;
        }

        public bool IsTie { get; set; }

        public Player Winner { get; set; }

        public abstract Choice SelectChoiceForComputer();

        public abstract void CalculateResult();
    }
}
