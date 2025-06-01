namespace Manager
{
    public interface IBoard
    {
        int SizeX { get; }
        int SizeY { get; }
        public ICell[,] Grid { get; }
        bool IsBoardFull();
        bool IsEmpty(int row, int col);
        IPiece GetPiece(int row, int col);
        bool IsOnBoard(int row, int col);
        int PositionXPiece(IPiece piece);
        int PositionYPiece(IPiece piece);
        public List<IPiece> GetAllPieces();
        public IEnumerable<List<IPiece>> CombinationsOf4(List<IPiece> pieces);
        (int row, int col) PositionPiece(IPiece piece);
        void InsertPiece(IPiece piece, int row, int col);
    }
}
