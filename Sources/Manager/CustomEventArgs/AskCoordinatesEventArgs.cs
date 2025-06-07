namespace Manager.CustomEventArgs
{
    public class AskCoordinatesEventArgs(IPlayer player, IBoard board) : EventArgs
    {
        public IPlayer Player { get; } = player;
        public IBoard Board { get; } = board;
        public TaskCompletionSource<(int row, int col)?> CoordinatesTcs { get; } = new TaskCompletionSource<(int, int)?>();
    }
}
