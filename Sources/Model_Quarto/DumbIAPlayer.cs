using Manager;
using Manager.CustomEventArgs;
using System.Security.Cryptography;

namespace Model
{
    /// <summary>
    /// First IA player which is not optimised
    /// </summary>
    public class DumbAIPlayer : AIPlayer
    {
        public DumbAIPlayer() : base("Dumb AI") { }


        /// <summary>
        /// method that this fake player uses to play a game randomly by chosing a empty case on the board
        /// </summary>
        /// <param name="board">the board of the game</param>
        /// <param name="currentPiece">the piece chosen by the opponent</param>
        /// <returns>the piece chosen for the opponent</returns>
        public override async Task<(int, int)?> PlayTurn(IBoard board, IPiece currentPiece)
        {
            await Task.Delay(1000);
            using var randomGenerator = RandomNumberGenerator.Create();
            byte[] data = new byte[4];
            randomGenerator.GetBytes(data);
            int randomInt = Math.Abs(BitConverter.ToInt32(data, 0));

            List<(int row, int col)> availablePositions = Rules.GetAvailablePositions((Board)board);
            if (availablePositions.Count == 0)
                return null;

            var (row, col) = availablePositions[randomInt % availablePositions.Count];

            return (row, col);
        }

        public IPiece ChoosePiece(IBag bag)
        {
            Task.Delay(800).Wait();
            using var randomGenerator = RandomNumberGenerator.Create();
            byte[] data = new byte[4];
            randomGenerator.GetBytes(data);
            int randomInt = Math.Abs(BitConverter.ToInt32(data, 0));

            var pieces = bag.Baglist;
            return pieces[randomInt % pieces.Count];
        }
    }
}