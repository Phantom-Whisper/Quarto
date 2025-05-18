using Manager;
using Manager.CustomEventArgs;
using Model;

namespace TestModel
{
    public class PlayerTest
    {
        [Theory]
        [InlineData("Shadow Fox")]
        [InlineData("LunaSkye")]
        [InlineData("Pixel_Knight")]
        [InlineData("Z3r0Blade")]
        public void TestCtorPlayer_ValidNames(string pseudo)
        {
            HumanPlayer player = new(pseudo);
            Assert.NotNull(player);
            Assert.NotNull(player.Name); // Or player.Name if needed
            Assert.Equal(pseudo, player.Name);
        }

        [Fact]
        public void TestPseudoProperty()
        {
            var player = new HumanPlayer("JoueurTest");
            Assert.IsType<string>(player.Name);
            Assert.Equal("JoueurTest", player.Name);
        }

        private class TestGameManager : IGameManager
        {
            public (int, int) CoordinatesToReturn { get; set; }
            public List<string> Messages { get; } = new();

            public event EventHandler<AskPieceToPlayEventArgs>? AskPieceToPlay;

            public (int, int) RequestCoordinates(IPlayer player) => CoordinatesToReturn;
            public void OnDisplayMessage(string message) => Messages.Add(message);

            public void OnAskPieceToPlay(AskPieceToPlayEventArgs args)
            {
                throw new NotImplementedException();
            }

            public List<IPiece> GetAvailablePieces()
            {
                throw new NotImplementedException();
            }

            public void Run()
            {
                throw new NotImplementedException();
            }

        }


        [Fact]
        public void PlayTurn_ShouldPlacePiece_WhenMoveIsValid()
        {
            var player = new HumanPlayer("Test");
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);
            var gameManager = new TestGameManager { CoordinatesToReturn = (1, 1) };

            player.PlayTurn(board, piece, gameManager);

            Assert.Equal(piece, board.GetPiece(1, 1));
            Assert.Empty(gameManager.Messages);
        }


        [Fact]
        public void PlayTurn_ShouldDisplayMessage_WhenMoveIsInvalid()
        {
            var player = new HumanPlayer("Test");
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);
            var gameManager = new TestGameManager { CoordinatesToReturn = (5, 5) }; // Hors plateau

            player.PlayTurn(board, piece, gameManager);

            Assert.Null(board.GetPiece(1, 1)); // (1,1) n'a pas été joué
            Assert.Single(gameManager.Messages);
            Assert.Contains("Invalid move", gameManager.Messages[0]);
        }
    }
 }
