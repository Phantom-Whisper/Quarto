using Manager;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Model
{
    public class DumbAIPlayer : AIPlayer
    {
        public DumbAIPlayer() : base("Dumb AI") { }

        public override void PlayTurn(IBoard board, IPiece currentPiece, IGameManager gameManager)
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

            var availablePieces = gameManager.GetAvailablePieces();
            if (availablePieces.Count == 0)
            {
                gameManager.DisplayMessage($"{Name}: No pieces left to give.");
                return ;
            }
            randomInt = Math.Abs(randomInt);
            var selectedPiece = availablePieces[randomInt % availablePieces.Count];
        }
    }
}