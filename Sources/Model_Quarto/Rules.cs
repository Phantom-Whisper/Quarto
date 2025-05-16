using Manager;

namespace Model
{
    public class RulesBeginner : IRulesManager
    {
        public bool IsGameOver(IBag bag, IBoard board)
        {
            return bag.IsEmpty() && board.IsBoardFull();
        }

        private static bool IsMoveValid(Board board, int row, int col)
        {
            return board.IsOnBoard(row, col) && board.IsEmpty(row, col);
        }

        private static bool IsRow(Board board, List<IPiece> pieces)
        {
            if (pieces == null || pieces.Count != 4) throw new ArgumentException("Exactly 4 pieces are required.");

            int row = board.PositionXPiece(pieces[0]);
            return pieces.All(p => board.PositionXPiece(p) == row);
        }

        private static bool IsColumn(Board board, List<IPiece> pieces)
        {
            if (pieces == null || pieces.Count != 4) throw new ArgumentException("Exactly 4 pieces are required.");

            int col = board.PositionYPiece(pieces[0]);
            return pieces.All(p => board.PositionYPiece(p) == col);
        }

        private static bool IsDiagonal1(IBoard board, List<IPiece> pieces)
        {
            if (pieces == null || pieces.Count != 4) throw new ArgumentException("Exactly 4 pieces are required.");

            return pieces.All(p => board.PositionXPiece(p) == board.PositionYPiece(p));
        }

        private static bool IsDiagonal2(IBoard board, List<IPiece> pieces)
        {
            if (pieces == null || pieces.Count != 4) throw new ArgumentException("Exactly 4 pieces are required.");

            int sizeMinusOne = board.SizeX - 1;
            return pieces.All(p => board.PositionXPiece(p) + board.PositionYPiece(p) == sizeMinusOne);
        }

        private static bool IsSameShape(List<IPiece> pieces)
        {
            bool firstShape = pieces[0].IsSquare;
            return pieces.All(p => p.IsSquare == firstShape);
        }

        private static bool IsSameColor(List<IPiece> pieces)
        {
            bool firstColor = pieces[0].IsLight;
            return pieces.All(p => p.IsLight == firstColor);
        }

        public virtual bool HasCommonAttribute(List<IPiece> pieces)
        {
            if (pieces == null || pieces.Count != 4)
                throw new ArgumentException("Exactly 4 pieces are required", nameof(pieces));

            return IsSameColor(pieces) || IsSameShape(pieces);
        }

        public virtual bool AreAligned(IBoard board, List<IPiece> pieces)
        {
            if (pieces == null || pieces.Count != 4)
                throw new ArgumentException("Exactly 4 pieces are required", nameof(pieces));

            return IsRow((Board)board, pieces)
                || IsColumn((Board)board, pieces)
                || IsDiagonal1(board, pieces)
                || IsDiagonal2(board, pieces);
        }

        public virtual bool IsQuarto(IBoard board, List<IPiece> pieces)
        {
            return HasCommonAttribute(pieces) && AreAligned(board, pieces);
        }

        public static List<(int row, int col)> GetAvailablePositions(Board board)
        {
            var list = new List<(int row, int col)>();

            for (int row = 0; row < board.SizeX; row++)
                for (int col = 0; col < board.SizeY; col++)
                    if (board.IsEmpty(row, col))
                        list.Add((row, col));

            return list;
        }

        public static bool PlayAMove(IPiece piece, int row, int col, Board board)
        {
            if (!IsMoveValid(board, row, col))
            {
                return false;
            }
            board.InsertPiece(piece, row, col);
            return true;
        }
    }

    public class Rules : RulesBeginner
    {
        private static bool IsSameSize(List<IPiece> pieces)
        {
            bool firstSize = pieces[0].IsBig;
            return pieces.All(p => p.IsBig == firstSize);
        }

        private static bool IsSameState(List<IPiece> pieces)
        {
            bool firstState = pieces[0].IsFull;
            return pieces.All(p => p.IsFull == firstState);
        }

        public override bool HasCommonAttribute(List<IPiece> pieces)
        {
            return base.HasCommonAttribute(pieces)
                || IsSameSize(pieces)
                || IsSameState(pieces);
        }
    }

    public class RulesAdvanced : Rules
    {
        private static bool IsAround(IBoard board, List<IPiece> pieces)
        {
            if (pieces.Count != 4) return false;

            List<(int row, int col)> positions;
            try
            {
                positions = pieces.Select(p => board.PositionPiece(p)).ToList();
            }
            catch (InvalidCastException)
            {
                return false; // Si on ne peut pas caster en Piece
            }

            var min = positions.MinBy(pos => (pos.row, pos.col));

            bool right = positions.Any(pos => pos.row == min.row + 1 && pos.col == min.col);
            bool under = positions.Any(pos => pos.row == min.row && pos.col == min.col + 1);
            bool diagonal = positions.Any(pos => pos.row == min.row + 1 && pos.col == min.col + 1);

            return right && under && diagonal;
        }

        public override bool AreAligned(IBoard board, List<IPiece> pieces)
        {
            return base.AreAligned(board, pieces) || IsAround(board, pieces);
        }
    }
}

