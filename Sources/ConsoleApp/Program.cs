using Model;


Board board = new(4, 4);

Piece piece = new Piece(true, true, true, false);
var ia = new StubAI();
board.InsertPiece(piece, 2,2);
var list = ia.GetAvailablePositions(board);

//Dictionary<string, int> dP1 =  board.PositionPiece(piece);
//Console.WriteLine(dP1["y"]);

PlayerIA playerIA = new();
PlayerHumain playerHumain = new("joueur");
var win = playerIA.NbWin;
Console.WriteLine("-----------------------------------------------");
Console.WriteLine(playerIA.Pseudo);
Console.WriteLine(win);
Console.WriteLine(playerHumain.Pseudo);
Console.WriteLine(playerHumain.NbWin.ToString());
Console.WriteLine("-----------------------------------------------");
foreach(var item in list) { Console.WriteLine(item); }
ia.Move(board, piece);
Console.WriteLine(board.ToString());
Console.WriteLine("----------------------------------");
Piece piecee = new(true,false, true, true);
Console.WriteLine(piecee.ToString());
board.InsertPiece(piecee, 1, 1);

Console.WriteLine(board.ToString());
int index = 0;
/*foreach (Piece pieceE in pieces)
{
    Console.Write($"{index}. ");
    Console.WriteLine(pieceE.ToString());
    index++;
}*/

