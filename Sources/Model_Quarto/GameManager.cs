using Manager;
using Manager.CustomEventArgs;

namespace Model
{
    public class GameManager : IGameManager
    {
        private int TURNNUMBER = 0;

        public event EventHandler<MessageEventArgs>? OnDisplayMessage;
        public event EventHandler<PlayerNameRequestedEventArgs>? OnPlayerNameRequested;

        private IPlayer[] players = new IPlayer[2];
        private int currentPlayerIndex = 0;

        // private IRulesManager rulesManager;
        
        Bag bag = new() { };
        private readonly Board board = new();

        public IPlayer CurrentPlayer => players[currentPlayerIndex];

        Piece pieceToPlay = null;

        public void SwitchCurrentPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }

        private void FirstTurn()
        {
            // demande au J2 de choisir une piece dans le bag
            Console.WriteLine("Choose a piece to play");
            string? tile = Console.ReadLine();
        }

        public void Turn()
        {
            if (currentPlayerIndex == 0)
                TURNNUMBER++;
            Display();



           // Console.WriteLine("Point d'arrêt");
        }

        public void Run()
        {
            FirstTurn();
            /*
            while(!rulesManager.IsGameOver())
            {
                Turn();
            }
            */
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

        private void Display()
        {
            Console.WriteLine($"Tour: {TURNNUMBER}");
            Console.WriteLine($"Joueur courant: {CurrentPlayer.Name}");
            Console.WriteLine(board.ToString());
            Console.WriteLine(bag.ToString());
            Console.WriteLine($"Piece à jouer: {pieceToPlay}");

        }
    }
}