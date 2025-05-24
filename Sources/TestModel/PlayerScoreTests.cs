using Model;

namespace TestModel
{
    public class PlayerScoreTests
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            string expectedName = "Alice";
            int expectedScore = 42;

            // Act
            var playerScore = new PlayerScore(expectedName, expectedScore);

            // Assert
            Assert.Equal(expectedName, playerScore.Name);
            Assert.Equal(expectedScore, playerScore.Score);
        }

        [Fact]
        public void NameProperty_SetGet_Works()
        {
            // Arrange
            var playerScore = new PlayerScore("Initial", 0);
            string newName = "Bob";

            // Act
            playerScore.Name = newName;

            // Assert
            Assert.Equal(newName, playerScore.Name);
        }

        [Fact]
        public void ScoreProperty_SetGet_Works()
        {
            // Arrange
            var playerScore = new PlayerScore("Initial", 0);
            int newScore = 99;

            // Act
            playerScore.Score = newScore;

            // Assert
            Assert.Equal(newScore, playerScore.Score);
        }
    }
}
