namespace Manager.CustomEventArgs
{
    public class AskPieceToPlayEventArgs(IPlayer? player, List<IPiece> pieces, IPiece? pieceToPlay) : EventArgs
    {
        public IPlayer? Player { get; } = player;
        public List<IPiece> Pieces { get; } = pieces;
        public IPiece? PieceToPlay { get; set; } = pieceToPlay;
    }
}
