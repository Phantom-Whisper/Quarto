namespace Manager.CustomEventArgs
{
    public class PlayerNameRequestedEventArgs
    {
        public int PlayerIndex { get; }
        public string? PlayerName { get; set; }

        public PlayerNameRequestedEventArgs(int index)
        {
            PlayerIndex = index;
        }
    }
}
