using Manager;
using Manager.CustomEventArgs;
using System.Security.Cryptography;

namespace Model
{
    public class GameManager(IRulesManager rules, IPlayer[] players) : IGameManager
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

        /// <summary>
        /// Event to declare a Quarto
        /// </summary>
        public event EventHandler<QuartoEventArgs>? Quarto;
        private void OnQuarto(QuartoEventArgs args) => Quarto?.Invoke(this, args);

        public event EventHandler<BoardChangedEventArgs>? BoardChanged;
        private void OnBoardChanged(BoardChangedEventArgs args)
        {
            BoardChanged?.Invoke(this, args);
        }

        public event EventHandler<AskPieceToPlayEventArgs> ? AskPieceToPlay;
        private void OnAskPieceToPlay(AskPieceToPlayEventArgs args)
        {
            AskPieceToPlay?.Invoke(this, args);
        }

        private readonly IPlayer[] players = players;
        private int currentPlayerIndex = 1;

        private readonly IRulesManager rulesManager = rules;
        
        readonly Bag bag = new() { };
        public List<IPiece> GetAvailablePieces() => [.. bag.Baglist];
        private readonly Board board = new() { };

        public IPlayer CurrentPlayer => players[currentPlayerIndex];

        private IPiece? pieceToPlay = null;

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
                                bag.TakePiece(selectedPiece);
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
                OnQuarto(new QuartoEventArgs(rulesManager, board, CurrentPlayer));

            if (pieceToPlay is null)
                throw new InvalidOperationException("Piece not selected before usage.");
            pieceToPlay = CurrentPlayer.PlayTurn(board, pieceToPlay, this);
            SwitchCurrentPlayer();
            //Console.WriteLine("Point d'arrêt");
        }


        public void SwitchCurrentPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }        

        private void Display()
        {
            OnDisplayMessage?.Invoke(this, new MessageEventArgs($"Tour: {TURNNUMBER}"));
            OnDisplayMessage?.Invoke(this, new MessageEventArgs($"Joueur courant: {CurrentPlayer.Name}"));
            BoardChanged?.Invoke(this, new BoardChangedEventArgs(board)); // Faire des méthodes Display parcourant les éléments
            OnDisplayMessage?.Invoke(this, new MessageEventArgs(bag.ToString())); // Faire des méthodes Display parcourant les éléments
            OnDisplayMessage?.Invoke(this, new MessageEventArgs($"Piece à jouer: {pieceToPlay}"));
        }
    }
}