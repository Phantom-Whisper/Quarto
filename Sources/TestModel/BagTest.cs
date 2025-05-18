using Model;
using Xunit;

namespace TestModel
{
    public class BagTest
    {
        [Fact]
        public void TestCtor_ShouldInitializeWith16Pieces()
        {
            var bag = new Bag();
            Assert.Equal(16, bag.Baglist.Count);
        }

        [Fact]
        public void AddPiece_Duplicate_Exception()
        {
            var bag = new Bag();
            var piece = new Piece(false, false, false, false);
            bag.Remove(piece); 
            bag.AddPiece(piece);
            Assert.Throws<InvalidOperationException>(() => bag.AddPiece(piece));
        }


        [Theory]
        [InlineData(true, true, true, true)]
        [InlineData(false, false, true, true)]
        [InlineData(true, true, false, false)]
        public void TestAddPiece_ShouldAddCorrectPiece(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Bag bag = new Bag();
            Piece piece = new Piece(isSquare, isLight, isBig, isFull);
            bag.Remove(piece); 
            bag.AddPiece(piece);
            Assert.Contains(piece, bag.Baglist);
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
        public void AddPiece_ContainsPieceWithCorrectProperties(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Bag bag = new Bag();
            Piece piece = new Piece(isSquare, isLight, isBig, isFull);
            bag.Remove(piece); 
            bag.AddPiece(piece);

            var foundPiece = bag.Baglist.FirstOrDefault(p =>
                p.IsSquare == isSquare &&
                p.IsLight == isLight &&
                p.IsBig == isBig &&
                p.IsFull == isFull);

            Assert.NotNull(foundPiece);
        }

        [Fact]
        public void TakePiece_Null_Exception()
        {
            Bag bag = new Bag();
            Assert.Throws<ArgumentNullException>(() => bag.TakePiece(null));
        }

        [Fact]
        public void TakePiece_NotInBag_ReturnNull()
        {
            Bag bag = new Bag();
            var piece = new Piece(false, false, false, false);
            bag.Remove(piece); 
            var result = bag.TakePiece(piece);
            Assert.Null(result);
        }

        [Theory]
        [InlineData(true, true, true, true)]
        [InlineData(false, false, true, true)]
        [InlineData(true, true, false, false)]
        public void TakePiece_RemovePiece(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Bag bag = new Bag();
            var piece = new Piece(isSquare, isLight, isBig, isFull);
            bag.Remove(piece); // prevent dupe exception
            bag.AddPiece(piece);
            var taken = bag.TakePiece(piece);

            Assert.Equal(piece, taken);
            Assert.DoesNotContain(piece, bag.Baglist);
        }

        [Fact]
        public void IsEmpty_ShouldReturnTrue_WhenBagIsEmpty()
        {
            var bag = new Bag();
            foreach (var piece in bag.Baglist.ToList())
                bag.Remove(piece);

            Assert.True(bag.IsEmpty());
        }

        [Fact]
        public void IsEmpty_ShouldReturnFalse_WhenBagIsNotEmpty()
        {
            var bag = new Bag();
            Assert.False(bag.IsEmpty());
        }
        [Fact]
        public void ToString_ShouldListAllPiecesWithCorrectFormat()
        {
            var bag = new Bag();
            var output = bag.ToString();

            // Vérifie qu'il y a bien 16 lignes (une par pièce)
            var lines = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(16, lines.Length);

            // Vérifie le format de la première ligne
            Assert.StartsWith("1. ", lines[0]);
            Assert.Contains("Square", lines[0]); // ou "Round" selon la première pièce

            // Vérifie le format de la dernière ligne
            Assert.StartsWith("16. ", lines[15]);
        }

    }
}
