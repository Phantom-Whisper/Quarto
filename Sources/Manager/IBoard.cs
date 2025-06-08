using System.IO.Pipelines;

namespace Manager
{
    public interface IBoard
    {
        int SizeX { get; }
        int SizeY { get; }
        public IPiece[,] Grid { get; }
        bool IsBoardFull();
        bool IsEmpty(int row, int col);
        IPiece GetPiece(int row, int col);
        bool IsOnBoard(int row, int col);
        int PositionXPiece(IPiece piece);
        int PositionYPiece(IPiece piece);
        public List<IPiece> GetPieces();
        public IEnumerable<List<IPiece>> CombinationsOf4(List<IPiece> pieces);
        (int row, int col) PositionPiece(IPiece piece);
        void InsertPiece(IPiece piece, int row, int col);
        public IPiece[,] GetPiecesGrid();
    }
}
