using Model;

namespace TestModel
{
    public class BagTest
    {
        /*
        Piece pieceTest = new(true, true, true, true);

        [Fact]
        public void TestCtor_Empty()
        {
            var bag = new Bag();
            Assert.Empty(bag.Baglist);
        }

        [Fact]
        public void AddPiece_Duplicate()
        {
            var bag = new Bag();
            bag.AddPiece(pieceTest);

            Assert.Throws<InvalidOperationException>(() =>
                bag.AddPiece(pieceTest));
        }


        [Theory]
        [InlineData(true, true,true,true)]
        [InlineData(false, false, true, true)]
        [InlineData(true, true, false, false)]
        public void TestAddPiece_NotEmpty(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Bag bag = new Bag();
            Piece piece = new Piece(isSquare, isLight, isBig, isFull);

            bag.AddPiece(piece);

            Assert.NotEmpty(bag.Baglist);
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
        public void AddPiece_ContainCorrectPiece(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Bag bag = new Bag();
            Piece piece = new(isSquare, isLight, isBig, isFull);
            bag.AddPiece(piece);

            Piece addPiece = bag.Baglist.First();

            Assert.Equal(isSquare, addPiece.IsSquare);
            Assert.Equal(isLight, addPiece.IsLight);
            Assert.Equal(isBig, addPiece.IsBig);
            Assert.Equal(isFull, addPiece.IsFull);
        }

        [Fact]
        public void TestTakePiece_Null()
        {
            Bag? bag = new Bag();
            bag = null;

            Assert.Null(bag);
        }


        [Fact]
        public void TestTakePiece_Empty()
        {
            Bag bag = new Bag();
            Piece piece = new Piece(true, true, false, true);

            Assert.Throws<InvalidOperationException>(() => bag.TakePiece(piece));
        }

        [Fact]
        public void TestTakePiece_NotInTheBag()
        {
            Bag bag = new Bag();
            bag.AddPiece(pieceTest);
            Piece piece = new Piece(true, true, true, true);

            Assert.Throws<InvalidOperationException>(() => bag.TakePiece(piece));
        }


        [Theory]
        [InlineData(true, true, true, true)]
        [InlineData(false, false, true, true)]
        [InlineData(true, true, false, false)]
        public void TestTakePiece_DoesNotContain(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Bag bag = new Bag();
            Piece piece = new Piece(isSquare, isLight, isBig, isFull);

            bag.AddPiece(piece);
            bag.TakePiece(piece);

            Assert.DoesNotContain(piece,bag.Baglist);
        }
        */
    }
}
