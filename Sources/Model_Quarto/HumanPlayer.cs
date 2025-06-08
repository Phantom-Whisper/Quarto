using Manager;
using Manager.CustomEventArgs;

namespace Model
{
    public class HumanPlayer(string name) : Player(name)
    {
        public event EventHandler<AskCoordinatesEventArgs>? AskCoordinate;

        /// <summary>
        /// method which make a player play a turn by choosing a place for the piece and choose the piece for the opponent
        /// </summary>
        /// <param name="board">the board of the game</param>
        /// <param name="currentPiece">the piece chosen by the opponent</param>
        /// <returns>the piece chosen for the opponent</returns>
        public override async Task<(int, int)?> PlayTurn(IBoard board, IPiece currentPiece)
        {
            var args = new AskCoordinatesEventArgs(this, board);
            AskCoordinate?.Invoke(this, args);
            var coords = await args.CoordinatesTcs.Task;

            if (coords == null)
                return null;

            return coords;
        }
    }
}
