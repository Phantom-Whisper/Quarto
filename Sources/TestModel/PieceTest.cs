using Model;

namespace TestModel
{
    public class PieceTest
    {
        public static TheoryData<object, object, bool> EqualsTestData => new()        
        {
            { new Piece(true, true, false, true), new Board(4, 4), false },
            { new Piece(true, true, false, true), new Piece(true, true, false, true), true }
        };

        [Theory]
        [InlineData(true, true, true, true)]
        [InlineData(true, true, true, false)]
        [InlineData(true, true, false, true)]
        [InlineData(true, true, false, false)]
        [InlineData(true, false, true, true)]
        [InlineData(true, false, true, false)]
        [InlineData(true, false, false, true)]
        [InlineData(true, false, false, false)]
        [InlineData(false, true, true, true)]
        [InlineData(false, true, true, false)]
        [InlineData(false, true, false, true)]
        [InlineData(false, true, false, false)]
        [InlineData(false, false, true, true)]
        [InlineData(false, false, true, false)]
        [InlineData(false, false, false, true)]
        [InlineData(false, false, false, false)]
        public void TestConstructeur(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Piece piece = new Piece(isSquare, isLight, isBig, isFull);
            Assert.NotNull(piece);
            Assert.Equal(isSquare, piece.IsSquare);
            Assert.Equal(isLight, piece.IsLight);
            Assert.Equal(isBig, piece.IsBig);
            Assert.Equal(isFull, piece.IsFull);
        }

        [Theory]
        [InlineData(true, true, true, true, "Square Light Big Full")]
        [InlineData(true, true, true, false, "Square Light Big Hollow")]
        [InlineData(true, true, false, true, "Square Light Small Full")]
        [InlineData(true, true, false, false, "Square Light Small Hollow")]
        [InlineData(true, false, true, true, "Square Dark Big Full")]
        [InlineData(true, false, true, false, "Square Dark Big Hollow")]
        [InlineData(true, false, false, true, "Square Dark Small Full")]
        [InlineData(true, false, false, false, "Square Dark Small Hollow")]
        [InlineData(false, true, true, true, "Round Light Big Full")]
        [InlineData(false, true, true, false, "Round Light Big Hollow")]
        [InlineData(false, true, false, true, "Round Light Small Full")]
        [InlineData(false, true, false, false, "Round Light Small Hollow")]
        [InlineData(false, false, true, true, "Round Dark Big Full")]
        [InlineData(false, false, true, false, "Round Dark Big Hollow")]
        [InlineData(false, false, false, true, "Round Dark Small Full")]
        [InlineData(false, false, false, false, "Round Dark Small Hollow")]
        public void TestToString(bool isSquare, bool isLight, bool isBig, bool isFull, string comparable)
        {
            Piece piece = new Piece(isSquare, isLight, isBig, isFull);
            Assert.Equal(piece.ToString(), comparable);
        }

        [Theory]
        [MemberData(nameof(EqualsTestData))]
        public void TestIsEqualsObject(object obj1, object obj2, bool expectedResult)
        {                       
            var result = obj1.Equals(obj2);

            Assert.Equal(result, expectedResult);
        }

        [Theory]
        [InlineData(true, true, true, true)]
        [InlineData(true, true, true, false)]
        [InlineData(true, true, false, true)]
        [InlineData(true, true, false, false)]
        [InlineData(true, false, true, true)]
        [InlineData(true, false, true, false)]
        [InlineData(true, false, false, true)]
        [InlineData(true, false, false, false)]
        [InlineData(false, true, true, true)]
        [InlineData(false, true, true, false)]
        [InlineData(false, true, false, true)]
        [InlineData(false, true, false, false)]
        [InlineData(false, false, true, true)]
        [InlineData(false, false, true, false)]
        [InlineData(false, false, false, true)]
        [InlineData(false, false, false, false)]
        public void TestGetHashCodePiece(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Piece piece = new Piece(isSquare, isLight, isBig, isFull);
            var hash = piece.GetHashCode();

            Assert.Equal(hash, piece.GetHashCode());
        }
    }
}