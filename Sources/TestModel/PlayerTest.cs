using Model;

namespace TestModel
{
    public class PlayerTest
    {
        /*readonly HumanPlayer player = new("JoueurTest");

        [Theory]
        [InlineData("Shadow Fox")]
        [InlineData("LunaSkye")]
        [InlineData("Pixel_Knight")]
        [InlineData("Z3r0Blade")]
        public void TestCtorPlayer(string pseudo)
        {
            HumanPlayer player = new(pseudo);
            Assert.NotNull(player);
            Assert.NotNull(player.Pseudo);
            Assert.Equal(pseudo, player.Pseudo);
            Assert.Equal(0, player.NbWin);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        public void TestCtorPlayerArgumentException(string pseudo)
        {
            Assert.Throws<ArgumentNullException>(() => { HumanPlayer player = new(pseudo); });
        }


        [Fact]
        public void TestPseudo()
        {
            Assert.IsType<string>(player.Pseudo);
            Assert.Equal("JoueurTest", player.Pseudo);
        }

        [Fact]
        public void TestNbWin()
        {
            Assert.IsType<int>(player.NbWin);
            Assert.Equal(0, player.NbWin);
        }

        [Fact]
        public void TestAddWin()
        {
            player.AddWin();
            Assert.Equal(1, player.NbWin);
        }
        */
    }
}
