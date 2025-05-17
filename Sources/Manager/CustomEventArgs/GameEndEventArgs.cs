namespace Manager.CustomEventArgs
{
    public class GameEndEventArgs(IPlayer winner) : EventArgs
    {
        public IPlayer Winner { get; } = winner;
    }
}
