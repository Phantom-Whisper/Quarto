namespace Manager.CustomEventArgs
{
    public class QuartoEventArgs(IRulesManager rules, IBoard board, IPlayer currentPlayer) : EventArgs
    {
        public IRulesManager RulesManager { get; } = rules;
        public IBoard Board { get; } = board;
        public IPlayer CurrentPlayer { get; } = currentPlayer;
    }
}
