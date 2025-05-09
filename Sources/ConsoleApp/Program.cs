using Model;


Board board = new(4, 4);

Piece piece = new Piece(true, true, true, false);
board.InsertPiece(piece, 2,2);


PlayerIA playerIA = new();
PlayerHumain playerHumain = new("joueur");
var win = playerIA.NbWin;
Console.WriteLine("-----------------------------------------------");
Console.WriteLine(playerIA.Pseudo);
Console.WriteLine(win);
Console.WriteLine(playerHumain.Pseudo);
Console.WriteLine(playerHumain.NbWin.ToString());
Console.WriteLine("-----------------------------------------------");

Console.WriteLine(board.ToString());
Console.WriteLine("----------------------------------");
Piece piecee = new(true,false, true, true);
Console.WriteLine(piecee.ToString());
board.InsertPiece(piecee, 1, 1);

Console.WriteLine(board.ToString());


