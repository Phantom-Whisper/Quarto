using Model;

namespace TestModel
{
    public class BoardTest
    {
        /*
        readonly Board board = new(4, 4);
        readonly Piece piece = new(true, true, true, true);
        readonly Piece piece2 = new(true, false, true, true);


        [Theory]
        [InlineData(4,4,4,4,16)]
        public void TestCtorBoard(int x, int y, int expectedSizex, int expectedSizeZ, int expectedSizeNbCells)
        {
            Board board = new(x, y);
            Assert.NotNull(board);
            Assert.NotNull(board.Grid);
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
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(0, 2)]
        [InlineData(0, 3)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(2, 3)]
        [InlineData(3, 0)]
        [InlineData(3, 1)]
        [InlineData(3, 2)]
        [InlineData(3, 3)]
        public void TestInsertPiece(int x, int y)
        {
            board.InsertPiece(piece, x, y);

            Assert.Equal(board.GetPiece(x,y), piece);

            board.ClearBoard();
        }

        [Theory]
        [InlineData(4, 3)]
        [InlineData(4, 0)]
        [InlineData(3, 4)]
        [InlineData(5, 8)]
        [InlineData(-1, -1)]
        [InlineData(-1, -2)]
        [InlineData(-3, -3)]
        [InlineData(-10, 24)]
        [InlineData(2, 2)]
        public void TestInsertPieceInvalidArgument(int x, int y)
        {
            board.InsertPiece(piece, 2, 2);
            Assert.Throws<InvalidOperationException>(() => { board.InsertPiece(piece, x, y); });

            board.ClearBoard();
        }

        [Theory]
        [InlineData(0,0, true)]
        [InlineData(0, 1, true)]
        [InlineData(0, 2, true)]
        [InlineData(0, 3, true)]
        [InlineData(1, 0, true)]
        [InlineData(1, 1, true)]
        [InlineData(1, 2, true)]
        [InlineData(1, 3, true)]
        [InlineData(2, 0, true)]
        [InlineData(2, 1, true)]
        [InlineData(2, 2, true)]
        [InlineData(2, 3, true)]
        [InlineData(3, 0, true)]
        [InlineData(3, 1, true)]
        [InlineData(3, 2, true)]
        [InlineData(3, 3, true)]
        [InlineData(4, 3, false)]
        [InlineData(4, 0, false)]
        [InlineData(3,4, false)]
        [InlineData(5,8, false)]
        [InlineData(-1, -1, false)]
        [InlineData(-1, -2, false)]
        [InlineData(-3, -3, false)]
        public void TestIsOnBoard(int x, int y, bool expectedBool)
        {
            Assert.Equal(expectedBool, board.IsOnBoard(x, y));
        }


        [Theory]
        [InlineData(3, 3, true)]
        [InlineData(2, 2, false)]
        public void TestIsEmpty(int x, int y, bool expectedBool)
        {
            board.InsertPiece(piece, 2, 2);

            Assert.Equal(expectedBool, board.IsEmpty(x,y));
            
            board.ClearBoard();
        }

        [Fact]
        public void ToString_ShouldHandleEmptyGrid()
        {
            var output = board.ToString();

            Assert.Contains("row/col", output);
            Assert.Contains("  0 |", output);
            Assert.Contains("  1 |", output);
            Assert.Contains(" 0  |", output);
            Assert.Contains(" 1  |", output);
            Assert.DoesNotContain("null", output); // pas de null direct
        }

        [Fact]
        public void ToString_ShouldDisplaySinglePiece()
        {
            board.InsertPiece(piece,1, 1);
            var output = board.ToString();

            Assert.Contains("Square Light Big Full", output);

            board.ClearBoard();
        }

        [Fact]
        public void ToString_ShouldDisplayMultiplePiecesCorrectly()
        {
            board.InsertPiece(piece, 0, 0);
            board.InsertPiece(piece2, 1, 1);

            var output = board.ToString();

            Assert.Contains("Square Light Big Full", output);
            Assert.Contains("Square Dark Big Full", output);

            board.ClearBoard();
        }

        [Fact]
        public void ToString_ShouldFormatHeadersAndSeparatorsCorrectly()
        {
            var output = board.ToString();

            Assert.Contains("row/col |", output);
            Assert.Contains("---", output); // separator lines
            Assert.Contains("  0 |", output);
            Assert.Contains("  1 |", output);
            Assert.Contains("  2 |", output);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 1)]
        [InlineData(0, 2)]
        [InlineData(0, 3)]
        [InlineData(1, 0)]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(2, 0)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(2, 3)]
        [InlineData(3, 0)]
        [InlineData(3, 1)]
        [InlineData(3, 2)]
        [InlineData(3, 3)]
        public void TestGetPiece(int x, int y)
        {
            board.InsertPiece(piece, x, y);
            Assert.Equal(piece, board.GetPiece(x, y));

            board.ClearBoard();
        }

        [Theory]
        [InlineData(4, 3)]
        [InlineData(4, 0)]
        [InlineData(3, 4)]
        [InlineData(5, 8)]
        [InlineData(-1, -1)]
        [InlineData(-1, -2)]
        [InlineData(-3, -3)]
        [InlineData(-10, 24)]
        public void TestGetPieceInvalidArgument(int x, int y)
        {
            Assert.Throws<ArgumentException>(() => { board.GetPiece(x, y); });
        }

        [Fact]
        public void TestClearBoard()
        {
            board.InsertPiece(piece, 2, 2);
            board.InsertPiece(piece, 1, 3);
            board.InsertPiece(piece, 0, 0);
            board.InsertPiece(piece, 3, 1);
            board.InsertPiece(piece, 0, 3);
            board.InsertPiece(piece, 1, 1);
            board.ClearBoard();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Assert.Null(board.GetPiece(i, j));
                }
            }
        }*/
    }
}
