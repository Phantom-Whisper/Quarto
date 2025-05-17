namespace Manager.CustomEventArgs
{
    public class BagChangedEventArgs(IBag bag) : EventArgs
    {
        public IBag Bag { get; } = bag;
    }
}
