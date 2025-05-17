using Manager;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Model
{
    /// <summary>
    /// First IA player which is not optimised
    /// </summary>
    public class DumbAIPlayer : AIPlayer
    {
        public DumbAIPlayer() : base("Dumb AI") { }

<<<<<<< HEAD
        /// <summary>
        /// method that this fake player uses to play a game randomly by chosing a empty case on the board
        /// </summary>
        /// <param name="board">the board of the game</param>
        /// <param name="currentPiece">the piece chosen by the opponent</param>
        /// <param name="gameManager"> the main interface that manage the game</param>
        /// <returns>the piece chosen for the opponent</returns>
        public override IPiece? PlayTurn(IBoard board, IPiece currentPiece, IGameManager gameManager)
=======
        public override void PlayTurn(IBoard board, IPiece currentPiece, IGameManager gameManager)
>>>>>>> origin/ConsoleApp
        {
            using var randomGenerator = RandomNumberGenerator.Create();
            byte[] data = new byte[4];
            randomGenerator.GetBytes(data);

            int randomInt = BitConverter.ToInt32(data, 0);

            randomInt = Math.Abs(randomInt);
            List<(int row, int col)> availablePositions = Rules.GetAvailablePositions((Board)board);
            var (row, col) = availablePositions[randomInt % availablePositions.Count];

            bool success = Rules.PlayAMove((Piece)currentPiece, row, col, (Board)board);

            if (success)
            {
                gameManager.DisplayMessage($"{Name} placed a piece at ({row}, {col}).");
            }
            else
            {
                gameManager.DisplayMessage($"{Name}: Failed to place the piece. This should not happen.");
            }
        }
    }
}