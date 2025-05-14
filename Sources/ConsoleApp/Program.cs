using System.ComponentModel.Design;
using System.Runtime.InteropServices;
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
            var gameManager = new GameManager();

            gameManager.OnPlayerNameRequested += PlayerNameRequested;

            int choice = Menu();

            switch(choice)
            {
                case 1:
                    Console.Write("Playing against an AI (Y/N) ? ");
                    string? input = Console.ReadLine();
                    bool solo = false;
                    while ((input != "Y" && input != "N" && input != "y" && input != "n"))
                    {
                        Console.WriteLine("Invalid choice. Please enter Y or N.");
                        input = Console.ReadLine();
                    }
                    if (input == "Y" || input == "y")
                        solo = true;
                    gameManager.CreatePlayers(solo);
                    gameManager.Run();
                    break;
                default:
                    break;

            }

            Console.WriteLine("point d'arrêt");
        }

        private static void PlayerNameRequested(object? sender, PlayerNameRequestedEventArgs e)
        {
            Console.Write($"Entrez le nom du Joueur {e.PlayerIndex + 1} : ");
            e.PlayerName = Console.ReadLine();
        }

    }
}