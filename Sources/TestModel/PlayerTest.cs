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
            Assert.NotNull(player.Name); // Or player.Name if needed
            Assert.Equal(pseudo, player.Name);
        }

        [Fact]
        public void TestPseudoProperty()
        {
            var player = new HumanPlayer("JoueurTest");
            Assert.IsType<string>(player.Name);
            Assert.Equal("JoueurTest", player.Name);
        }
    }
 }
