using Manager;
using Manager.CustomEventArgs;
using Serialize;
using System.ComponentModel;

namespace Model
{
    /// <summary>
    /// Main class that manage the game by setting up events
    /// </summary>
    public class GameManager(IRulesManager rules, IScoreManager scoreManager, IBoard board, IBag bag, IPlayer[] players) : IGameManager, INotifyPropertyChanged
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

        /// <summary>
        /// Tells a property has changed
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

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
        private IPiece? _pieceToPlay;
        public IPiece? PieceToPlay
        {
            get => _pieceToPlay;
            set
            {
                if (_pieceToPlay != value)
                {
                    _pieceToPlay = value;
                    OnPropertyChanged(nameof(PieceToPlay));
                }
            }
        }

        public IBoard? Board => board;

        public IBag? Bag => bag;

        public int TurnNumber
        {
            get => _turnNumber;
            set
            {
                if (_turnNumber != value)
                {
                    _turnNumber = value;
                    OnPropertyChanged(nameof(TurnNumber));
                }
            }
        }

        /// <summary>
        /// Game loop :
        ///    - start : display of the board, pieces available and the name of the current player
        ///    - a loop of turn which consist of changing of current player, ask the piece the opponent has to put and switch again
        /// </summary>
        public async Task Run()
        {
            _gameLog = new GameLog(DateTime.Now);
            OnGameStarted(new GameStartedEventArgs(board, bag, CurrentPlayer));

            FirstTurn();

            while (!rules.IsGameOver(bag, board) && !_hasWinner)
            {
                Display();
                await Turn();
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
            var args = new AskPieceToPlayEventArgs(CurrentPlayer, GetAvailablePieces(), _pieceToPlay);
            OnAskPieceToPlay(args);

            if (args.PieceToPlay is null)
                throw new InvalidOperationException("No piece was selected.");

            _pieceToPlay = args.PieceToPlay;
        }

        /// <summary>
        /// method that does the exchange between players by switching between them, request the <c>PieceToPlay</c> to one player 
        /// and switch again with the player that play
        /// </summary>
        private void FirstTurn()
        {
            SwitchCurrentPlayer();
            RequestNewPiece();
            bag.Remove(_pieceToPlay);
            SwitchCurrentPlayer();
        }

        public void ExecuteTurn(int row, int col)
        {
            bool placed = Rules.PlayAMove((Piece)_pieceToPlay!, row, col, (Board)board);

            if (!placed)
            {
                OnDisplayMessage("Invalid move. The move will be skipped.");
                return;
            }

            bag.Remove(_pieceToPlay);

            try
            {
                var (placedRow, placedCol) = board.PositionPiece(_pieceToPlay!); // si différent, sinon utiliser `row,col`

                var turnLog = new TurnLog(
                    TurnNumber,
                    CurrentPlayer.Name,
                    (Piece)_pieceToPlay!,
                    placedRow,
                    placedCol
                );

                _gameLog!.AddTurn(turnLog);

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
        /// method of one turn of the game :
        ///    - Verify if there is quarto
        ///    - Play the piece 
        ///    - ask a new piece
        ///    - switch between the player
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task Turn()
        {
            if (currentPlayerIndex == 0)
                TurnNumber++;

            if (_pieceToPlay is null)
                throw new InvalidOperationException("Piece not selected before usage.");

            var coords = await CurrentPlayer.PlayTurn(board, _pieceToPlay);

            if (coords == null)
            {
                // UI mode — attendre que la UI appelle ExecuteTurn()
                return;
            }

            ExecuteTurn(coords.Value.Item1, coords.Value.Item2);
        }

        /// <summary>
        /// method that changed the current player
        /// </summary>
        public void SwitchCurrentPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }

        /// <summary>
        /// Displays the current game state by triggering appropriate events.
        /// </summary>
        internal void Display()
        {
            OnDisplayMessage($"Tour: {TurnNumber}");
            OnDisplayMessage($"Joueur courant: {CurrentPlayer.Name}");
            OnBoardChanged(new BoardChangedEventArgs(board));
            OnBagChanged(new BagChangedEventArgs(bag));
            OnDisplayMessage($"Piece à jouer: {_pieceToPlay}");
        }
    }
}