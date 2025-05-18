namespace Manager.CustomEventArgs
{
    public class QuartoEventArgs : EventArgs
    {
        public IRulesManager RulesManager { get; }
        public IBoard Board { get; }
        public IPlayer CurrentPlayer { get; }
        public IEnumerable<IPiece>? QuartoPieces { get; }
        public List<(int row, int col)>? Coordinates { get; }

        /// <summary>
        /// Constructor for QuartoEventArgs with pieces.
        /// </summary>
        public QuartoEventArgs(IRulesManager rules, IBoard board, IPlayer currentPlayer, IEnumerable<IPiece> quartoPieces)
        {
            RulesManager = rules;
            Board = board;
            CurrentPlayer = currentPlayer;
            QuartoPieces = quartoPieces;
            Coordinates = null;
        }

        /// <summary>
        /// Constructor for QuartoEventArgs with coordinates.
        /// </summary>
        public QuartoEventArgs(IRulesManager rules, IBoard board, IPlayer currentPlayer, List<(int row, int col)> coords)
        {
            RulesManager = rules;
            Board = board;
            CurrentPlayer = currentPlayer;
            Coordinates = coords;
            QuartoPieces = null;
        }
    }
}
