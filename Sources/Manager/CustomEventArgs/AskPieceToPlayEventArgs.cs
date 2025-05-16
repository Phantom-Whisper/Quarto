namespace Manager.CustomEventArgs
{
    public class AskPieceToPlayEventArgs(IBag bag) : EventArgs
    {
        public IBag Bag { get; } = bag;
    }
}
