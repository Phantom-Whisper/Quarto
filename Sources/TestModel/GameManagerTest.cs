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

        private class DummyPlayer(string name) : HumanPlayer(name)
        {
            public int PlayTurnCount { get; private set; }

            public override void PlayTurn(IBoard board, IPiece currentPiece, IGameManager gameManager)
            {
                PlayTurnCount++;
            }
        }

        [Fact]
        public void RequestCoordinates_ShouldReturnCoordinatesProvidedByCallback()
        {
            var rules = new DummyRules();
            var scoreManager = new DummyScoreManager();
            var board = new Board(4, 4);
            var bag = new Bag();
            var player = new HumanPlayer("Alice");
            var players = new IPlayer[] { player, new HumanPlayer("Bob") };
            var manager = new GameManager(rules, scoreManager, board, bag, players);

            (int row, int col) expected = (2, 3);
            bool eventTriggered = false;

            manager.AskCoordinate += (s, e) =>
            {
                eventTriggered = true;
                // Simule la réponse utilisateur via le callback
                e.CoordinateCallback(expected);
            };

            var result = manager.RequestCoordinates(player);

            Assert.True(eventTriggered);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void SwitchCurrentPlayer_ShouldIncrementIndex_AndWrapAround()
        {
            var rules = new DummyRules();
            var scoreManager = new DummyScoreManager();
            var board = new Board(4, 4);
            var bag = new Bag();
            var players = new IPlayer[] { new HumanPlayer("A"), new HumanPlayer("B"), new HumanPlayer("C") };
            var manager = new GameManager(rules, scoreManager, board, bag, players);

            var field = typeof(GameManager).GetField("currentPlayerIndex", BindingFlags.NonPublic | BindingFlags.Instance)!;

            // Index initial à 0
            Assert.Equal(0, (int)field.GetValue(manager)!);

            // Passe à 1
            manager.SwitchCurrentPlayer();
            Assert.Equal(1, (int)field.GetValue(manager)!);

            // Passe à 2
            manager.SwitchCurrentPlayer();
            Assert.Equal(2, (int)field.GetValue(manager)!);

            // Revient à 0 (boucle)
            manager.SwitchCurrentPlayer();
            Assert.Equal(0, (int)field.GetValue(manager)!);
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

        [Fact]
        public void RequestNewPiece_ShouldThrow_WhenPieceNotSelected()
        {
            var gm = CreateGameManager();

            gm.AskPieceToPlay += (_, args) => { };

            var method = typeof(GameManager).GetMethod("RequestNewPiece", BindingFlags.NonPublic | BindingFlags.Instance)!;

            Assert.Throws<TargetInvocationException>(() => method.Invoke(gm, null));
        }

        [Fact]
        public void Run_ShouldLoopUntilGameOver()
        {
            var rulesMock = new Mock<IRulesManager>();
            var piecesList = new ObservableCollection<IPiece> { Mock.Of<IPiece>() };
            var readonlyPieces = new ReadOnlyObservableCollection<IPiece>(piecesList);
            var bagMock = new Mock<IBag>();
            bagMock.SetupGet(b => b.Baglist).Returns(readonlyPieces);

            int callCount = 0;
            rulesMock.Setup(r => r.IsGameOver(It.IsAny<IBag>(), It.IsAny<IBoard>()))
                .Returns(() => callCount++ > 1); 

            var playerMock1 = new Mock<IPlayer>();
            playerMock1.Setup(p => p.Name).Returns("Player1");
            playerMock1.Setup(p => p.PlayTurn(It.IsAny<IBoard>(), It.IsAny<IPiece>(), It.IsAny<IGameManager>()));

            var playerMock2 = new Mock<IPlayer>();
            playerMock2.Setup(p => p.Name).Returns("Player2");
            playerMock2.Setup(p => p.PlayTurn(It.IsAny<IBoard>(), It.IsAny<IPiece>(), It.IsAny<IGameManager>()));

            var players = new IPlayer[] { playerMock1.Object, playerMock2.Object };

            var boardMock = new Mock<IBoard>();

            var gm = new GameManager(rulesMock.Object, Mock.Of<IScoreManager>(), boardMock.Object, bagMock.Object, players);

            gm.AskPieceToPlay += (s, e) => e.PieceToPlay = e.Pieces.First();

            gm.Run();

            Assert.True(callCount >= 2);

            playerMock1.Verify(p => p.PlayTurn(It.IsAny<IBoard>(), It.IsAny<IPiece>(), gm), Times.AtLeastOnce);
            playerMock2.Verify(p => p.PlayTurn(It.IsAny<IBoard>(), It.IsAny<IPiece>(), gm), Times.AtLeastOnce);
        }
    }
}