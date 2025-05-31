using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModel
{
    public class ScoreManagerTest
    {
        [Fact]
        public void AddVictory_ShouldAddNewPlayerWithOneVictory()
        {
            var manager = new ScoreManager();
            var player = new HumanPlayer("Alice");

            manager.AddVictory(player);

            var scores = manager.GetAllScores();
            Assert.Contains(scores, ps => ps.Name == "Alice" && ps.Score == 1);
        }

        [Fact]
        public void AddVictory_ShouldIncrementVictory()
        {
            var manager = new ScoreManager();
            var player = new HumanPlayer("Bob");

            manager.AddVictory(player);
            manager.AddVictory(player);

            var scores = manager.GetAllScores();
            var bobScore = scores.FirstOrDefault(ps => ps.Name == "Bob");
            Assert.NotNull(bobScore);
            Assert.Equal(2, bobScore.Score);
        }

        [Fact]
        public void AddVictory_WhenPlayerIsNull()
        {
            var manager = new ScoreManager();

            manager.AddVictory(null!);

            var scores = manager.GetAllScores();
            Assert.Empty(scores);
        }

        [Fact]
        public void GetScore_ShouldReturnZero_WhenPlayerIsNull()
        {
            var manager = new ScoreManager();

            int score = manager.GetScore(null!);
            Assert.Equal(0, score);
        }

        [Fact]
        public void GetScore_ShouldReturnZero_WhenPlayerNotInScores()
        {
            var manager = new ScoreManager();
            var player = new HumanPlayer("NonExistent");

            int score = manager.GetScore(player);
            Assert.Equal(0, score);
        }

        [Fact]
        public void GetScore_ShouldReturnCorrectScore_WhenPlayerExists()
        {
            var manager = new ScoreManager();
            var player = new HumanPlayer("Alice");

            manager.AddVictory(player);
            manager.AddVictory(player);

            int score = manager.GetScore(player);
            Assert.Equal(2, score);
        }

        [Fact]
        public void GetAllScores_ShouldReturnAllCurrentScores()
        {
            var manager = new ScoreManager();
            var player1 = new HumanPlayer("Alice");
            var player2 = new HumanPlayer("Bob");

            manager.AddVictory(player1);
            manager.AddVictory(player2);
            manager.AddVictory(player2);

            var scores = manager.GetAllScores();

            Assert.Equal(2, scores.Count);
            Assert.Contains(scores, ps => ps.Name == "Alice" && ps.Score == 1);
            Assert.Contains(scores, ps => ps.Name == "Bob" && ps.Score == 2);
        }

        [Fact]
        public void GetAllScores_CollectionIsObservable()
        {
            var manager = new ScoreManager();
            Assert.IsType<System.Collections.ObjectModel.ObservableCollection<PlayerScore>>(manager.GetAllScores());
        }

        [Fact]
        public void SaveScores_ShouldSortScoresDescending()
        {
            var manager = new ScoreManager();
            var player1 = new HumanPlayer("Alice");
            var player2 = new HumanPlayer("Bob");

            manager.AddVictory(player1);
            manager.AddVictory(player2);
            manager.AddVictory(player2);

            manager.SaveScores();

            var scores = manager.GetAllScores();

            Assert.Equal(2, scores.Count);
            Assert.Equal("Bob", scores[0].Name);
            Assert.Equal(2, scores[0].Score);
            Assert.Equal("Alice", scores[1].Name);
            Assert.Equal(1, scores[1].Score);
        }
    }
}
