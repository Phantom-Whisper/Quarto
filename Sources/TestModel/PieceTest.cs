using Model;

namespace TestModel
{
    public class PieceTest
    {
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
        [InlineData(true, true, true, true, "square_full_big_light")]
        [InlineData(true, true, true, false, "square_hollow_big_light")]
        [InlineData(true, true, false, true, "square_full_small_light")]
        [InlineData(true, true, false, false, "square_hollow_small_light")]
        [InlineData(true, false, true, true, "square_full_big_dark")]
        [InlineData(true, false, true, false, "square_hollow_big_dark")]
        [InlineData(true, false, false, true, "square_full_small_dark")]
        [InlineData(true, false, false, false, "square_hollow_small_dark")]
        [InlineData(false, true, true, true, "round_full_big_light")]
        [InlineData(false, true, true, false, "round_hollow_big_light")]
        [InlineData(false, true, false, true, "round_full_small_light")]
        [InlineData(false, true, false, false, "round_hollow_small_light")]
        [InlineData(false, false, true, true, "round_full_big_dark")]
        [InlineData(false, false, true, false, "round_hollow_big_dark")]
        [InlineData(false, false, false, true, "round_full_small_dark")]
        [InlineData(false, false, false, false, "round_hollow_small_dark")]
        public void TestToString(bool isSquare, bool isLight, bool isBig, bool isFull, string comparable)
        {
            Piece piece = new Piece(isSquare, isLight, isBig, isFull);
            Assert.Equal(piece.ToString(), comparable);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenObjectIsNotPiece()
        {
            var piece = new Piece(false, true, true, false);
            var notPiece = "Not a piece";

            Assert.False(piece.Equals(notPiece));
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenPropertiesDiffer()
        {
            var piece1 = new Piece(true, true, true, true);
            var piece2 = new Piece(false, false, false, false);

            Assert.False(piece1.Equals(piece2));
        }

        [Fact]
        public void Equals_ReturnsTrue_WhenAllPropertiesMatch()
        {
            var piece1 = new Piece(true, true, true, true);
            var piece2 = new Piece(true, true, true, true);

            Assert.True(piece1.Equals(piece2));
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

        [Fact]
        public void TestEquals_WithNull_False()
        {
            var piece = new Piece(true, true, true, true);

            bool result = piece.Equals(null);

            Assert.False(result);
        }

        [Fact]
        public void Equals_ObjectIsNull_ReturnsFalse()
        {
            var piece = new Piece(true, false, true, false);
            bool result = piece.Equals((object?)null);

            Assert.False(result);
        }


    }
}