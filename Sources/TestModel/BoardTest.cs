using Manager;
using Model;

namespace TestModel
{
    public class BoardTest
    {
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
        public void ToString_ShouldDisplaySinglePiece()
        {
            board.InsertPiece(piece,1, 1);
            var output = board.ToString();

            Assert.Contains("square_full_big_light", output);

            board.ClearBoard();
        }

        [Fact]
        public void ToString_ShouldDisplayMultiplePiecesCorrectly()
        {
            board.InsertPiece(piece, 0, 0);
            board.InsertPiece(piece2, 1, 1);

            var output = board.ToString();

            Assert.Contains("square_full_big_light", output);
            Assert.Contains("square_full_big_dark", output);

            board.ClearBoard();
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
        }

        [Fact]
        public void TestClearBoardWithNoPieces()
        {
            board.ClearBoard();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Assert.Null(board.GetPiece(i, j));
                }
            }
        }

        [Fact]
        public void IsBoardFull_ShouldReturnFalse_WhenBoardIsEmpty()
        {
            var board = new Board(4, 4);
            Assert.False(board.IsBoardFull());
        }

        [Fact]
        public void IsBoardFull_ShouldReturnTrue_WhenBoardIsFull()
        {
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);

            for (int i = 0; i < board.SizeX; i++)
                for (int j = 0; j < board.SizeY; j++)
                    board.InsertPiece(piece, i, j);

            Assert.True(board.IsBoardFull());
        }

        [Fact]
        public void IsBoardFull_ShouldReturnFalse_WhenBoardIsPartiallyFilled()
        {
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);

            // Remplir toutes les cases sauf une
            for (int i = 0; i < board.SizeX; i++)
                for (int j = 0; j < board.SizeY; j++)
                    if (!(i == 3 && j == 3))
                        board.InsertPiece(piece, i, j);

            Assert.False(board.IsBoardFull());
        }

        [Fact]
        public void IsBoardFull_ShouldReturnFalse_AfterClearBoard()
        {
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);

            for (int i = 0; i < board.SizeX; i++)
                for (int j = 0; j < board.SizeY; j++)
                    board.InsertPiece(piece, i, j);

            board.ClearBoard();

            Assert.False(board.IsBoardFull());
        }

        [Fact]
        public void ClearBoard_ShouldSetAllCellsToNull()
        {
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);

            // Remplir toutes les cases
            for (int i = 0; i < board.SizeX; i++)
                for (int j = 0; j < board.SizeY; j++)
                    board.InsertPiece(piece, i, j);

            board.ClearBoard();

            for (int i = 0; i < board.SizeX; i++)
                for (int j = 0; j < board.SizeY; j++)
                    Assert.Null(board.GetPiece(i, j));
        }

        [Fact]
        public void IsBoardFull_ShouldReturnTrue_WhenAllCellsFilled()
        {
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);

            for (int i = 0; i < board.SizeX; i++)
                for (int j = 0; j < board.SizeY; j++)
                    board.InsertPiece(piece, i, j);

            Assert.True(board.IsBoardFull());
        }

        [Fact]
        public void IsBoardFull_ShouldReturnFalse_WhenAtLeastOneCellIsEmpty()
        {
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);

            // Remplir toutes les cases sauf une
            for (int i = 0; i < board.SizeX; i++)
                for (int j = 0; j < board.SizeY; j++)
                    if (!(i == 0 && j == 0))
                        board.InsertPiece(piece, i, j);

            Assert.False(board.IsBoardFull());
        }

        [Fact]
        public void PositionXPiece_ShouldThrow_WhenPieceNotOnBoard()
        {
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);

            var ex = Assert.Throws<InvalidOperationException>(() => board.PositionXPiece(piece));
            Assert.Equal("Piece not in the board !", ex.Message);
        }

        [Fact]
        public void PositionYPiece_ShouldThrow_WhenPieceNotOnBoard()
        {
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);

            var ex = Assert.Throws<InvalidOperationException>(() => board.PositionYPiece(piece));
            Assert.Equal("Piece not in the board !", ex.Message);
        }

        [Fact]
        public void PositionPiece_ShouldThrow_WhenPieceNotOnBoard()
        {
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);

            var ex = Assert.Throws<InvalidOperationException>(() => board.PositionPiece(piece));
            Assert.Equal("Piece not in the board !", ex.Message);
        }
        [Fact]
        public void GetAllPieces_ShouldReturnEmptyList_WhenBoardIsEmpty()
        {
            var board = new Board(4, 4);

            var pieces = board.GetAllPieces();

            Assert.NotNull(pieces);
            Assert.Empty(pieces);
        }

        [Fact]
        public void GetAllPieces_ShouldReturnOnePiece_WhenOnePieceOnBoard()
        {
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);

            board.InsertPiece(piece, 1, 2);

            var pieces = board.GetAllPieces();

            Assert.Single(pieces);
            Assert.Contains(piece, pieces);
        }

        [Fact]
        public void GetAllPieces_ShouldReturnAllPieces_WhenBoardIsPartiallyFilled()
        {
            var board = new Board(4, 4);
            var piece1 = new Piece(true, true, true, true);
            var piece2 = new Piece(false, false, false, false);

            board.InsertPiece(piece1, 0, 0);
            board.InsertPiece(piece2, 3, 3);

            var pieces = board.GetAllPieces();

            Assert.Equal(2, pieces.Count);
            Assert.Contains(piece1, pieces);
            Assert.Contains(piece2, pieces);
        }

        [Fact]
        public void GetAllPieces_ShouldReturnAllPieces_WhenBoardIsFull()
        {
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);

            for (int i = 0; i < board.SizeX; i++)
                for (int j = 0; j < board.SizeY; j++)
                    board.InsertPiece(piece, i, j);

            var pieces = board.GetAllPieces();

            Assert.Equal(board.SizeX * board.SizeY, pieces.Count);
        }

        [Fact]
        public void CombinationsOf4_ShouldReturnNoCombination_WhenListHasLessThan4Pieces()
        {
            var board = new Board(4, 4);
            var pieces = new List<Manager.IPiece>
            {
                new Piece(true, true, true, true),
                new Piece(false, true, true, true),
                new Piece(true, false, true, true)
            };

            var result = board.CombinationsOf4(pieces);

            Assert.Empty(result);
        }

        [Fact]
        public void CombinationsOf4_ShouldReturnOneCombination_WhenListHasExactly4Pieces()
        {
            var board = new Board(4, 4);
            var pieces = new List<Manager.IPiece>
            {
                new Piece(true, true, true, true),
                new Piece(false, true, true, true),
                new Piece(true, false, true, true),
                new Piece(true, true, false, true)
            };

            var result = board.CombinationsOf4(pieces).ToList();

            Assert.Single(result);
            Assert.Equal(4, result[0].Count);
            Assert.All(pieces, p => Assert.Contains(p, result[0]));
        }

        [Fact]
        public void CombinationsOf4_ShouldReturnAllCombinations_WhenListHasMoreThan4Pieces()
        {
            var board = new Board(4, 4);
            var pieces = new List<Manager.IPiece>
            {
                new Piece(true, true, true, true),
                new Piece(false, true, true, true),
                new Piece(true, false, true, true),
                new Piece(true, true, false, true),
                new Piece(false, false, false, false)
            };

            var result = board.CombinationsOf4(pieces).ToList();

            // Il y a C(5,4) = 5 combinaisons possibles
            Assert.Equal(5, result.Count);
            foreach (var combination in result)
            {
                Assert.Equal(4, combination.Count);
                Assert.All(combination, p => Assert.Contains(p, pieces));
            }
        }

        [Fact]
        public void CombinationsOf4_ReturnsEmpty_WhenPiecesIsNull()
        {
            var board = new Board();
            var result = board.CombinationsOf4(null);
            Assert.Empty(result);
        }

        [Fact]
        public void InsertPiece_ShouldRaisePropertyChangedEvent()
        {
            var board = new Board(4, 4);
            string? propertyName = null;
            board.PropertyChanged += (s, e) => propertyName = e.PropertyName;

            var piece = new Piece(true, true, true, true);
            board.InsertPiece(piece, 0, 0);

            Assert.Equal("BoardMatrix", propertyName);
        }


        [Fact]
        public void Cell_Piece_Setter_ShouldRaisePropertyChanged()
        {
            var piece1 = new Piece(true, true, true, true);
            var piece2 = new Piece(false, false, false, false);
            var cell = new Cell(0, 0, piece1);

            string? propertyName = null;
            cell.PropertyChanged += (s, e) => propertyName = e.PropertyName;

            cell.Piece = piece2;

            Assert.Equal(nameof(Cell.Piece), propertyName);
            Assert.Equal(piece2, cell.Piece);
        }

        [Fact]
        public void Cell_Constructor_ShouldSetProperties()
        {
            var piece = new Piece(true, false, true, false);
            var cell = new Cell(1, 2, piece);

            Assert.Equal(1, cell.X);
            Assert.Equal(2, cell.Y);
            Assert.Equal(piece, cell.Piece);
        }
    }
}
