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
            private int _currentPlayerIndex = 0;

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
            /// <summary>
            /// Player which is playing 
            /// </summary>
            private IPlayer? _currentPlayer;
            public IPlayer CurrentPlayer
            {
                get => _currentPlayer!;
                private set
                {
                    if (_currentPlayer != value)
                    {
                        _currentPlayer = value;
                        OnPropertyChanged(nameof(CurrentPlayer));
                    }
                }
            }

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

            private TaskCompletionSource<IPiece>? _pieceSelectionTcs;
            public TaskCompletionSource<IPiece>? PieceSelectionTcs => _pieceSelectionTcs;


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

            public bool HasWinner => _hasWinner;

            private readonly string? _fileName;
            public string? FileName
            {
                get; set;
            }

        private bool _isFirstTurnDone = false;
            public bool IsFirstTurnDone => _isFirstTurnDone;


        /// <summary>
        /// Game loop :
        ///    - start : display of the board, pieces available and the name of the current player
        ///    - a loop of turn which consist of changing of current player, ask the piece the opponent has to put and switch again
        /// </summary>
            public async Task Run()
            {
                _gameLog = new GameLog(DateTime.Now);
                OnGameStarted(new GameStartedEventArgs(board, bag, CurrentPlayer));

                await FirstTurnAsync();
                
                _isFirstTurnDone = true;

                while (!rules.IsGameOver(bag, board) && !_hasWinner)
                {
                    Display();

                    if (CurrentPlayer is AIPlayer)
                    {
                        await Turn();
                    }
                    else
                    {
                        break;
                    }
                }
            }

            /// <summary>
            /// method that ask the player to choose a piece and add this piece to <c>PieceToPlay</c> so 
            /// the next player can play with this <c>Piece</c>
            /// </summary>
            /// <exception cref="InvalidOperationException"> if the piece is not selected </exception>
            private async Task RequestNewPiece()
            {
                var args = new AskPieceToPlayEventArgs(CurrentPlayer, GetAvailablePieces(), null);

                _pieceSelectionTcs = new TaskCompletionSource<IPiece>();

                OnAskPieceToPlay(args); 

                var selectedPiece = await _pieceSelectionTcs.Task; 

                _pieceToPlay = selectedPiece;

            }

            /// <summary>
            /// method that does the exchange between players by switching between them, request the <c>PieceToPlay</c> to one player 
            /// and switch again with the player that play
            /// </summary>
            private async Task FirstTurnAsync()
            {
                SwitchCurrentPlayer();
                await RequestNewPiece();
                bag.Remove(_pieceToPlay);
                SwitchCurrentPlayer();
                _isFirstTurnDone = true;
            }

            public async Task ExecuteTurn(int row, int col)
            {
                if (_currentPlayerIndex == 0)
                {
                    TurnNumber++;
                }
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

                    FileName = $"GameLog_{_gameLog.GameStartTime:yyyy-MM-dd_HH-mm-ss}.xml";
                    GameSerializer.Save(_gameLog, FileName);
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
                    FileName = $"GameLog_{_gameLog.GameStartTime:yyyy-MM-dd_HH-mm-ss}.xml";
                    GameSerializer.Save(_gameLog, FileName);
                    OnGameEnd(new GameEndEventArgs(CurrentPlayer));
                    
                }

                if (_hasWinner && CurrentPlayer is HumanPlayer humanPlayer)
                {
                    scoreManager.LoadScores();
                    scoreManager.AddVictory(humanPlayer);
                    scoreManager.SaveScores();
                    return;
                }

                _pieceToPlay = null;
                await RequestNewPiece();
                bag.Remove(_pieceToPlay);
                SwitchCurrentPlayer();

            if (CurrentPlayer is AIPlayer)
            {
                await Turn();
            }
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
                if (_currentPlayerIndex == 0)
                {
                    TurnNumber++;
                    OnPropertyChanged(nameof(TurnNumber));
                }

                if (_pieceToPlay is null)
                    throw new InvalidOperationException("Piece not selected before usage.");

                if (CurrentPlayer is AIPlayer ai)
                {
                    var aiCoords = await ai.PlayTurn(board, _pieceToPlay);
                    if (aiCoords != null)
                    {
                        await ExecuteTurn(aiCoords.Value.Item1, aiCoords.Value.Item2);
                    }
                    else
                    {
                        OnDisplayMessage("L'IA n'a pas réussi à jouer.");
                    }
                }
            }

            /// <summary>
            /// method that changed the current player
            /// </summary>
            public void SwitchCurrentPlayer()
            {
                _currentPlayerIndex = (_currentPlayerIndex + 1) % players.Length;
                CurrentPlayer = players[_currentPlayerIndex];
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