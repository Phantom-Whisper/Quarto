using Manager;

namespace Model
{
    /// <summary>
    /// Class Rules that verify if the game is played according to the 
    /// beginner rules which means the 4 pieces :
    ///     - has only two characteristic : color(light or dark) and shape(round or square)
    ///     - has to Quarto by being placed in row, column or one of the two diagonals
    /// </summary>
    public class RulesBeginner : IRulesManager
    {
        /// <summary>
        /// method to verify if the game is over by looking if the board is full or if the bag of piece available is empty
        /// </summary>
        /// <param name="bag">bag of pieces available to play</param>
        /// <param name="board">board of the game</param>
        /// <returns>true if it's game over or false if it's not</returns>
        public bool IsGameOver(IBag bag, IBoard board)
        {
            return bag.IsEmpty() && board.IsBoardFull();
        }

        /// <summary>
        /// method to verify if the move chosen is valid by looking if the position is on the board 
        /// and if this position is not already taken
        /// </summary>
        /// <param name="board">board of the game</param>
        /// <param name="row">number of the row chosen</param>
        /// <param name="col">number of the column chosen</param>
        /// <returns>true if the move is valid or false if it's not</returns>
        private static bool IsMoveValid(Board board, int row, int col)
        {
            return board.IsOnBoard(row, col) && board.IsEmpty(row, col);
        }

        /// <summary>
        /// method that verify if pieces are on the same row by verifing that their x position are equals
        /// </summary>
        /// <param name="board">board of the game</param>
        /// <param name="pieces">list of pieces to compare </param>
        /// <returns>true if the 4 pieces are in a row</returns>
        /// <exception cref="ArgumentException"> if their is not 4 pieces in the list </exception>
        private static bool IsRow(Board board, List<IPiece> pieces)
        {
            if (pieces == null || pieces.Count != 4) throw new ArgumentException("Exactly 4 pieces are required.");

            int row = board.PositionXPiece(pieces[0]);
            return pieces.All(p => board.PositionXPiece(p) == row);
        }

        /// <summary>
        /// method that verify if pieces are on the same column by verifing that their y position are equals
        /// </summary>
        /// <param name="board">board of the game</param>
        /// <param name="pieces">list of pieces to compare </param>
        /// <returns>true if the 4 pieces are in a column</returns>
        /// <exception cref="ArgumentException"> if their is not 4 pieces in the list </exception>
        private static bool IsColumn(Board board, List<IPiece> pieces)
        {
            if (pieces == null || pieces.Count != 4) throw new ArgumentException("Exactly 4 pieces are required.");

            int col = board.PositionYPiece(pieces[0]);
            return pieces.All(p => board.PositionYPiece(p) == col);
        }

        /// <summary>
        /// method that verify if pieces are on the same diagonal by verifing that 
        /// </summary>
        /// <param name="board">board of the game</param>
        /// <param name="pieces">list of pieces to compare </param>
        /// <returns>true if the 4 pieces are in the first diagonal</returns>
        /// <exception cref="ArgumentException"> if their is not 4 pieces in the list </exception>
        private static bool IsDiagonal1(IBoard board, List<IPiece> pieces)
        {
            if (pieces == null || pieces.Count != 4) throw new ArgumentException("Exactly 4 pieces are required.");

            return pieces.All(p => board.PositionXPiece(p) == board.PositionYPiece(p));
        }

        /// <summary>
        /// method that verify if pieces are on the same diagonal by verifing that 
        /// </summary>
        /// <param name="board">board of the game</param>
        /// <param name="pieces">list of pieces to compare </param>
        /// <returns>true if the 4 pieces are in the second diagonal</returns>
        /// <exception cref="ArgumentException"> if their is not 4 pieces in the list </exception>
        private static bool IsDiagonal2(IBoard board, List<IPiece> pieces)
        {
            if (pieces == null || pieces.Count != 4) throw new ArgumentException("Exactly 4 pieces are required.");

            int sizeMinusOne = board.SizeX - 1;
            return pieces.All(p => board.PositionXPiece(p) + board.PositionYPiece(p) == sizeMinusOne);
        }

