namespace Manager.CustomEventArgs
{
    public class BoardChangedEventArgs(IBoard board) : EventArgs
    {
        public IBoard Board { get; } = board;
    }
}
