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
            var (row, col) = gameManager.RequestCoordinates(this);

            bool placed = Rules.PlayAMove((Piece)currentPiece, row, col, (Board)board);

            if (!placed)
            {
                gameManager.OnDisplayMessage("Invalid move. The move will be skipped.");
            }
        }
    }
}
