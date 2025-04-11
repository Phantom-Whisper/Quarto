namespace Model
{
    public interface IRules
    {
        bool IsGameOver(Bag bag, Board board);
        bool IsMoveValid(Board board, int x, int y);
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
        public virtual bool IsQuarto(Board board, Piece p1, Piece p2, Piece p3, Piece p4)
        {
            // to do
            return false;
        }
    }
}
