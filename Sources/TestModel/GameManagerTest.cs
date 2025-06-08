using Model;
using Manager;
using System.Reflection;
using Moq;
using System.Collections.ObjectModel;

namespace TestModel
{
    public class GameManagerTest
    {
        private static GameManager CreateGameManager(
            IRulesManager? rulesManager = null,
            IScoreManager? scoreManager = null,
            IBoard? board = null,
            IBag? bag = null,
            IPlayer[]? players = null)
        {
            rulesManager ??= Mock.Of<IRulesManager>(r => r.IsGameOver(It.IsAny<IBag>(), It.IsAny<IBoard>()) == true);
            scoreManager ??= Mock.Of<IScoreManager>();
            board ??= Mock.Of<IBoard>();
            var emptyBagList = new ReadOnlyObservableCollection<IPiece>([]);

            bag ??= Mock.Of<IBag>(b => b.Baglist == emptyBagList);
            players ??= [Mock.Of<IPlayer>(p => p.Name == "J1"), Mock.Of<IPlayer>(p => p.Name == "J2")];

            return new GameManager(rulesManager, scoreManager, board, bag, players);
        }

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

            // Add the missing property to fix the error
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
        public void RequestNewPiece_ShouldRaiseAskPieceToPlay()
        {
            var gm = CreateGameManager();
            bool raised = false;

            gm.AskPieceToPlay += (_, args) =>
            {
                raised = true;
                args.PieceToPlay = Mock.Of<IPiece>();
            };

            var method = typeof(GameManager).GetMethod("RequestNewPiece", BindingFlags.NonPublic | BindingFlags.Instance)!;
            method.Invoke(gm, null);

            Assert.True(raised);
        }
    }
}