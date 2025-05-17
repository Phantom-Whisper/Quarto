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
        [InlineData(true, true, false, false, true, false, false, true, false)]
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
    }
}
