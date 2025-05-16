using Manager;
using Manager.CustomEventArgs;

namespace Model
{
    public class HumanPlayer  : Player
    {
        public HumanPlayer(string name) : base(name) { }

        public override void PlayTurn(IBoard board, IPiece currentPiece, IGameManager gameManager)
        {
            bool placed = false;

            while (!placed)
            {
                gameManager.DisplayMessage($"{Name}, enter the row where you want to place the piece:");
                if (!int.TryParse(Console.ReadLine(), out int row))
                {
                    gameManager.DisplayMessage("Invalid row input.");
                    continue;
                }

                gameManager.DisplayMessage("Enter the column:");
                if (!int.TryParse(Console.ReadLine(), out int col))
                {
                    gameManager.DisplayMessage("Invalid column input.");
                    continue;
                }

                placed = Rules.PlayAMove((Piece)currentPiece, row, col, (Board)board);

                if (!placed)
                {
                    gameManager.DisplayMessage("Invalid move. Try again.");
                }
            }
        }
    }
}
