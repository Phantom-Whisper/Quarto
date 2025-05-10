namespace Model
{
    public interface IRules
    {
        bool IsGameOver(Bag bag, Board board);
        bool IsMoveValid(Board board, int x, int y);
        bool IsRow(Board board, Piece p1, Piece p2, Piece p3, Piece p4);
        bool IsColumn(Board board, Piece p1, Piece p2, Piece p3, Piece p4);
        bool IsDiagonal1(Board board, Piece p1, Piece p2, Piece p3, Piece p4);
        bool IsDiagonal2(Board board, Piece p1, Piece p2, Piece p3, Piece p4);
        bool IsSameShape(Piece p1, Piece p2, Piece p3, Piece p4);
        bool IsSameColor(Piece p1, Piece p2, Piece p3, Piece p4);
        bool IsQuarto(Board board, Piece p1, Piece p2, Piece p3, Piece p4);
    }

    public class RulesBeginner : IRules
    {
        public bool IsGameOver(Bag bag, Board board)
        {
            if (bag.IsEmpty() && board.IsBoardFull()) return true;
            return false;
        }

        public bool IsMoveValid(Board board, int x, int y)
        {
            return board.IsOnBoard(x, y);
        }

        public bool IsRow(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (board.PositionXPiece(p1) == board.PositionXPiece(p2)
                && board.PositionXPiece(p1) == board.PositionXPiece(p3)
                && board.PositionXPiece(p1) == board.PositionXPiece(p4))
            {
                return true;
            }
            return false;
        }

        public bool IsColumn(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (board.PositionYPiece(p1) == board.PositionYPiece(p2)
                && board.PositionYPiece(p1) == board.PositionYPiece(p3)
                && board.PositionYPiece(p1) == board.PositionYPiece(p4))
            {
                return true;
            }
            return false;
        }

        public bool IsDiagonal1(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (board.PositionXPiece(p1) == board.PositionYPiece(p1)
                && board.PositionXPiece(p2) == board.PositionYPiece(p2)
                && board.PositionXPiece(p3) == board.PositionYPiece(p3)
                && board.PositionXPiece(p4) == board.PositionYPiece(p4))
            {
                return true;
            }
            return false;
        }

        public bool IsDiagonal2(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (board.PositionXPiece(p1) + board.PositionYPiece(p1) == board.SizeX - 1
                && board.PositionXPiece(p2) + board.PositionYPiece(p2) == board.SizeX - 1
                && board.PositionXPiece(p3) + board.PositionYPiece(p3) == board.SizeX - 1
                && board.PositionXPiece(p4) + board.PositionYPiece(p4) == board.SizeX - 1)
            {
                return true;
            }
            return false;
        }

        public bool IsSameShape(Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (p1.IsSquare == p2.IsSquare && p1.IsSquare == p3.IsSquare && p1.IsSquare == p4.IsSquare)
            {
                return true;
            }
            return false;
        }

        public bool IsSameColor(Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (p1.IsLight == p2.IsLight && p1.IsLight == p3.IsLight && p1.IsLight == p4.IsLight)
            {
                return true;
            }
            return false;
        }

        public virtual bool HasCommonAttribute(Piece p1, Piece p2, Piece p3, Piece p4)
        {
            return IsSameColor(p1, p2, p3, p4) || IsSameShape(p1, p2, p3, p4);
        }

        public virtual bool AreAligned(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            return IsRow(board, p1, p2, p3, p4)
                || IsColumn(board, p1, p2, p3, p4)
                || IsDiagonal1(board, p1, p2, p3, p4)
                || IsDiagonal2(board, p1, p2, p3, p4);
        }

        public virtual bool IsQuarto(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            return HasCommonAttribute(p1, p2, p3, p4) && AreAligned(board, p1, p2, p3, p4);
        }
    }

    public class Rules : RulesBeginner
    {

        public static bool IsSameSize(Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (p1.IsBig == p2.IsBig && p1.IsBig == p3.IsBig && p1.IsBig == p4.IsBig)
            {
                return true;
            }
            return false;
        }

        public static bool IsSameState(Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (p1.IsFull == p2.IsFull && p1.IsFull == p3.IsFull && p1.IsFull == p4.IsFull)
            {
                return true;
            }
            return false;
        }

        public override bool HasCommonAttribute(Piece p1, Piece p2, Piece p3, Piece p4)
        {
            return base.HasCommonAttribute(p1, p2, p3, p4) || IsSameSize(p1, p2, p3, p4)
                || IsSameState(p1, p2, p3, p4);
        }
    }

    public class RulesAdvanced : Rules
    {

        /// <summary>
        /// Method that tells if the 4 pieces selected are around each other by comparing their position
        /// </summary>
        /// <param name="board"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <returns></returns>
        public static bool IsAround(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            (int x, int y) min = (int.MaxValue, int.MaxValue);
            List<(int x, int y)> list = new List<(int x, int y)>();
            bool right = false, under = false, diagonal = false;

            try
            {
                list.Add(board.PositionPiece(p1));
                list.Add(board.PositionPiece(p2));
                list.Add(board.PositionPiece(p3));
                list.Add(board.PositionPiece(p4));
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            
            foreach (var pos in list)
            {
                if (pos.x < min.x || (pos.x == min.x && pos.y < min.y))
                {
                    min = pos;
                }
            }

            foreach (var pos in list)
            {
                if (pos == (min.x + 1, min.y + 1))
                    diagonal = true;
                else if (pos == (min.x + 1, min.y))
                    right = true;
                else if (pos == (min.x, min.y + 1))
                    under = true;
            }

                return diagonal && right && under;
        }

        public override bool AreAligned(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            return base.AreAligned(board, p1, p2, p3, p4)
                || IsAround(board, p1, p2, p3, p4);
        }
    }
}

