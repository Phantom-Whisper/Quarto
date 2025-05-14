using Manager;
using Manager.CustomEventArgs;
using Model;

/*
Board board = new(4, 4);

Piece piece = new Piece(true, true, true, false);
board.InsertPiece(piece, 2,2);


//IAPlayer playerIA = new();
HumanPlayer playerHumain = new("joueur");
//var win = playerIA.NbWin;
Console.WriteLine("-----------------------------------------------");
//Console.WriteLine(playerIA.Pseudo);
//Console.WriteLine(win);
Console.WriteLine(playerHumain.Pseudo);
Console.WriteLine(playerHumain.NbWin.ToString());
Console.WriteLine("-----------------------------------------------");

Console.WriteLine(board.ToString());
Console.WriteLine("----------------------------------");
Piece piecee = new(true,false, true, true);
Console.WriteLine(piecee.ToString());
board.InsertPiece(piecee, 1, 1);

Console.WriteLine(board.ToString());

Console.WriteLine("----------------------------------");

GameManager gameManager = new GameManager();

gameManager.LaunchGame();*/

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameManager = new GameManager();

            gameManager.OnPlayerNameRequested += PlayerNameRequested;

            gameManager.CreatePlayers(false);

            Console.WriteLine("point d'arrêt");
        }

        private static void PlayerNameRequested(object? sender, PlayerNameRequestedEventArgs e)
        {
            Console.Write($"Entrez le nom du Joueur {e.PlayerIndex + 1} : ");
            e.PlayerName = Console.ReadLine();
        }
    }
}