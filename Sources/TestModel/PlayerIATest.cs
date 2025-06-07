using Manager.CustomEventArgs;
using Manager;
using Model;
using Moq;

namespace TestModel
{
    public class PlayerIATest
    {
        [Fact]
        public void TestConstructor()
        {
            string expectedName = "Dumb AI";
            var aiPlayer = new DumbAIPlayer();
            Assert.Equal(expectedName, aiPlayer.Name);
        }
    }
}
