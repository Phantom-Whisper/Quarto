using Model;
using System.IO.Pipelines;


Board board = new(4, 4);

List<Piece> pieces = [new Piece(true, true, true, false),
new Piece(true, true, true, true),
new Piece(true, true, false, false),
new Piece(true, true, false, true),
new Piece(true, false, true, false),
new Piece(true, false, true, true),
new Piece(true, false, false, false),
new Piece(true, false, false, true),

new Piece(false, true, true, false),
new Piece(false, true, true, true),
new Piece(false, true, false, false),
new Piece(false, true, false, true),
new Piece(false, false, true, false),
new Piece(false, false, true, true),
new Piece(false, false, false, false),
new Piece(false, false, false, true)];


board.InsertPiece(pieces[5], 2,2);

Console.WriteLine(board.ToString());
int index = 0;
foreach (Piece piece in pieces)
{
    Console.Write($"{index}. ");
    Console.WriteLine(piece.ToString());
    index++;
}

Console.WriteLine("----------------------------------");
Piece piecee = new(true, true, true, true);
Console.WriteLine(piecee.ToString());
