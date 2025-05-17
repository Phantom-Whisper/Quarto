using System.Security.Cryptography;
using System.Text;
using Manager;
using Manager.CustomEventArgs;
using Model;

namespace ConsoleApp
{
    static class Program
    {
        private static int Menu()
        {
            int choice;
            Console.WriteLine(@"
               ___                   _        
              / _ \ _   _  __ _ _ __| |_ ___  
             | | | | | | |/ _` | '__| __/ _ \ 
             | |_| | |_| | (_| | |  | || (_) |
              \__\_\\__,_|\__,_|_|   \__\___/ 
            ");
            Console.WriteLine("1. Launch a game");

            Console.Write("Enter your choice: ");

            string? input = Console.ReadLine();

            while (!int.TryParse(input, out choice) || (choice != 1 && choice != 2))
            {
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                input = Console.ReadLine();
            }

            return choice;
        }

        static void Main(string[] args)
        {
            IRulesManager rulesManager = new Rules();
            IPlayer[] players = new IPlayer[2];
            IBoard board = new Board();

            /// <summary>
            /// <c>Bag</c> contaning the piece that the players can play
            /// </summary>
            IBag bag = new Bag();



            int choice = Menu();

            switch(choice)
            {
                case 1:
                    Console.Write("Mode solo ? (y/n) ");
                    bool solo = Console.ReadLine()?.Trim().ToLower() == "y";
                    rulesManager = ChooseDifficulty();
                    CreatePlayers(solo, players);

                    var gameManager = new GameManager(rulesManager, board, bag, players);
                    
                    gameManager.GameStarted += GameStarted;
                    gameManager.MessageRequested += DisplayMessage;
                    gameManager.InputRequested += RequestInput;
                    gameManager.Quarto += Quarto;
                    gameManager.BoardChanged += BoardChange;
                    gameManager.AskPieceToPlay += AskPieceToPlay;
                    gameManager.AskCoordinate += AskCoordinate;
                    gameManager.BagChanged += BagChange;
                    gameManager.GameEnd += GameEnd;


                    gameManager.Run();
                    break;
                default:
                    break;

            }
        }

        private static IRulesManager ChooseDifficulty()
        {
            Console.WriteLine("Choisissez la difficulté :");
            Console.WriteLine("1. Débutant");
            Console.WriteLine("2. Normal");
            Console.WriteLine("3. Avancé");

            string? input = Console.ReadLine();
            return input switch
            {
                "1" => new RulesBeginner(),
                "2" => new Rules(),
                "3" => new RulesAdvanced(),
                _ => new Rules(),
            };
        }

        private static void CreatePlayers(bool solo, IPlayer[] players)
        {
            if (solo)
            {
                Console.Write($"Entrez votre nom de joueur: ");

                string? name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = "Player1";
                }

                players[0] = new HumanPlayer(name);
                players[1] = new DumbAIPlayer();
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    Console.Write($"Entrez votre nom de joueur: ");

                    string? name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        name = $"Player{i+1}";
                    }

                    players[i] = new HumanPlayer(name);
                }
            }
        }

        private static void GameStarted(object? sender, GameStartedEventArgs e)
        {
            Console.Clear();
            Console.WriteLine("The game has started !");
        }

        private static void Quarto(object? sender, QuartoEventArgs e)
        {
            Console.WriteLine($"{e.CurrentPlayer.Name}, do you want to declare a Quarto? (y/n)");
            string? response = Console.ReadLine()?.Trim().ToLower();

            if (response != "y") return;

            List<(int row, int col)> selectedPositions = new();

            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine($"Select piece {i}: enter row:");
                if (!int.TryParse(Console.ReadLine(), out int row))
                {
                    Console.WriteLine("Invalid input. Aborting Quarto attempt.");
                    return;
                }

                Console.WriteLine("Enter column:");
                if (!int.TryParse(Console.ReadLine(), out int col))
                {
                    Console.WriteLine("Invalid input. Aborting Quarto attempt.");
                    return;
                }

                if (!e.Board.IsOnBoard(row, col) || e.Board.IsEmpty(row, col))
                {
                    Console.WriteLine("Invalid selection. Aborting Quarto attempt.");
                    return;
                }

                selectedPositions.Add((row, col));
            }

            var selectedPieces = selectedPositions
                .Select(pos => e.Board.GetPiece(pos.row, pos.col))
                .ToList();

            if (e.RulesManager.IsQuarto(e.Board, selectedPieces))
            {
                Console.WriteLine($"{e.CurrentPlayer.Name} wins with a Quarto!");
            }
            else
            {
                Console.WriteLine("Incorrect Quarto declaration. The game continues.");
            }
        }

        private static void DisplayMessage(object? sender, MessageEventArgs e) => Console.WriteLine(e.Message);

        private static void RequestInput(object? sender, InputRequestedEventArgs e)
        {
            Console.WriteLine(e.Prompt);
            string? input = Console.ReadLine();
            e.Callback(input);
        }

        private static void BoardChange(object? sender, BoardChangedEventArgs e)
        {
            StringBuilder sb = new();
            int col = 0, row = 0;

            for (int i = -1; i < e.Board.SizeY; i++)
            {
                if (i == -1)
                {
                    sb.Append("row/y | ");
                }
                else
                {
                    sb.AppendFormat("{0,4} | ", i);
                }
            }
            sb.AppendLine();

            string horizontalSeparator = new string('-', (e.Board.SizeY + 1) * 7);
            sb.AppendLine(horizontalSeparator);

            sb.AppendFormat("{0,2}  |", col);

            foreach (var piece in e.Board.Grid)
            {
                if (row == e.Board.SizeX)
                {
                    row = 0;
                    sb.AppendLine();
                    sb.AppendLine(horizontalSeparator);
                    col++;
                    sb.AppendFormat("{0,2}  |", col);
                }

                sb.AppendFormat("{0,5} |", piece?.ToString() ?? "");
                row++;
            }

            Console.WriteLine(sb.ToString());
        }

        private static void AskPieceToPlay(object? sender, AskPieceToPlayEventArgs e)
        {
            if (e.Player is AIPlayer)
            {
                using var randomGenerator = RandomNumberGenerator.Create();
                byte[] data = new byte[4];
                randomGenerator.GetBytes(data);

                int randomInt = BitConverter.ToInt32(data, 0);

                randomInt = Math.Abs(randomInt);

                e.PieceToPlay = e.Pieces[randomInt % e.Pieces.Count];
                return;
            }

            Console.Write("Enter the number of the piece you to your opponent: ");
            string? input = Console.ReadLine();

            while (int.TryParse(input, out int index))
            {
                index -= 1;
                if (index >= 0 && index < e.Pieces.Count)
                {
                    e.PieceToPlay = e.Pieces[index];
                    break;
                }
            }
        }

        private static void BagChange(object? sender, BagChangedEventArgs e)
        {
            int i = 1;
            foreach (var piece in e.Bag.Baglist)
            {
                Console.WriteLine($"{i}. {piece}");
                i++;
            }
        }

        private static void GameEnd(object? sender, GameEndEventArgs e)
        {
            Console.WriteLine($"Game Over! Winner: {e.Winner.Name}");
        }

        private static void AskCoordinate(object? sender, AskCoordinatesEventArgs e)
        {
            Console.WriteLine($"{e.Player.Name}, it's your turn to play.");

            int row = -1, col = -1;

            while (true)
            {
                Console.Write("Enter row (0 to 3): ");
                string? rowInput = Console.ReadLine();
                if (int.TryParse(rowInput, out row) && e.Board.IsOnBoard(row, 0))
                {
                    break;
                }
                Console.WriteLine("Invalid row. Please enter a number between 0 and 3.");
            }

            while (true)
            {
                Console.Write("Enter column (0 to 3): ");
                string? colInput = Console.ReadLine();
                if (int.TryParse(colInput, out col) && e.Board.IsOnBoard(0, col))
                {
                    break;
                }
                Console.WriteLine("Invalid column. Please enter a number between 0 and 3.");
            }

            e.CoordinateCallback((row, col));
        }
    }
}