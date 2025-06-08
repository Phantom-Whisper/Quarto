using Manager;
using Manager.CustomEventArgs;
using Model;

namespace TestModel
{
    public class PlayerTest
    {
        [Theory]
        [InlineData("Shadow Fox")]
        [InlineData("LunaSkye")]
        [InlineData("Pixel_Knight")]
        [InlineData("Z3r0Blade")]
        public void TestCtorPlayer_ValidNames(string pseudo)
        {
            HumanPlayer player = new(pseudo);
            Assert.NotNull(player);
            Assert.NotNull(player.Name);
            Assert.Equal(pseudo, player.Name);
        }

        [Fact]
        public void TestPseudoProperty()
        {
            var player = new HumanPlayer("JoueurTest");
            Assert.IsType<string>(player.Name);
            Assert.Equal("JoueurTest", player.Name);
        }

        [Fact]
        public void PlayTurn_ShouldDisplayMessage_WhenMoveIsInvalid()
        {
            var player = new HumanPlayer("Test");
            var board = new Board(4, 4);
            var piece = new Piece(true, true, true, true);

            player.PlayTurn(board, piece);

            Assert.Null(board.GetPiece(1, 1)); // No piece placed at (1,1)
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenSameReference()
        {
            var player = new HumanPlayer("Alice");
            Assert.True(player.Equals(player, player));
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenNamesAreEqual()
        {
            var player1 = new HumanPlayer("Alice");
            var player2 = new HumanPlayer("Alice");
            Assert.True(player1.Equals(player1, player2));
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenNamesAreDifferent()
        {
            var player1 = new HumanPlayer("Alice");
            var player2 = new HumanPlayer("Bob");
            Assert.False(player1.Equals(player1, player2));
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenOneIsNull()
        {
            var player1 = new HumanPlayer("Alice");
            Player? player2 = null;
            Assert.False(player1.Equals(player1, player2));
            Assert.False(player1.Equals(player2, player1));
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenBothAreNull()
        {
            Player? player1 = null;
            Player? player2 = null;
            Assert.True(new HumanPlayer("X").Equals(player1, player2));
        }

        [Fact]
        public void GetHashCode_ShouldBeSame_ForPlayersWithSameName()
        {
            var player1 = new HumanPlayer("Alice");
            var player2 = new HumanPlayer("Alice");

            int hash1 = player1.GetHashCode(player1);
            int hash2 = player2.GetHashCode(player2);

            Assert.Equal(hash1, hash2);
        }

        [Fact]
        public void GetHashCode_ShouldBeDifferent_ForPlayersWithDifferentNames()
        {
            var player1 = new HumanPlayer("Alice");
            var player2 = new HumanPlayer("Bob");

            int hash1 = player1.GetHashCode(player1);
            int hash2 = player2.GetHashCode(player2);

            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void GetHashCode_ShouldMatchNameHashCode()
        {
            var player = new HumanPlayer("Charlie");
            int expected = player.Name.GetHashCode();

            int actual = player.GetHashCode(player);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PlayerConstructor_ShouldThrow_OnNullName()
        {
            Assert.Throws<ArgumentNullException>(() => new HumanPlayer(null!));
        }

        [Fact]
        public void Name_Getter_ShouldReturnName()
        {
            var player = new HumanPlayer("Alice");
            Assert.Equal("Alice", player.Name);
        }

        [Fact]
        public void Name_Getter_ThrowsIfNameNotInitialized()
        {
            // Crée instance avec constructeur (nom="temp")
            var player = (Player)Activator.CreateInstance(typeof(HumanPlayer), ["temp"])!;

            // Réinitialise le champ privé 'name' à null via réflexion
            var field = typeof(Player).GetField("name", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
            field.SetValue(player, null);

            // Teste que la lecture de Name lance InvalidOperationException
            Assert.Throws<InvalidOperationException>(() => { var n = player.Name; });
        }

        [Fact]
        public void PropertyChanged_ShouldBeRaised_WhenNameChanges()
        {
            var player = new HumanPlayer("Bob");
            bool eventRaised = false;

            player.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "Name") eventRaised = true;
            };

            player.Name = "Charlie";

            Assert.True(eventRaised);
            Assert.Equal("Charlie", player.Name);
        }

        [Fact]
        public void Name_Setter_ShouldThrowOnNull()
        {
            var player = new HumanPlayer("Jean");
            Assert.Throws<ArgumentNullException>(() => player.Name =null!);
        }

        [Fact]
        public void GetHashCode_ShouldReturnHashCodeOfName()
        {
            var player = new HumanPlayer("TestPlayer");

            int expected = player.Name.GetHashCode();

            Assert.Equal(expected, player.GetHashCode(player));
        }

        [Fact]
        public void GetHashCode_ShouldThrowOnNullPlayer()
        {
            var player = new HumanPlayer("Grégoire");
            Assert.Throws<ArgumentNullException>(() => player.GetHashCode(null!));
        }
    }
}