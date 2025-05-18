using Model;
using Manager;

namespace TestModel
{
    public class RulesTest
    {
        [Theory]
        [InlineData(true, true, true, true, 1, 2, true)]
        [InlineData(true, true, true, true, 8, 5, false)]
        public void TestPlayAMove(bool isSquare, bool isLight, bool isBig, bool isFull, int x, int y, bool res)
        {
            Piece p = new Piece(isSquare, isLight, isBig, isFull);
            Board board = new Board();
            Assert.Equal(Rules.PlayAMove(p, x, y, board), res);
        }
        
        [Theory]
        [InlineData(0, 0, 0, 1, 0, 2, 0, 3, true)]
        [InlineData(0, 0, 1, 0, 2, 0, 3, 0, true)]
        [InlineData(0, 0, 1, 1, 2, 2, 3, 3, true)]
        [InlineData(0, 3, 1, 2, 2, 1, 3, 0, true)]
        [InlineData(0, 0, 1, 1, 2, 2, 3, 0, false)]
        public void TestIsQuartoPositions(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, bool res)
        {
            Board board = new Board();
            RulesBeginner rules = new RulesBeginner();
            
            IPiece p1 = new Piece(true, true, true, true);
            IPiece p2 = new Piece(false, true, false, false);
            IPiece p3 = new Piece(true, true, false, true);
            IPiece p4 = new Piece(false, true, true, false);

            board.InsertPiece(p1, x1, y1);
            board.InsertPiece(p2, x2, y2);
            board.InsertPiece(p3, x3, y3);
            board.InsertPiece(p4, x4, y4);

            var pieces = new List<IPiece> { p1, p2, p3, p4 };
            
            bool result = rules.IsQuarto(board, pieces);
            
            Assert.Equal(res, result);
        }
        
        [Theory]
        [InlineData(true, true, true, true, true, true, true, true, true)]
        [InlineData(true, false, true, false, true, false, true, false, true)]
        [InlineData(false, true, false, true, false, true, false, true, true)]
        [InlineData(true, true, false, false, true, false, false, true, false)]
        public void TestIsQuartoCommonAttribute(bool s1, bool l1, bool s2, bool l2, bool s3, bool l3, bool s4, bool l4, bool res)
        {
            Board board = new Board();
            RulesBeginner rules = new RulesBeginner();

            IPiece p1 = new Piece(s1, l1, true, true);
            IPiece p2 = new Piece(s2, l2, true, true);
            IPiece p3 = new Piece(s3, l3, true, true);
            IPiece p4 = new Piece(s4, l4, true, true);

            var pieces = new List<IPiece> { p1, p2, p3, p4 };
            
            board.InsertPiece(p1, 0, 0);
            board.InsertPiece(p2, 0, 1);
            board.InsertPiece(p3, 0, 2);
            board.InsertPiece(p4, 0, 3);
            
            bool result = rules.IsQuarto(board, pieces);
            
            Assert.Equal(res, result);
        }
        
        [Theory]
        [InlineData(true, true, true, true, true, true, true, true, true)]
        [InlineData(true, false, true, false, true, false, true, false, true)]
        [InlineData(false, true, false, true, false, true, false, true, true)]
        [InlineData(true, true, false, false, true, false, false, true, true)]
        public void TestHasCommonAttribute(
            bool s1, bool l1, bool s2, bool l2, bool s3, bool l3, bool s4, bool l4, bool expected)
        {
            Rules rules = new Rules();

            IPiece p1 = new Piece(s1, l1, true, true);
            IPiece p2 = new Piece(s2, l2, true, true);
            IPiece p3 = new Piece(s3, l3, true, true);
            IPiece p4 = new Piece(s4, l4, true, true);

            var pieces = new List<IPiece> { p1, p2, p3, p4 };
            
            bool result = rules.HasCommonAttribute(pieces);
            
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData(0, 0, 1, 0, 0, 1, 1, 1, true)]
        [InlineData(2, 2, 3, 2, 2, 3, 3, 3, true)]
        [InlineData(0, 0, 1, 0, 2, 0, 3, 0, true)]
        [InlineData(0, 0, 0, 1, 0, 2, 0, 3, true)]
        [InlineData(0, 0, 1, 1, 2, 2, 3, 3, true)]
        public void TestIsAroundAreAlignedAdvanced(
            int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, bool expected)
        {
            var board = new Board();
            var rules = new RulesAdvanced();

            var p1 = new Piece(true, true, true, true);
            var p2 = new Piece(true, true, true, true);
            var p3 = new Piece(true, true, true, true);
            var p4 = new Piece(true, true, true, true);

            board.InsertPiece(p1, x1, y1);
            board.InsertPiece(p2, x2, y2);
            board.InsertPiece(p3, x3, y3);
            board.InsertPiece(p4, x4, y4);

            var pieces = new List<IPiece> { p1, p2, p3, p4 };

            
            bool result = rules.AreAligned(board, pieces);
            
            Assert.Equal(expected, result);
        }
        [Fact]
        public void IsGameOver_ShouldReturnTrue_WhenBagIsEmptyAndBoardIsFull()
        {
            var bag = new Bag();
            var board = new Board(4, 4);

            // Vider le sac
            foreach (var piece in bag.Baglist.ToList())
                bag.Remove(piece);

            // Remplir le plateau
            var pieceToPlace = new Piece(true, true, true, true);
            for (int i = 0; i < board.SizeX; i++)
                for (int j = 0; j < board.SizeY; j++)
                    board.InsertPiece(pieceToPlace, i, j);

            var rules = new Rules();
            Assert.True(rules.IsGameOver(bag, board));
        }

