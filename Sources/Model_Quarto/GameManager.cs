using Manager;
using Manager.CustomEventArgs;
using System.Security.Cryptography;

namespace Model
{
    public class GameManager(IRulesManager rules, IPlayer[] playersParam) : IGameManager
    {
        private int turnNumber  = 0;

        public event EventHandler<MessageEventArgs>? OnDisplayMessage;
        public event EventHandler<InputRequestedEventArgs>? OnInputRequested;


        public event EventHandler<GameStartedEventArgs>? GameStarted;
        private void OnGameStarted(GameStartedEventArgs args) => GameStarted?.Invoke(this, args);

        /// <summary>
        /// Event to declare a Quarto
        /// </summary>
        public event EventHandler<QuartoEventArgs>? Quarto;
        private void OnQuarto(QuartoEventArgs args) => Quarto?.Invoke(this, args);

        public event EventHandler<BoardChangedEventArgs>? BoardChanged;
        private void OnBoardChanged(BoardChangedEventArgs args) => BoardChanged?.Invoke(this, args);

        public event EventHandler<AskPieceToPlayEventArgs> ? AskPieceToPlay;
        private void OnAskPieceToPlay(AskPieceToPlayEventArgs args) => AskPieceToPlay?.Invoke(this, args);

        private readonly IPlayer[] _players = playersParam;
        private int currentPlayerIndex = 0;

        private readonly IRulesManager _rulesManager = rules;
        
        readonly Bag bag = new();
        public List<IPiece> GetAvailablePieces() => [.. bag.Baglist];
        private readonly Board board = new();

        public IPlayer CurrentPlayer => _players[currentPlayerIndex];

        private IPiece? pieceToPlay = null;

        public void Run()
        {
            OnGameStarted(new GameStartedEventArgs(board, bag, CurrentPlayer));
            FirstTurn();
            while (!_rulesManager.IsGameOver(bag, board))
            {
                Turn();
            }
        }

        private void RequestNewPiece()
        {
            var args = new AskPieceToPlayEventArgs(CurrentPlayer, GetAvailablePieces(), pieceToPlay);
            AskPieceToPlay?.Invoke(this, args);

            if (args.PieceToPlay is null)
                throw new InvalidOperationException("No piece was selected.");

            pieceToPlay = args.PieceToPlay;
        }

        private void FirstTurn()
        {
            SwitchCurrentPlayer();
            RequestNewPiece();
            SwitchCurrentPlayer();
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
                turnNumber ++;

            if (turnNumber  >= 4)
                OnQuarto(new QuartoEventArgs(_rulesManager, board, CurrentPlayer));

            if (pieceToPlay is null)
                throw new InvalidOperationException("Piece not selected before usage.");
            CurrentPlayer.PlayTurn(board, pieceToPlay, this);
            RequestNewPiece();
            SwitchCurrentPlayer();
        }


        public void SwitchCurrentPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % _players.Length;
        }        

        private void Display()
        {
            OnDisplayMessage?.Invoke(this, new MessageEventArgs($"Tour: {turnNumber}"));
            OnDisplayMessage?.Invoke(this, new MessageEventArgs($"Joueur courant: {CurrentPlayer.Name}"));
            OnBoardChanged(new BoardChangedEventArgs(board));
            OnDisplayMessage?.Invoke(this, new MessageEventArgs(bag.ToString())); // Faire des méthodes Display parcourant les éléments
            OnDisplayMessage?.Invoke(this, new MessageEventArgs($"Piece à jouer: {pieceToPlay}"));
        }
    }
}