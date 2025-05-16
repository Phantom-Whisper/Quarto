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
    }
}
