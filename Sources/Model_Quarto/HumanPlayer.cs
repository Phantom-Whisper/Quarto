using Manager;
using Manager.CustomEventArgs;

namespace Model
{
    public class HumanPlayer  : Player
    {
        public HumanPlayer(string name) : base(name) { }

        public override IPiece? PlayTurn(IBoard board, IPiece currentPiece, IGameManager gameManager)
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

            return ChoosePieceForOpponent(gameManager);
        }

        private IPiece ChoosePieceForOpponent(IGameManager gameManager)
        {
            IPiece? selectedPiece = null;
            var availablePieces = gameManager.GetAvailablePieces();

            while (selectedPiece == null)
            {
                gameManager.DisplayMessage("Choose a piece to give to your opponent:");
                gameManager.DisplayMessage(availablePieces.ToString());

                Console.Write("Enter the number of the piece: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int index))
                {
                    index -= 1;
                    if (index >= 0 && index < availablePieces.Count)
                    {
                        selectedPiece = availablePieces[index];
                        return selectedPiece;
                    }
                }

                gameManager.DisplayMessage("Invalid selection. Try again.");
            }

            return selectedPiece!; // never null at this point
        }
    }
}
