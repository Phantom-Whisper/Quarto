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

    public class Rules : IRules
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
                && board.PositionXPiece(p3) ==  board.PositionYPiece(p3)
                && board.PositionXPiece(p4) == board.PositionYPiece(p4))
            {
                return true;
            }
            return false;
        }

        public bool IsDiagonal2(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (board.PositionXPiece(p1)+board.PositionYPiece(p1) == board.SizeX-1
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

        public bool IsSameSize(Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (p1.IsBig == p2.IsBig && p1.IsBig == p3.IsBig && p1.IsBig == p4.IsBig)
            {
                return true;
            }
            return false;
        }

        public bool IsSameState(Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (p1.IsFull == p2.IsFull && p1.IsFull == p3.IsFull && p1.IsFull == p4.IsFull)
            {
                return true;
            }
            return false;
        }

        public virtual bool IsQuarto(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (IsSameColor(p1, p2, p3, p4) 
                || IsSameShape(p1, p2, p3, p4) 
                || IsSameSize(p1, p2, p3, p4) 
                || IsSameState(p1, p2, p3, p4))
            {
                if (IsRow(board, p1, p2, p3, p4)
                    || IsColumn(board, p1, p2, p3, p4)
                    || IsDiagonal1(board, p1, p2, p3, p4)
                    || IsDiagonal2(board, p1, p2, p3, p4))
                {
                    return true;
                }
            }
            return false;
        }
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

        public virtual bool IsQuarto(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (IsSameColor(p1, p2, p3, p4)
                || IsSameShape(p1, p2, p3, p4))
            {
                if (IsRow(board, p1, p2, p3, p4)
                    || IsColumn(board, p1, p2, p3, p4)
                    || IsDiagonal1(board, p1, p2, p3, p4)
                    || IsDiagonal2(board, p1, p2, p3, p4))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class RulesAdvanced : IRules
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

        /*public bool IsAround(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            (int x, int y) tP1 = board.PositionPiece(p1);
            (int x, int y) tP2 = board.PositionPiece(p2);
            (int x, int y) tP3 = board.PositionPiece(p3);
            (int x, int y) tP4 = board.PositionPiece(p4);
            List<int> list = new List<int>();

            for (int i = 1; i < 5; i++)
            {
                list.Append();
            }
        }*/

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

        public bool IsSameSize(Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (p1.IsBig == p2.IsBig && p1.IsBig == p3.IsBig && p1.IsBig == p4.IsBig)
            {
                return true;
            }
            return false;
        }

        public bool IsSameState(Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (p1.IsFull == p2.IsFull && p1.IsFull == p3.IsFull && p1.IsFull == p4.IsFull)
            {
                return true;
            }
            return false;
        }

        public virtual bool IsQuarto(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (IsSameColor(p1, p2, p3, p4)
                || IsSameShape(p1, p2, p3, p4)
                || IsSameSize(p1, p2, p3, p4)
                || IsSameState(p1, p2, p3, p4))
            {
                if (IsRow(board, p1, p2, p3, p4)
                    || IsColumn(board, p1, p2, p3, p4)
                    || IsDiagonal1(board, p1, p2, p3, p4)
                    || IsDiagonal2(board, p1, p2, p3, p4)
                    /*|| IsAround(board, p1, p2, p3, p4)*/)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

