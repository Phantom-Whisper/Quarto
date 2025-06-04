using Manager;
using Manager.CustomEventArgs;
using Serialize;

namespace Model
{
    /// <summary>
    /// Main class that manage the game by setting up events
    /// </summary>
    public class GameManager(IRulesManager rules, IScoreManager scoreManager, IBoard board, IBag bag, IPlayer[] players) : IGameManager
    {
        private int _turnNumber  = 0;
        private bool _hasWinner = false;
        private GameLog? _gameLog;

        public event EventHandler<MessageEventArgs>? MessageRequested;
        public void OnDisplayMessage(string message) => MessageRequested?.Invoke(this, new MessageEventArgs(message));

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
        /// Tells the bag has changed
        /// </summary>
        public event EventHandler<BagChangedEventArgs>? BagChanged;
        private void OnBagChanged(BagChangedEventArgs args) => BagChanged?.Invoke(this, args);

        /// <summary>
        /// Event telling that someone choose the piece that the opponent gonna play
        /// </summary>
        public event EventHandler<AskPieceToPlayEventArgs> ? AskPieceToPlay;
        public void OnAskPieceToPlay(AskPieceToPlayEventArgs args) => AskPieceToPlay?.Invoke(this, args);

        /// <summary>
        /// Tells we have a winner
        /// </summary>
        public event EventHandler<GameEndEventArgs>? GameEnd;
        private void OnGameEnd(GameEndEventArgs args) => GameEnd?.Invoke(this, args);

        public event EventHandler<AskCoordinatesEventArgs>? AskCoordinate;
        private void OnAskedCoordinate(AskCoordinatesEventArgs args) => AskCoordinate?.Invoke(this, args);

        /// <summary>
        /// List of players
        /// </summary>

        /// <summary>
        /// The index of the current player in the list
        /// </summary>
        private int currentPlayerIndex;

        /// <summary>
        /// Rules of the game according to the level chosen (easy, normal and advanced)
        /// </summary>
        private IRulesManager RulesManager => rules;

        /// <summary>
        /// List of the piece available
        /// </summary>
        /// <returns>the list of the <c>Piece available</c></returns>
        public List<IPiece> GetAvailablePieces() => [.. bag.Baglist];

        /// <summary>
        /// Player which is playing 
        /// </summary>
        public IPlayer CurrentPlayer => players[currentPlayerIndex];

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
            _gameLog = new GameLog(DateTime.Now);
            OnGameStarted(new GameStartedEventArgs(board, bag, CurrentPlayer));
            FirstTurn();
            while (!RulesManager.IsGameOver(bag, board) && !_hasWinner) // fin du jeu -> si un joueur à gagner ou si baglist est vide et board plein
            {
                Display();
                Turn();
            }

            if (_hasWinner && CurrentPlayer is HumanPlayer humanPlayer)
            {
                scoreManager.AddVictory(humanPlayer);
                scoreManager.SaveScores();
            }
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
            bag.Remove(pieceToPlay);
            SwitchCurrentPlayer();
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
                _turnNumber++;

            if (pieceToPlay is null)
                throw new InvalidOperationException("Piece not selected before usage.");

            CurrentPlayer.PlayTurn(board, pieceToPlay, this);

            bag.Remove(pieceToPlay);

            try
            {
                var (row, col) = board.PositionPiece(pieceToPlay); // get last placed piece position

                var turnLog = new TurnLog(
                    _turnNumber,
                    CurrentPlayer.Name,
                    (Piece)pieceToPlay,
                    row,
                    col
                );

                _gameLog!.AddTurn(turnLog);

                // Save with unique filename per game
                string fileName = $"GameLog_{_gameLog.GameStartTime:yyyy-MM-dd_HH-mm-ss}.xml";
                GameSerializer.Save(_gameLog, fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logging failed: {ex.Message}");
            }

            var quartoPieces = RulesManager.GetQuarto(board);
            if (quartoPieces != null && quartoPieces.Distinct().Count() == 4)
            {
                if (CurrentPlayer is not null)
                {
                    OnQuarto(new QuartoEventArgs(rules, board, CurrentPlayer, quartoPieces));
                }
                else
                {
                    throw new InvalidOperationException("Unknown player type.");
                }

                _hasWinner = true;
                _gameLog!.Winner = CurrentPlayer.Name;
                string fileName = $"GameLog_{_gameLog.GameStartTime:yyyy-MM-dd_HH-mm-ss}.xml";
                GameSerializer.Save(_gameLog, fileName);
                OnGameEnd(new GameEndEventArgs(CurrentPlayer));
                return;
            }

            RequestNewPiece();
            SwitchCurrentPlayer();
        }

        /// <summary>
        /// method that changed the current player
        /// </summary>
        public void SwitchCurrentPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }

        /// <summary>
        /// Requests coordinates input from the specified player by raising an event,
        /// and waits synchronously for the player's response.
        /// </summary>
        /// <param name="player">The player from whom to request coordinates.</param>
        /// <returns>
        /// A tuple containing the row and column coordinates selected by the player.
        /// </returns>
        /// <remarks>
        /// This method blocks synchronously until the player provides valid coordinates.
        /// Will consider using an asynchronous version to avoid blocking the calling thread.
        /// </remarks>
        public (int row, int col) RequestCoordinates(IPlayer player)
        {
            var tcs = new TaskCompletionSource<(int row, int col)>();

            OnAskedCoordinate(new AskCoordinatesEventArgs(player, board, coords =>
            {
                tcs.SetResult(coords);
            }));

            return tcs.Task.Result;
        }

        /// <summary>
        /// Displays the current game state by triggering appropriate events.
        /// </summary>
        internal void Display()
        {
            OnDisplayMessage($"Tour: {_turnNumber}");
            OnDisplayMessage($"Joueur courant: {CurrentPlayer.Name}");
            OnBoardChanged(new BoardChangedEventArgs(board));
            OnBagChanged(new BagChangedEventArgs(bag));
            OnDisplayMessage($"Piece à jouer: {pieceToPlay}");
        }
    }
}