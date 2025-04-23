using Model;


Board board = new(4, 4);

Piece piece = new Piece(true, true, true, false);

board.InsertPiece(piece, 2,2);

//Dictionary<string, int> dP1 =  board.PositionPiece(piece);
//Console.WriteLine(dP1["y"]);

PlayerIA playerIA = new();
PlayerHumain playerHumain = new("joueur");
var win = playerIA.NbWin;
Console.WriteLine(playerIA.Pseudo);
Console.WriteLine(win);
Console.WriteLine(playerHumain.Pseudo);
Console.WriteLine(playerHumain.NbWin.ToString());