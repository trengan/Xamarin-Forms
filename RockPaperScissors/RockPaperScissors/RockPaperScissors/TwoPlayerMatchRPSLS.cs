using System;

namespace RockPaperScissors
{
    /// <summary>
    /// Class this is responsible for choose the choice for computer and calculate the result for Rock,Paper,Scissor,Lizard,Spock game.
    /// </summary>
    public class TwoPlayerMatchRPSLS : TwoPlayerMatch
    {
        Random _random;
        public TwoPlayerMatchRPSLS(Player player1, Player player2)
        {
            _random = new Random();
            Player1 = player1;
            Player2 = player2;
        }
 
        public override void CalculateResult()
        {
            //  Todo : Have to calculate result for RPSLS
            var player1Choice = Player1.Choice;
            var player2Choice = Player2.Choice;

            if (player1Choice == player2Choice)
            {

            }
        }

        public override Choice SelectChoiceForComputer()
        {
            int randomNumber = _random.Next((int)Choice.Spock);
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
                case 3:
                    _choice = Choice.Lizard;
                    break;
                case 4:
                    _choice = Choice.Spock;
                    break;
                default:
                    _choice = Choice.Rock;
                    break;
            }
            return _choice;
        }
    }
}
