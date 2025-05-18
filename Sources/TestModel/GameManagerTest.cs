using Xunit;
using Model;
using Manager;
using Manager.CustomEventArgs;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        private class DummyPlayer : HumanPlayer
        {
            public int PlayTurnCount { get; private set; }
            public DummyPlayer(string name) : base(name) { }
            public override void PlayTurn(IBoard board, IPiece currentPiece, IGameManager gameManager)
            {
                PlayTurnCount++;
            }
        }

        [Fact]
        public void Turn_ShouldIncrementTurnNumber_WhenCurrentPlayerIndexIsZero()
        {
            var rules = new DummyRules();
            var scoreManager = new DummyScoreManager();
            var board = new Board(4, 4);
            var bag = new Bag();
            var players = new IPlayer[] { new DummyPlayer("A"), new DummyPlayer("B") };
            var manager = new GameManager(rules, scoreManager, board, bag, players);

            // Préparer le contexte
            typeof(GameManager).GetField("currentPlayerIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(manager, 0);
            typeof(GameManager).GetField("pieceToPlay", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(manager, bag.Baglist.First());

            // Appel
            var turnNumberField = typeof(GameManager).GetField("turnNumber", BindingFlags.NonPublic | BindingFlags.Instance)!;
            int before = (int)turnNumberField.GetValue(manager)!;
            manager.Turn();
            int after = (int)turnNumberField.GetValue(manager)!;

            Assert.Equal(before + 1, after);
        }

        [Fact]
        public void Turn_ShouldCallPlayTurn_OnCurrentPlayer()
        {
            var rules = new DummyRules();
            var scoreManager = new DummyScoreManager();
            var board = new Board(4, 4);
            var bag = new Bag();
            var player = new DummyPlayer("A");
            var players = new IPlayer[] { player, new DummyPlayer("B") };
            var manager = new GameManager(rules, scoreManager, board, bag, players);

            typeof(GameManager).GetField("currentPlayerIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(manager, 0);
            typeof(GameManager).GetField("pieceToPlay", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(manager, bag.Baglist.First());

            manager.AskPieceToPlay += (s, e) => e.PieceToPlay = bag.Baglist.First();

            manager.Turn();

            Assert.True(player.PlayTurnCount > 0);
        }

        [Fact]
        public void Turn_ShouldThrow_WhenPieceToPlayIsNull()
        {
            var rules = new DummyRules();
            var scoreManager = new DummyScoreManager();
            var board = new Board(4, 4);
            var bag = new Bag();
            var players = new IPlayer[] { new DummyPlayer("A"), new DummyPlayer("B") };
            var manager = new GameManager(rules, scoreManager, board, bag, players);

            typeof(GameManager).GetField("currentPlayerIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(manager, 0);
            typeof(GameManager).GetField("pieceToPlay", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(manager, null);

            Assert.Throws<InvalidOperationException>(() => manager.Turn());
        }

        [Fact]
        public void Turn_ShouldSwitchPlayer_WhenNoQuarto()
        {
            var rules = new DummyRules();
            var scoreManager = new DummyScoreManager();
            var board = new Board(4, 4);
            var bag = new Bag();
            var players = new IPlayer[] { new DummyPlayer("A"), new DummyPlayer("B") };
            var manager = new GameManager(rules, scoreManager, board, bag, players);

            typeof(GameManager).GetField("currentPlayerIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(manager, 0);
            typeof(GameManager).GetField("pieceToPlay", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(manager, bag.Baglist.First());

            // Simule la sélection d'une nouvelle pièce à chaque RequestNewPiece
            manager.AskPieceToPlay += (s, e) => e.PieceToPlay = bag.Baglist.FirstOrDefault();

            manager.Turn();

            var currentPlayerIndex = (int)typeof(GameManager).GetField("currentPlayerIndex", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(manager)!;
            Assert.Equal(1, currentPlayerIndex);
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

    }
}
