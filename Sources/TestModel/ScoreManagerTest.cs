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

            var scores = manager.GetAllScores(); // Use the public method to access scores  
            Assert.True(scores.ContainsKey("Alice"));
            Assert.Equal(1, scores["Alice"]);
        }

        [Fact]
        public void AddVictory_ShouldIncrementVictory()
        {
            var manager = new ScoreManager();
            var player = new HumanPlayer("Bob");

            manager.AddVictory(player);
            manager.AddVictory(player);

            var scores = manager.GetAllScores(); 
            Assert.Equal(2, scores["Bob"]);
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
            var player = new HumanPlayer("NonExistant");
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
            Assert.Equal(1, scores["Alice"]);
            Assert.Equal(2, scores["Bob"]);
        }

        [Fact]
        public void GetAllScores_ReturnedDictionaryIsACopy()
        {
            var manager = new ScoreManager();
            var player = new HumanPlayer("Alice");
            manager.AddVictory(player);

            var scores = manager.GetAllScores();
            scores["Alice"] = 42; // Modification du dictionnaire retourné

            // Le score interne ne doit pas être modifié
            var actualScore = manager.GetScore(player);
            Assert.Equal(1, actualScore);
        }
    }
}
