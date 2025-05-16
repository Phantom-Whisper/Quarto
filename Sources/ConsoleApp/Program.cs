using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Text;
using Manager;
using Manager.CustomEventArgs;
using Model;

namespace ConsoleApp
{
    class Program
    {
        private static int Menu()
        {
            int choice;
            Console.WriteLine("   ___                   _        ");
            Console.WriteLine("  / _ \\ _   _  __ _ _ __| |_ ___  ");
            Console.WriteLine(" | | | | | | |/ _` | '__| __/ _ \\ ");
            Console.WriteLine(" | |_| | |_| | (_| | |  | || (_) |");
            Console.WriteLine("  \\__\\_\\\\__,_|\\__,_|_|   \\__\\___/ \r");
            Console.WriteLine("");
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

            

            int choice = Menu();

            switch(choice)
            {
                case 1:
                    Console.Write("Mode solo ? (y/n) ");
                    bool solo = Console.ReadLine()?.Trim().ToLower() == "y";
                    ChooseDifficulty(rulesManager);
                    CreatePlayers(solo, players);

                    var gameManager = new GameManager(rulesManager, players);
                    
                    gameManager.GameStarted += GameStarted;
                    gameManager.OnDisplayMessage += DisplayMessage;
                    gameManager.OnInputRequested += RequestInput;
                    gameManager.OnPlayerNameRequested += PlayerNameRequested;
                    gameManager.Quarto += Quarto;
                    gameManager.BoardChanged += BoardChange;
                    gameManager.AskPieceToPlay += AskPieceToPlay;

                    gameManager.Run();
                    break;
                default:
                    break;

            }

            Console.WriteLine("point d'arrêt");
        }

        private static void ChooseDifficulty(IRulesManager rulesManager)
        {
            Console.WriteLine("Choisissez la difficulté :");
            Console.WriteLine("1. Débutant");
            Console.WriteLine("2. Normal");
            Console.WriteLine("3. Avancé");

            string? input = Console.ReadLine();
            rulesManager = input switch
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
                var args = new PlayerNameRequestedEventArgs(0);

                string name = string.IsNullOrWhiteSpace(args.PlayerName)
                    ? "Player1"
                    : args.PlayerName;

                players[0] = new HumanPlayer(name);

                players[1] = new DumbAIPlayer();
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    var args = new PlayerNameRequestedEventArgs(i);

                    string name = string.IsNullOrWhiteSpace(args.PlayerName)
                        ? $"Player{i + 1}"
                        : args.PlayerName;

                    players[i] = new HumanPlayer(name);
                }
            }
        }

        private static void GameStarted(object? sender, GameStartedEventArgs e)
        {
            Console.WriteLine(e.Board.ToString());
            Console.WriteLine(e.Bag.ToString());
            Console.WriteLine(e.CurrentPlayer.Name);
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

        private static void PlayerNameRequested(object? sender, PlayerNameRequestedEventArgs e)
        {
            Console.Write($"Entrez le nom du Joueur {e.PlayerIndex + 1} : ");
            e.PlayerName = Console.ReadLine();
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
            var sb = new StringBuilder();
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

        }
    }
}