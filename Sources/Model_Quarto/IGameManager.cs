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
        readonly Board board;

        Player[] players;
        Player player;

        Bag bag;

        GameManager(Board board, Bag bag)
        {
            this.board = board;
            this.bag = bag;
            //CreatePlayers();
        }
        public void LaunchGame()
        {


            //Méthode pour sélectionner les noms de joueurs


        }


        private void CreatePlayers(string nom1, string nom2)
        {
            players[0] = new Player(nom1);
            players[1] = new Player(nom2);
        }
    }
}
