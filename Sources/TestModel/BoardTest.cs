using Model;

namespace TestModel
{
    public class BoardTest
    {

        [Theory]
        [InlineData(4,4,4,4,16)]
        public void TestCtorBoard(int x, int y, int expectedSizex, int expectedSizeZ, int expectedSizeNbCells)
        {
            Board board = new(x, y);
            Assert.NotNull(board);
            Assert.NotNull(board.grid);
            Assert.Equal(expectedSizex, board.SizeX);
            Assert.Equal(expectedSizeZ, board.SizeY);
            Assert.Equal(expectedSizeNbCells, board.NbCells);
        }

        [Theory]
        [InlineData(4, 3)]
        [InlineData(3, 4)]
        [InlineData(0, 4)]
        [InlineData(7, 0)]
        [InlineData(-4, 8)]
        [InlineData(4, -4)]
        public void TestCtorBoardInvalidArguments(int x, int y)
        {            
            Assert.Throws<ArgumentException>(() => { Board board = new(x, y); });
        }

        [Theory]
        [InlineData(2,2, true)]
        [InlineData(4, 3, false)]
        public void TestIsOnBoard(int x, int y, bool expectedBool)
        {
            Board board = new(4,4);

            Assert.Equal(expectedBool, board.IsOnBoard(x, y));
        }


        [Theory]
        [InlineData(3, 3, true)]
        [InlineData(2, 2, false)]
        public void TestIsEmpty(int x, int y, bool expectedBool)
        {
            Board board = new(4,4);
            Piece piece = new(true, true, true, true);

            board.InsertPiece(piece, 2, 2);

            Assert.Equal(expectedBool, board.IsEmpty(x,y));
        }
    }
}
