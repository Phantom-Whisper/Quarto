using Manager.CustomEventArgs;
using Manager;
using Model;
using Moq;

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
            public List<string> Messages { get; } = [];
            public List<IPiece> AvailablePieces { get; set; } = [];
            public AskPieceToPlayEventArgs? LastAskArgs { get; private set; }

            public event EventHandler<AskPieceToPlayEventArgs>? AskPieceToPlay;
            public void OnAskPieceToPlay(AskPieceToPlayEventArgs args)
            {
                LastAskArgs = args;
                AskPieceToPlay?.Invoke(this, args); // This is what triggers the event
            }
            public void OnDisplayMessage(string message) => Messages.Add(message);
            public List<IPiece> GetAvailablePieces() => AvailablePieces;

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
                AvailablePieces =
                [
                    new Piece(false, false, false, false)
                ]
            };

            ai.PlayTurn(board, piece, gameManager);

            // Vérifie qu'un message de placement a été affiché
            Assert.Contains(gameManager.Messages, m => m.Contains("placed a piece at (") || m.Contains("Failed to place the piece"));
            // Vérifie qu'une demande de pièce à jouer a été envoyée
            Assert.NotNull(gameManager.LastAskArgs);
            Assert.Equal(ai, gameManager.LastAskArgs.Player);
            Assert.Equal(gameManager.AvailablePieces[0], gameManager.LastAskArgs.PieceToPlay);
        }

        [Fact]
        public void PlayTurn_ShouldPlacePieceOnBoard()
        {
            // Arrange
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);
            var gameManagerMock = new Mock<IGameManager>();

            gameManagerMock.Setup(gm => gm.GetAvailablePieces())
                           .Returns([new Piece(true, true, true, true)]);

            var aiPlayer = new DumbAIPlayer();

            // Act
            aiPlayer.PlayTurn(board, piece, gameManagerMock.Object);

            // Assert
            bool piecePlaced = false;
            for (int r = 0; r < board.SizeX; r++)
            {
                for (int c = 0; c < board.SizeY; c++)
                {
                    if (board.GetPiece(r, c) == piece)
                    {
                        piecePlaced = true;
                        break;
                    }
                }
                if (piecePlaced) break;
            }
            Assert.True(piecePlaced, "The piece should be placed somewhere on the board.");
        }

        [Fact]
        public void PlayTurn_SuccessPath_MessageContainsPlacedPiece()
        {
            var player = new DumbAIPlayer();
            var mockGameManager = new Mock<IGameManager>();

            // Setup GetAvailablePieces to have some pieces to avoid early return
            mockGameManager.Setup(g => g.GetAvailablePieces()).Returns([new Mock<IPiece>().Object]);

            // Track messages
            string displayedMessage = null!;
            mockGameManager.Setup(g => g.OnDisplayMessage(It.IsAny<string>()))
                .Callback<string>(msg => displayedMessage = msg);

            player.PlayTurn(new Board(), new Piece(true,true,true,true), mockGameManager.Object);

            Assert.Contains("placed a piece at", displayedMessage);
        }

        
        [Fact]
        public void PlayTurn_NoAvailablePieces_ShowsNoPiecesMessage()
        {
            var player = new DumbAIPlayer();
            var mockGameManager = new Mock<IGameManager>();

            // Empty pieces list to trigger early return
            mockGameManager.Setup(g => g.GetAvailablePieces()).Returns([]);

            string displayedMessage = null!;
            mockGameManager.Setup(g => g.OnDisplayMessage(It.IsAny<string>()))
                .Callback<string>(msg => displayedMessage = msg);

            player.PlayTurn(new Board(), new Piece(true, true, true, true), mockGameManager.Object);

            Assert.Equal("Dumb AI: No pieces left to give.", displayedMessage);
        }
    }
}
