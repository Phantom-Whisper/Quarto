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

        [Fact]
        public async Task PlayTurn_ShouldReturnValidPosition_WhenPositionsAvailable()
        {
            var aiPlayer = new DumbAIPlayer();
            var board = new Board();
            var piece = new Piece(true, true, true, true);

            // On laisse toutes les cases vides
            var result = await aiPlayer.PlayTurn(board, piece);

            Assert.NotNull(result);
            var available = Rules.GetAvailablePositions(board);
            Assert.Contains(result.Value, available);
        }

        [Fact]
        public async Task PlayTurn_ShouldReturnNull_WhenNoPositionsAvailable()
        {
            var aiPlayer = new DumbAIPlayer();
            var board = new Board();
            var piece = new Piece(true, true, true, true);

            // Remplir toutes les cases
            for (int i = 0; i < board.SizeX; i++)
                for (int j = 0; j < board.SizeY; j++)
                    board.InsertPiece(piece, i, j);

            var result = await aiPlayer.PlayTurn(board, piece);

            Assert.Null(result);
        }
    }
}