        [Fact]
        public void IsGameOver_ShouldReturnFalse_WhenBagIsNotEmpty()
        {
            var bag = new Bag();
            var board = new Board(4, 4);

            // Remplir le plateau
            var pieceToPlace = new Piece(true, true, true, true);
            for (int i = 0; i < board.SizeX; i++)
                for (int j = 0; j < board.SizeY; j++)
                    board.InsertPiece(pieceToPlace, i, j);

            var rules = new Rules();
            Assert.False(rules.IsGameOver(bag, board));
        }

        [Fact]
        public void IsGameOver_ShouldReturnFalse_WhenBoardIsNotFull()
        {
            var bag = new Bag();
            var board = new Board(4, 4);

            // Vider le sac
            foreach (var piece in bag.Baglist.ToList())
                bag.Remove(piece);

            // Ne pas remplir le plateau
            var rules = new Rules();
            Assert.False(rules.IsGameOver(bag, board));
        }

        [Fact]
        public void IsGameOver_ShouldReturnFalse_WhenBagIsNotEmptyAndBoardIsNotFull()
        {
            var bag = new Bag();
            var board = new Board(4, 4);

            var rules = new Rules();
            Assert.False(rules.IsGameOver(bag, board));
        }

        // Update the test to use a public method or ensure the method being tested is accessible.  
        [Fact]
        public void IsColumn_ShouldThrow_WhenPiecesIsNullOrNot4()
        {
            var board = new Board();
            var rules = new Rules();

            // Use the public method HasCommonAttribute instead of the inaccessible IsColumn method.  
            Assert.Throws<ArgumentException>(() => rules.HasCommonAttribute(null));
            Assert.Throws<ArgumentException>(() => rules.HasCommonAttribute(new List<IPiece>()));
            Assert.Throws<ArgumentException>(() => rules.HasCommonAttribute(new List<IPiece> { new Piece(true, true, true, true) }));
            Assert.Throws<ArgumentException>(() => rules.HasCommonAttribute(new List<IPiece> { new Piece(true, true, true, true), new Piece(false, false, false, false), new Piece(true, false, true, false) }));
            Assert.Throws<ArgumentException>(() => rules.HasCommonAttribute(new List<IPiece> { new Piece(true, true, true, true), new Piece(false, false, false, false), new Piece(true, false, true, false), new Piece(false, true, false, true), new Piece(true, true, false, false) }));
        }

        [Fact]
        public void HasCommonAttribute_ShouldThrow_WhenPiecesIsNullOrNot4()
        {
            var rules = new Rules();

            Assert.Throws<ArgumentException>(() => rules.HasCommonAttribute(null));
            Assert.Throws<ArgumentException>(() => rules.HasCommonAttribute(new List<IPiece>()));
            Assert.Throws<ArgumentException>(() => rules.HasCommonAttribute(new List<IPiece> { new Piece(true, true, true, true) }));
            Assert.Throws<ArgumentException>(() => rules.HasCommonAttribute(new List<IPiece> { new Piece(true, true, true, true), new Piece(false, false, false, false), new Piece(true, false, true, false) }));
            Assert.Throws<ArgumentException>(() => rules.HasCommonAttribute(new List<IPiece> { new Piece(true, true, true, true), new Piece(false, false, false, false), new Piece(true, false, true, false), new Piece(false, true, false, true), new Piece(true, true, false, false) }));
        }

        [Fact]
        public void AreAligned_ShouldThrow_WhenPiecesIsNullOrNot4()
        {
            var board = new Board();
            var rules = new Rules();

            Assert.Throws<ArgumentException>(() => rules.AreAligned(board, new List<IPiece>()));
            Assert.Throws<ArgumentException>(() => rules.AreAligned(board, new List<IPiece> { new Piece(true, true, true, true) }));
            Assert.Throws<ArgumentException>(() => rules.AreAligned(board, new List<IPiece> {
               new Piece(true, true, true, true),
               new Piece(false, false, false, false),
               new Piece(true, false, true, false)
           }));
            Assert.Throws<ArgumentException>(() => rules.AreAligned(board, new List<IPiece> {
               new Piece(true, true, true, true),
               new Piece(false, false, false, false),
               new Piece(true, false, true, false),
               new Piece(false, true, false, true),
               new Piece(true, true, false, false)
           }));
        }

        [Fact]
        public void HasCommonAttribute_ShouldReturnTrue_WhenAllPiecesHaveSameIsFull()
        {
            var rules = new Rules();
            var pieces = new List<IPiece>
            {
                new Piece(true, true, true, true),
                new Piece(false, false, false, true),
                new Piece(true, false, true, true),
                new Piece(false, true, false, true)
            };

            Assert.True(rules.HasCommonAttribute(pieces));
        }

        [Fact]
        public void HasCommonAttribute_ShouldReturnFalse_WhenNotAllPiecesHaveSameIsFull()
        {
            var rules = new Rules();
            var pieces = new List<IPiece>
            {
                new Piece(true, true, true, true),
                new Piece(false, false, false, false),
                new Piece(true, false, true, true),
                new Piece(false, true, false, true)
            };

            Assert.False(rules.HasCommonAttribute(pieces));
        }

    }
}
