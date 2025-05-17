namespace Manager.CustomEventArgs
{
    public class AskCoordinatesEventArgs(IPlayer player, IBoard board, Action<(int row, int col)> callback) : EventArgs
    {
        public IPlayer Player { get; } = player;
        public IBoard Board { get; } = board;
        public Action<(int row, int col)> CoordinateCallback { get; } = callback;
    }
}
