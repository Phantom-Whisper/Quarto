using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace TestModel
{
    public class GameLogTest
    {
        [Fact]
        public void TestConstructor_ShouldInitializeProperties()
        {
            var log = new GameLog();
            Assert.NotNull(log.Turns);
            Assert.Empty(log.Turns);
            Assert.Equal(default(DateTime), log.GameStartTime);
            Assert.Null(log.Winner);
        }

        [Fact]
        public void TestConstructor_ShouldSetGameStartTime()
        {
            var now = DateTime.Now;
            var log = new GameLog(now);
            Assert.Equal(now, log.GameStartTime);
            Assert.NotNull(log.Turns);
            Assert.Empty(log.Turns);
        }

        [Fact]
        public void TestAddTurn_ShouldAddTurnToList()
        {
            var log = new GameLog();
            var piece = new Piece(true, false, true, false);
            var turn = new TurnLog(1, "Alice", piece, 2, 3);

            log.AddTurn(turn);

            Assert.Single(log.Turns);
            Assert.Equal(turn, log.Turns[0]);
        }

        [Fact]
        public void TestWinnerProperty_ShouldBeSettableAndGettable()
        {
            var log = new GameLog();
            log.Winner = "Bob";
            Assert.Equal("Bob", log.Winner);
        }

        [Fact]
        public void TestTurnsList_ShouldAllowMultipleTurns()
        {
            var log = new GameLog();
            var piece1 = new Piece(true, false, true, false);
            var piece2 = new Piece(false, true, false, true);

            var turn1 = new TurnLog(1, "Alice", piece1, 0, 0);
            var turn2 = new TurnLog(2, "Bob", piece2, 1, 1);

            log.AddTurn(turn1);
            log.AddTurn(turn2);

            Assert.Equal(2, log.Turns.Count);
            Assert.Contains(turn1, log.Turns);
            Assert.Contains(turn2, log.Turns);
        }
    }
}
