using Manager;
using Manager.CustomEventArgs;
using System.Security.Cryptography;

namespace Model
{
    /// <summary>
    /// Main class that manage the game by setting up events
    /// </summary>
    public class GameManager(IRulesManager rules, IPlayer[] playersParam) : IGameManager
    {
        private int turnNumber  = 0;

        public event EventHandler<MessageEventArgs>? OnDisplayMessage;
        public event EventHandler<InputRequestedEventArgs>? OnInputRequested;

        /// <summary>
        /// Event telling that the game has started
        /// </summary>
        public event EventHandler<GameStartedEventArgs>? GameStarted;
        private void OnGameStarted(GameStartedEventArgs args) => GameStarted?.Invoke(this, args);

        /// <summary>
        /// Event telling that someone declare a Quarto
        /// </summary>
        public event EventHandler<QuartoEventArgs>? Quarto;
        private void OnQuarto(QuartoEventArgs args) => Quarto?.Invoke(this, args);

        /// <summary>
        /// Event telling that the board has changed
        /// </summary>
        public event EventHandler<BoardChangedEventArgs>? BoardChanged;
        private void OnBoardChanged(BoardChangedEventArgs args) => BoardChanged?.Invoke(this, args);

        /// <summary>
        /// Event telling that someone choose the piece that the opponent gonna play
        /// </summary>
        public event EventHandler<AskPieceToPlayEventArgs> ? AskPieceToPlay;
        private void OnAskPieceToPlay(AskPieceToPlayEventArgs args) => AskPieceToPlay?.Invoke(this, args);

        /// <summary>
        /// List of players
        /// </summary>
        private IPlayer[] _players => playersParam;

        /// <summary>
        /// The index of the current player in the list
        /// </summary>
        private int currentPlayerIndex = 0;

        /// <summary>
        /// Rules of the game according to the level chosen (easy, normal and advanced)
        /// </summary>
        private IRulesManager _rulesManager => rules;
        
        /// <summary>
        /// <c>Bag</c> contaning the piece that the players can play
        /// </summary>
        readonly Bag bag = new();

        /// <summary>
        /// List of the piece available
        /// </summary>
        /// <returns>the list of the <c>Piece available</c></returns>
        public List<IPiece> GetAvailablePieces() => [.. bag.Baglist];
        private readonly Board board = new();

        /// <summary>
        /// Player which is playing 
        /// </summary>
        public IPlayer CurrentPlayer => _players[currentPlayerIndex];

        /// <summary>
        /// <c>Piece</c> chosen by the opponent to by played by the current player
        /// </summary>
        private IPiece? pieceToPlay = null;

        /// <summary>
        /// Game loop :
        ///    - start : display of the board, pieces available and the name of the current player
        ///    - a loop of turn which consist of changing of current player, ask the piece the opponent has to put and switch again
        /// </summary>
        public void Run()
        {
            OnGameStarted(new GameStartedEventArgs(board, bag, CurrentPlayer));
            FirstTurn();
            while (!_rulesManager.IsGameOver(bag, board))
            {
                Display();
                Turn();
            }
            // fin du jeu -> si un joueur à gagner ou si baglist est vide et board plein
        }

        /// <summary>
        /// method that ask the player to choose a piece and add this piece to <c>PieceToPlay</c> so 
        /// the next player can play with this <c>Piece</c>
        /// </summary>
        /// <exception cref="InvalidOperationException"> if the piece is not selected </exception>
        private void RequestNewPiece()
        {
            var args = new AskPieceToPlayEventArgs(CurrentPlayer, GetAvailablePieces(), pieceToPlay);
            OnAskPieceToPlay(args);

            if (args.PieceToPlay is null)
                throw new InvalidOperationException("No piece was selected.");

            pieceToPlay = args.PieceToPlay;
        }

        /// <summary>
        /// method that does the exchange between players by switching between them, request the <c>PieceToPlay</c> to one player 
        /// and switch again with the player that play
        /// </summary>
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

        /// <summary>
        /// method of one turn of the game :
        ///    - Verify if there is quarto
        ///    - Play the piece 
        ///    - ask a new piece
        ///    - switch between the player
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
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

        /// <summary>
        /// method that changed the current player
        /// </summary>
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