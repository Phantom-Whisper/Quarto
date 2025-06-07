using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModel
{
    public class TurnLogTest
    {
        [Fact]
        public void TestConstructor_ShouldInitializePropertiesToDefaults()
        {
            var log = new TurnLog();
            Assert.Equal(0, log.TurnNumber);
            Assert.Null(log.CurrentPlayerName);
            Assert.Null(log.PiecePlayed);
            Assert.Equal(0, log.Row);
            Assert.Equal(0, log.Col);
        }

        [Fact]
        public void TestConstructor_ShouldSetAllProperties()
        {
            var piece = new Piece(true, false, true, false);
            var log = new TurnLog(3, "Alice", piece, 2, 1);

            Assert.Equal(3, log.TurnNumber);
            Assert.Equal("Alice", log.CurrentPlayerName);
            Assert.Equal(piece.ToString(), log.PiecePlayed);
            Assert.Equal(2, log.Row);
            Assert.Equal(1, log.Col);
        }

        [Fact]
        public void Test_ShouldBeSettableAndGettable()
        {
            var log = new TurnLog();
            log.TurnNumber = 5;
            log.CurrentPlayerName = "Bob";
            log.PiecePlayed = "CustomPiece";
            log.Row = 7;
            log.Col = 8;

            Assert.Equal(5, log.TurnNumber);
            Assert.Equal("Bob", log.CurrentPlayerName);
            Assert.Equal("CustomPiece", log.PiecePlayed);
            Assert.Equal(7, log.Row);
            Assert.Equal(8, log.Col);
        }
    }
}
