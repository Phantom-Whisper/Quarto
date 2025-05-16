using Manager;
using Manager.CustomEventArgs;
using System.Security.Cryptography;

namespace Model
{
    public class GameManager : IGameManager
    {
        private int TURNNUMBER = 0;

        public event EventHandler<MessageEventArgs>? OnDisplayMessage;
        public event EventHandler<InputRequestedEventArgs>? OnInputRequested;
        public event EventHandler<PlayerNameRequestedEventArgs>? OnPlayerNameRequested;

        public event EventHandler<GameStartedEventArgs>? GameStarted;
        private void OnGameStarted(GameStartedEventArgs args)
        {
            GameStarted?.Invoke(this, args);
        }

        private readonly IPlayer[] players = new IPlayer[2];
        private int currentPlayerIndex = 1;

        private IRulesManager rulesManager = new Rules();
        
        readonly Bag bag = new() { };
        public List<IPiece> GetAvailablePieces() => [.. bag.Baglist];
        private readonly Board board = new() { };

        public IPlayer CurrentPlayer => players[currentPlayerIndex];

        private IPiece? pieceToPlay = null;

        public GameManager(IRulesManager rules, IPlayer[] players)
        {
            rulesManager = rules;
            this.players = players;
        }

        public void Run()
        {
            OnGameStarted(new GameStartedEventArgs(board, bag, CurrentPlayer));
            pieceToPlay = FirstTurn();
            while (!rulesManager.IsGameOver(bag, board))
            {
                Turn();
            }
        }

        private IPiece FirstTurn()
        {
            IPiece? selectedPiece = null;
            using ManualResetEvent waitHandle = new(false);

            void RequestPiece()
            {
                OnInputRequested?.Invoke(this, new InputRequestedEventArgs(
                    "Enter the number of the piece:",
                    input =>
                    {
                        if (int.TryParse(input, out int index))
                        {
                            index -= 1;
                            if (index >= 0 && index < bag.Baglist.Count)
                            {
                                selectedPiece = bag.Baglist[index];
                                bag.TakePiece((Piece)selectedPiece);
                                waitHandle.Set();
                                return;
                            }
                        }

                        OnDisplayMessage?.Invoke(this, new MessageEventArgs("Invalid selection. Please try again."));
                        RequestPiece();
                    }));
            }

            if (CurrentPlayer is AIPlayer)
            {
                using var randomGenerator = RandomNumberGenerator.Create();
                byte[] data = new byte[4];
                randomGenerator.GetBytes(data);

                int randomInt = BitConverter.ToInt32(data, 0);

                randomInt = Math.Abs(randomInt);

                SwitchCurrentPlayer();
                return bag.Baglist[randomInt % bag.Baglist.Count];                
            }

            OnDisplayMessage?.Invoke(this, new MessageEventArgs($"{CurrentPlayer.Name} choose a piece to give to your opponent:"));

            RequestPiece(); 
            waitHandle.WaitOne();
            SwitchCurrentPlayer();
            return selectedPiece!;
        }

        public void DisplayMessage(string message)
        {
            OnDisplayMessage?.Invoke(this, new MessageEventArgs(message));
        }

        public void RequestInput(string prompt, Action<string?> callback)
        {
            OnInputRequested?.Invoke(this, new InputRequestedEventArgs(prompt, callback));
        }

        public void Turn()
        {
            if (currentPlayerIndex == 0)
                TURNNUMBER++;
            Display();

            if (TURNNUMBER >= 4)
                TryDeclareQuarto(board, rulesManager);

            if (pieceToPlay is null)
                throw new InvalidOperationException("Piece not selected before usage.");
            pieceToPlay = CurrentPlayer.PlayTurn(board, pieceToPlay, this);
            SwitchCurrentPlayer();
           Console.WriteLine("Point d'arrêt");
        }

        private void TryDeclareQuarto(Board board, IRulesManager rulesManager)
        {
            OnDisplayMessage?.Invoke(this, new MessageEventArgs($"{CurrentPlayer.Name}, do you want to declare a Quarto? (y/n)"));
            string? response = Console.ReadLine()?.Trim().ToLower();

            if (response != "y") return;

            List<(int row, int col)> selectedPositions = new();

            for (int i = 1; i <= 4; i++)
            {
                OnDisplayMessage?.Invoke(this, new MessageEventArgs($"Select piece {i}: enter row:"));
                if (!int.TryParse(Console.ReadLine(), out int row))
                {
                    OnDisplayMessage?.Invoke(this, new MessageEventArgs("Invalid input. Aborting Quarto attempt."));
                    return;
                }

                OnDisplayMessage?.Invoke(this, new MessageEventArgs("Enter column:"));
                if (!int.TryParse(Console.ReadLine(), out int col))
                {
                    OnDisplayMessage?.Invoke(this, new MessageEventArgs("Invalid input. Aborting Quarto attempt."));
                    return;
                }

                if (!board.IsOnBoard(row, col) || board.IsEmpty(row, col))
                {
                    OnDisplayMessage?.Invoke(this, new MessageEventArgs("Invalid selection. Aborting Quarto attempt."));
                    return;
                }

                selectedPositions.Add((row, col));
            }

            var selectedPieces = selectedPositions
                .Select(pos => board.GetPiece(pos.row, pos.col))
                .ToList();

            if (rulesManager.IsQuarto(board, selectedPieces))
            {
                OnDisplayMessage?.Invoke(this, new MessageEventArgs($"{CurrentPlayer.Name} wins with a Quarto!"));
            }
            else
            {
                OnDisplayMessage?.Invoke(this, new MessageEventArgs("Incorrect Quarto declaration. The game continues."));
            }
        }

        public void SwitchCurrentPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }        

        private void Display()
        {
            OnDisplayMessage?.Invoke(this, new MessageEventArgs($"Tour: {TURNNUMBER}"));
            OnDisplayMessage?.Invoke(this, new MessageEventArgs($"Joueur courant: {CurrentPlayer.Name}"));
            OnDisplayMessage?.Invoke(this, new MessageEventArgs(board.ToString())); // Faire des méthodes Display parcourant les éléments
            OnDisplayMessage?.Invoke(this, new MessageEventArgs(bag.ToString())); // Faire des méthodes Display parcourant les éléments
            OnDisplayMessage?.Invoke(this, new MessageEventArgs($"Piece à jouer: {pieceToPlay}"));
        }
    }
}