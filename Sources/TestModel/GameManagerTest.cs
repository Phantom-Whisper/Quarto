using Xunit;
using Model;
using Manager;
using Manager.CustomEventArgs;
using System.Collections.Generic;
using System.Linq;

namespace TestModel
{
    public class GameManagerTest
    {
        private class DummyRules : IRulesManager
        {
            public bool IsGameOverReturn { get; set; }
            public int IsGameOverCallCount { get; set; }
            public bool IsGameOver(IBag bag, IBoard board)
            {
                IsGameOverCallCount++;
                return IsGameOverReturn;
            }
            public bool IsQuarto(IBoard board, List<IPiece> pieces) => false;
            public List<IPiece>? GetQuarto(IBoard board) => null;
        }

        private class DummyScoreManager : IScoreManager
        {
            public int AddVictoryCount { get; set; }
            public int SaveScoresCount { get; set; }
            public void AddVictory(IPlayer winner) => AddVictoryCount++;
            public void SaveScores() => SaveScoresCount++;
            public void LoadScores() { }
            public int GetScore(IPlayer player) => 0;
        }

        [Fact]
        public void Run_ShouldTriggerGameStartedEvent()
        {
            var rules = new DummyRules { IsGameOverReturn = true };
            var scoreManager = new DummyScoreManager();
            var board = new Board(4, 4);
            var bag = new Bag();
            var players = new IPlayer[] { new HumanPlayer("A"), new HumanPlayer("B") };

            var manager = new GameManager(rules, scoreManager, board, bag, players);

            bool eventTriggered = false;
            manager.GameStarted += (s, e) => eventTriggered = true;

            // Simule une sélection de pièce valide pour éviter l'exception
            manager.AskPieceToPlay += (s, e) => e.PieceToPlay = bag.Baglist.First();

            manager.Run();

            Assert.True(eventTriggered);
        }

        [Fact]
        public void Run_ShouldNotLoop_WhenGameIsOverImmediately()
        {
            var rules = new DummyRules { IsGameOverReturn = true };
            var scoreManager = new DummyScoreManager();
            var board = new Board(4, 4);
            var bag = new Bag();
            var players = new IPlayer[] { new HumanPlayer("A"), new HumanPlayer("B") };

            var manager = new GameManager(rules, scoreManager, board, bag, players);

            // Simule une sélection de pièce valide pour éviter l'exception
            manager.AskPieceToPlay += (s, e) => e.PieceToPlay = bag.Baglist.First();

            manager.Run();

            Assert.Equal(1, rules.IsGameOverCallCount); // Appelé une seule fois
        }

    }
}
