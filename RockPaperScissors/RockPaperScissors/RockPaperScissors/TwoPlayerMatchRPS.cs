using System;
using System.Collections.Generic;
using System.Text;

namespace RockPaperScissors
{

    /// <summary>
    /// Class this is responsible for choose the choice for computer and calculate the result for Rock,Paper,Scissor game.
    /// </summary>
    public class TwoPlayerMatchRPS : TwoPlayerMatch
    {
        Random _random;
        public TwoPlayerMatchRPS(Player player1, Player player2)
        {
            _random = new Random();
            Player1 = player1;
            Player2 = player2;
        }

        public override void CalculateResult()
        {
            var player1Choice = Player1.Choice;
            var player2Choice = Player2.Choice;

            if (player1Choice == player2Choice)
            {
                IsTie = true;
            }
            else if ((player1Choice.Equals(Choice.Rock) && player2Choice.Equals(Choice.Scissors))
                     || (player1Choice.Equals(Choice.Paper) && player2Choice.Equals(Choice.Rock))
                     || (player1Choice.Equals(Choice.Scissors) && player2Choice.Equals(Choice.Paper))
                     )
            {
                Winner = Player1;
            }
            else
            {
                Winner = Player2;
            }
        }

        public override Choice SelectChoiceForComputer()
        {
            int randomNumber = _random.Next((int)Choice.Scissors);
            Choice _choice;
            switch (randomNumber)
            {
                case 0:
                    _choice = Choice.Rock;
                    break;
                case 1:
                    _choice = Choice.Paper;
                    break;
                case 2:
                    _choice = Choice.Scissors;
                    break;
                default:
                    _choice = Choice.Rock;
                    break;
            }

            return _choice;
        }
    }
}
