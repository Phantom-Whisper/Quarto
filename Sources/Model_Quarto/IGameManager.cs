using System.Security.Cryptography.X509Certificates;

namespace Model
{
    public interface IGameManager
    {
        void LaunchGame();
    }

    public class GameManager : IGameManager
    {
        public void LaunchGame()
        {
            //Méthode pour sélectionner les noms de joueurs
        }
    }
}
