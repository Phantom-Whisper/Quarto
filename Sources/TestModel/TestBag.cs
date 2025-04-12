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
        public void TestCtor_Empty()
        {
            var bag = new Bag();
            Assert.Empty(bag.Baglist);
        }

        [Fact]
        public void AddPiece_Duplicate()
        {
            var bag = new Bag();
            bag.AddPiece(true, true, false, true);

            Assert.Throws<InvalidOperationException>(() =>
                bag.AddPiece(true, true, false, true));
        }


        [Theory]
        [InlineData(true, true,true,true)]
        [InlineData(false, false, true, true)]
        [InlineData(true, true, false, false)]
        public void TestAddPiece_NotEmpty(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Bag bag = new Bag();
            Piece piece = new Piece(isSquare, isLight, isBig, isFull);

            bag.AddPiece(isSquare, isLight, isBig, isFull);

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
            bag.AddPiece(isSquare, isLight, isBig, isFull);

            Piece addPiece = bag.Baglist.First();

            Assert.Equal(isSquare, addPiece.IsSquare);
            Assert.Equal(isLight, addPiece.IsLight);
            Assert.Equal(isBig, addPiece.IsBig);
            Assert.Equal(isFull, addPiece.IsFull);
        }


        [Fact]
        public void TestRemovePiece_Empty()
        {
            Bag bag = new Bag();
            Piece piece = new Piece(true, true, false, true);

            Assert.Throws<InvalidOperationException>(() => bag.RemovePiece(piece));
        }

        [Fact]
        public void TestRemovePiece_NotInTheBag()
        {
            Bag bag = new Bag();
            bag.AddPiece(true,false,false,false);
            Piece piece = new Piece(true, true, true, true);

            Assert.Throws<InvalidOperationException>(() => bag.RemovePiece(piece));
        }


        [Theory]
        [InlineData(true, true, true, true)]
        [InlineData(false, false, true, true)]
        [InlineData(true, true, false, false)]
        public void TestRemovePiece_DoesNotContain(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            Bag bag = new Bag();
            Piece piece = new Piece(isSquare, isLight, isBig, isFull);

            bag.AddPiece(isSquare, isLight, isBig, isFull);
            bag.RemovePiece(piece);

            Assert.DoesNotContain(piece,bag.Baglist);
        }
    }
}
