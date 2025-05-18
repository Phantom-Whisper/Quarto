namespace Manager.CustomEventArgs
{
    public class GameStartedEventArgs(IBoard board, IBag bag, IPlayer currentPlayer) : EventArgs
    {
        public IBoard Board { get; } = board;
        public IBag Bag { get; } = bag;
        public IPlayer CurrentPlayer { get; } = currentPlayer;
    }
}
