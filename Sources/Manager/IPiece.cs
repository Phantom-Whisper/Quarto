namespace Manager
{
    public interface IPiece
    {
        bool IsSquare { get; }
        bool IsLight { get; }
        bool IsBig { get; }
        bool IsFull { get; }
        public string PieceToString();
    }
}
