using Manager;
using Manager.CustomEventArgs;

namespace Model
{
    public class GameManager : IGameManager
    {
        public event EventHandler<MessageEventArgs>? OnDisplayMessage;
        public event EventHandler<PlayerNameRequestedEventArgs>? OnPlayerNameRequested;

        private IPlayer[] players = new IPlayer[2];
        private int currentPlayerIndex = 0;
        
        Bag bag = new() { };
        private readonly Board board = new();

        public IPlayer CurrentPlayer => players[currentPlayerIndex];

        public void SwitchCurrentPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }

        public void LaunchGame()
        {
            Console.WriteLine("Point d'arrêt");
        }

        public void CreatePlayers(bool solo)
        {
            if (solo)
            {
                // Joueur Humain
                var args = new PlayerNameRequestedEventArgs(0);
                OnPlayerNameRequested?.Invoke(this, args);

                string name = string.IsNullOrWhiteSpace(args.PlayerName)
                    ? "Player1"
                    : args.PlayerName;

                players[0] = new HumanPlayer(name);

                // Joueur IA
                players[1] = new DumbAIPlayer();
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    var args = new PlayerNameRequestedEventArgs(i);
                    OnPlayerNameRequested?.Invoke(this, args);

                    string name = string.IsNullOrWhiteSpace(args.PlayerName)
                        ? $"Player{i + 1}"
                        : args.PlayerName;

                    players[i] = new HumanPlayer(name);
                }
            }
        }
    }
}