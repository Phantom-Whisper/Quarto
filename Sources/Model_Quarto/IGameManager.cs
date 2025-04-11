using System.Security.Cryptography.X509Certificates;

namespace Model
{
    public interface IGameManager
    {
        void LaunchGame();

        //void SaveGame();

        //void LoadGame();
    }

    public class GameManager : IGameManager
    {
        public void LaunchGame() 
        {
            public readonly Board board;

            Player[] players;
            Player player;
            
            Bag bag;

            //Méthode pour sélectionner les noms de joueurs


        }
    }
}