        /// <summary>
        /// method that compare the IsSquare variable of every pieces to see if there are all square or round pieces
        /// </summary>
        /// <param name="pieces">list of pieces to compare</param>
        /// <returns>true if the 4 pieces are the same shape</returns>
        private static bool IsSameShape(List<IPiece> pieces)
        {
            bool firstShape = pieces[0].IsSquare;
            return pieces.All(p => p.IsSquare == firstShape);
        }

        /// <summary>
        /// method that compare the IsLight variable of every pieces to see if there are all light or dark pieces
        /// </summary>
        /// <param name="pieces">list of pieces to compare</param>
        /// <returns>true if the 4 pieces are the same color</returns>
        private static bool IsSameColor(List<IPiece> pieces)
        {
            bool firstColor = pieces[0].IsLight;
            return pieces.All(p => p.IsLight == firstColor);
        }

        /// <summary>
        /// method that compare 4 pieces and tell if there are the same shape (round or square)
        /// or the same color (light or dark)
        /// </summary>
        /// <param name="pieces">list of pieces to compare</param>
        /// <returns> true if it's the same shape or color and false if that is not the case </returns>
        /// <exception cref="ArgumentException"> if their is not 4 pieces in the list </exception>
        public virtual bool HasCommonAttribute(List<IPiece> pieces)
        {
            if (pieces == null || pieces.Count != 4)
                throw new ArgumentException("Exactly 4 pieces are required", nameof(pieces));

            return IsSameColor(pieces) || IsSameShape(pieces);
        }

        /// <summary>
        /// method that compare 4 pieces and tell if there are at a good position for a quarto which means 
        /// in one of the two diagonals or in row or in column
        /// </summary>
        /// <param name="board">board of the game</param>
        /// <param name="pieces">list of pieces to compare </param>
        /// <returns> true if the pieces are in row or column, or one of the two diagonals or false if that is not the case </returns>
        /// <exception cref="ArgumentException"> if their is not 4 pieces in the list </exception>
        public virtual bool AreAligned(IBoard board, List<IPiece> pieces)
        {
            if (pieces == null || pieces.Count != 4)
                throw new ArgumentException("Exactly 4 pieces are required", nameof(pieces));

            return IsRow((Board)board, pieces)
                || IsColumn((Board)board, pieces)
                || IsDiagonal1(board, pieces)
                || IsDiagonal2(board, pieces);
        }

        /// <summary>
        /// method verifing if there is a quarto according to the rules of the beginner level
        /// </summary>
        /// <param name="board">board of the game</param>
        /// <param name="pieces">list of pieces to compare </param>
        /// <returns>if there is a Quarto (true) or not (false) </returns>
        public virtual bool IsQuarto(IBoard board, List<IPiece> pieces)
        {
            return HasCommonAttribute(pieces) && AreAligned(board, pieces);
        }

        /// <summary>
        /// method to know which position has not already been played
        /// </summary>
        /// <param name="board">board of the game</param>
        /// <returns>the position available to put a <c> Piece</c></returns>
        public static List<(int row, int col)> GetAvailablePositions(Board board)
        {
            var list = new List<(int row, int col)>();

            for (int row = 0; row < board.SizeX; row++)
                for (int col = 0; col < board.SizeY; col++)
                    if (board.IsEmpty(row, col))
                        list.Add((row, col));

            return list;
        }


        /// <summary>
        /// method to play a piece with a verification if the move is valid by looking if the position is on the board 
        /// and if this position is not already taken
        /// </summary>
        /// <param name="piece"><c>Piece</c> chosen by the opponent to be played</param>
        /// <param name="row">row chosen to put the piece</param>
        /// <param name="col">column chosen to put the piece</param>
        /// <param name="board">board of the game</param>
        /// <returns>if the move is played or not due to the validity of the move </returns>
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

