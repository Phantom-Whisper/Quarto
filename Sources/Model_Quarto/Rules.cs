using Manager;

namespace Model
{
    public class RulesBeginner : IRulesManager
    {
        public IPlayer? IsGameOver(IBag bag, IBoard board) // Chiant -_-
        {
            // Faut vérifier si il y a quarto mais ça implique une conversion de IPiece en Piece 
            // Ou de passer des méthodes de caractéristiques communes ailleurs
            if (bag.IsEmpty() && board.IsBoardFull()) return null; 
            //if(IsQuarto(board, p1, p2, p3, p4))
               // return null;
            return null;
        }

        private bool IsMoveValid(Board board, int row, int col)
        {
            return board.IsOnBoard(row, col);
        }

        private bool IsRow(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (board.PositionXPiece(p1) == board.PositionXPiece(p2)
                && board.PositionXPiece(p1) == board.PositionXPiece(p3)
                && board.PositionXPiece(p1) == board.PositionXPiece(p4))
            {
                return true;
            }
            return false;
        }

        private bool IsColumn(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (board.PositionYPiece(p1) == board.PositionYPiece(p2)
                && board.PositionYPiece(p1) == board.PositionYPiece(p3)
                && board.PositionYPiece(p1) == board.PositionYPiece(p4))
            {
                return true;
            }
            return false;
        }

        private bool IsDiagonal1(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
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

        private bool IsDiagonal2(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
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

        private bool IsSameShape(Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (p1.IsSquare == p2.IsSquare && p1.IsSquare == p3.IsSquare && p1.IsSquare == p4.IsSquare)
            {
                return true;
            }
            return false;
        }

        private bool IsSameColor(Piece p1, Piece p2, Piece p3, Piece p4)
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

        public virtual IPlayer? IsQuarto(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if(HasCommonAttribute(p1, p2, p3, p4) && AreAligned(board, p1, p2, p3, p4))
                return null; // Return the current player (who called the Quarto verif)
            return null;
        }

        public List<(int row, int col)> GetAvailablePositions(Board board)
        {
            var list = new List<(int row, int col)>();

            for (int row = 0; row < board.SizeX; row++)
            {
                for (int col = 0; col < board.SizeY; col++)
                {
                    if (board.IsEmpty(row, col))
                    {
                        list.Add((row, col));
                    }
                }
            }

            return list;
        }

        public void PlayAMove(Piece piece, int row)
        { 

        }
    }

    public class Rules : RulesBeginner
    {

        private bool IsSameSize(Piece p1, Piece p2, Piece p3, Piece p4)
        {
            if (p1.IsBig == p2.IsBig && p1.IsBig == p3.IsBig && p1.IsBig == p4.IsBig)
            {
                return true;
            }
            return false;
        }

        private bool IsSameState(Piece p1, Piece p2, Piece p3, Piece p4)
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
        private bool IsAround(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            List<(int row, int col)> list;
            bool right = false, under = false, diagonal = false;

            try
            {
                list = new List<(int row, int col)>
                {
                    board.PositionPiece(p1),
                    board.PositionPiece(p2),
                    board.PositionPiece(p3),
                    board.PositionPiece(p4)
                };
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            
            var min = list[0];
            foreach (var pos in list)
            {
                if (pos.row < min.row || (pos.row == min.row && pos.col < min.col))
                {
                    min = pos;
                }
            }

            foreach (var pos in list)
            {
                if (pos.row == min.row + 1 && pos.col == min.col) right = true;
                else if (pos.row == min.row && pos.col == min.col + 1) under = true;
                else if (pos.row == min.row + 1 && pos.col == min.col + 1) diagonal = true;
            }

            return right && under && diagonal;
        }

        public override bool AreAligned(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            return base.AreAligned(board, p1, p2, p3, p4)
                || IsAround(board, p1, p2, p3, p4);
        }
    }
}

