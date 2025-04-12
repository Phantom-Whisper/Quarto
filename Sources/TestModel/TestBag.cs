using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModel
{
    public class TestBag
    {

        [Fact]
        public void TestCtor()
        {
            Bag bag = new Bag();
            Assert.NotNull(bag);
        }


        [Theory]
        [InlineData(true, true,true,true)]
        [InlineData(false, false, true, true)]
        [InlineData(true, true, false, false)]
        public void TestAddPiece(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Bag bag = new Bag();
            Piece piece = new Piece(isSquare, isLight, isBig, isFull);

            bag.AddPiece(isSquare, isLight, isBig, isFull);

            Assert.NotEmpty(bag.Baglist);
        }

        [Theory]
        [InlineData(true, true, true, true)]
        [InlineData(false, false, true, true)]
        [InlineData(true, true, false, false)]
        public void TestRemovePiece_OK(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Bag bag = new Bag();
            Piece piece = new Piece(isSquare, isLight, isBig, isFull);

            bag.AddPiece(isSquare, isLight, isBig, isFull);
            bag.RemovePiece(piece);

            Assert.DoesNotContain(piece,bag.Baglist);
        }
    }
}
