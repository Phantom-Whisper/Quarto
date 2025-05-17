using Manager;
using Manager.CustomEventArgs;
using System.Security.Cryptography;

namespace Model
{
    public class HumanPlayer  : Player
    {
        public HumanPlayer(string name) : base(name) { }

        /// <summary>
        /// method which make a player play a turn by choosing a place for the piece and choose the piece for the opponent
        /// </summary>
        /// <param name="board">the board of the game</param>
        /// <param name="currentPiece">the piece chosen by the opponent</param>
        /// <param name="gameManager"> the main interface that manage the game</param>
        /// <returns>the piece chosen for the opponent</returns>
        public override void PlayTurn(IBoard board, IPiece currentPiece, IGameManager gameManager)
        {
            bool placed = false;

            while (!placed)
            {
                gameManager.DisplayMessage($"{Name}, enter the row where you want to place the piece:");
                if (!int.TryParse(Console.ReadLine(), out int row))
                {
                    gameManager.DisplayMessage("Invalid row input.");
                    continue;
                }

                gameManager.DisplayMessage("Enter the column:");
                if (!int.TryParse(Console.ReadLine(), out int col))
                {
                    gameManager.DisplayMessage("Invalid column input.");
                    continue;
                }

                placed = Rules.PlayAMove((Piece)currentPiece, row, col, (Board)board);

                if (!placed)
                {
                    gameManager.DisplayMessage("Invalid move. Try again.");
                }
            }
        }
    }
}
