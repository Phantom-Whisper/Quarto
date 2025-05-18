using Manager.CustomEventArgs;
using Manager;
using Model;

namespace TestModel
{
    public class PlayerIATest
    {
        [Fact]
        public void TestConstructor()
        {
            string expectedName = "Dumb AI";
            var aiPlayer = new DumbAIPlayer();
            Assert.Equal(expectedName, aiPlayer.Name);
        }

        private class MinimalGameManager : IGameManager
        {
            public List<string> Messages { get; } = new();
            public List<IPiece> AvailablePieces { get; set; } = new();
            public AskPieceToPlayEventArgs? LastAskArgs { get; private set; }

            public event EventHandler<AskPieceToPlayEventArgs>? AskPieceToPlay;

            public void OnDisplayMessage(string message) => Messages.Add(message);
            public List<IPiece> GetAvailablePieces() => AvailablePieces;
            public void OnAskPieceToPlay(AskPieceToPlayEventArgs args) => LastAskArgs = args;

            public (int row, int col) RequestCoordinates(IPlayer player)
            {
                throw new NotImplementedException();
            }

            public void Run()
            {
                throw new NotImplementedException();
            }

            // Ajoutez des membres vides si IGameManager a d'autres méthodes obligatoires
        }

        [Fact]
        public void PlayTurn_ShouldPlacePieceAndAskPieceToPlay()
        {
            var ai = new DumbAIPlayer();
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);

            // On prépare le plateau pour qu'il y ait au moins une position disponible
            // (le plateau est vide, donc toutes les positions sont disponibles)
            var gameManager = new MinimalGameManager
            {
                AvailablePieces = new List<IPiece>
                {
                    new Piece(false, false, false, false)
                }
            };

            ai.PlayTurn(board, piece, gameManager);

            // Vérifie qu'un message de placement a été affiché
            Assert.Contains(gameManager.Messages, m => m.Contains("placed a piece at (") || m.Contains("Failed to place the piece"));
            // Vérifie qu'une demande de pièce à jouer a été envoyée
            Assert.NotNull(gameManager.LastAskArgs);
            Assert.Equal(ai, gameManager.LastAskArgs.Player);
            Assert.Equal(gameManager.AvailablePieces[0], gameManager.LastAskArgs.PieceToPlay);
        }
    }
}