    /// <summary>
    /// Class Rules that verify if the game is played according to the 
    /// normal rules which means the 4 pieces :
    ///     - has four characteristic : color(light or dark), shape(round or square), Size(big or small) and state(full or hollow)
    ///     - has to Quarto by being placed in row, column or one of the two diagonals
    /// </summary>
    public class Rules : RulesBeginner
    {
        /// <summary>
        /// method that compare the IsBig variable of every pieces to see if there are all big or small pieces
        /// </summary>
        /// <param name="pieces">list of pieces to compare</param>
        /// <returns>true if the 4 pieces are the same size</returns>
        private static bool IsSameSize(List<IPiece> pieces)
        {
            bool firstSize = pieces[0].IsBig;
            return pieces.All(p => p.IsBig == firstSize);
        }

        /// <summary>
        /// method that compare the IsFull variable of every pieces to see if there are all full or hollow pieces
        /// </summary>
        /// <param name="pieces">list of pieces to compare</param>
        /// <returns>true if the 4 pieces are the same State</returns>
        private static bool IsSameState(List<IPiece> pieces)
        {
            bool firstState = pieces[0].IsFull;
            return pieces.All(p => p.IsFull == firstState);
        }

        /// <summary>
        /// method that compare 4 pieces and tell if there are the same shape (round or square)
        /// or the same color (light or dark) or the same size (big or smal or the same state (full or hollow)
        /// </summary>
        /// <param name="pieces">list of pieces to compare</param>
        /// <returns> true if it's the same shape or color or size or state and false if that is not the case </returns>
        /// <exception cref="ArgumentException"> inherits from the class <c>RulesBeginner</c> the exeption if their is not 4 pieces in the list </exception>
        public override bool HasCommonAttribute(List<IPiece> pieces)
        {
            return base.HasCommonAttribute(pieces)
                || IsSameSize(pieces)
                || IsSameState(pieces);
        }
    }

    /// <summary>
    /// Class Rules that verify if the game is played according to the 
    /// advanced rules which means the 4 pieces :
    ///     - has four characteristic : color(light or dark), shape(round or square), Size(big or small) and state(full or hollow)
    ///     - has to Quarto by being placed in row, column, one of the two diagonals or in a square zone
    /// </summary>
    public class RulesAdvanced : Rules
    {
        /// <summary>
        /// method that compare 4 pieces and tell if there are in a square zone 
        /// </summary>
        /// <param name="board">board of the game</param>
        /// <param name="pieces">list of pieces to compare </param>
        /// <returns> true if there are in a square zone or false if that is not the case </returns>
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
                return false; // If we can't cast to Piece
            }

            var min = positions.MinBy(pos => (pos.row, pos.col));

            bool right = positions.Any(pos => pos.row == min.row + 1 && pos.col == min.col);
            bool under = positions.Any(pos => pos.row == min.row && pos.col == min.col + 1);
            bool diagonal = positions.Any(pos => pos.row == min.row + 1 && pos.col == min.col + 1);

            return right && under && diagonal;
        }

        /// <summary>
        /// method that compare 4 pieces and tell if there are at a good position for a quarto which means 
        /// in one of the two diagonals or in row or in column or in a square zone
        /// </summary>
        /// <param name="board">board of the game</param>
        /// <param name="pieces">list of pieces to compare </param>
        /// <returns> true if the pieces are in row or column, or one of the two diagonals or in a square zone and false if that is not the case </returns>
        /// <exception cref="ArgumentException"> inherits from the class <c>Rules</c> the exeption if their is not 4 pieces in the list </exception>
        public override bool AreAligned(IBoard board, List<IPiece> pieces)
        {
            return base.AreAligned(board, pieces) || IsAround(board, pieces);
        }
    }
}

