using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IQuartoAI
    {
        void Move(Board board, Piece piece);
        Piece ChooseNextPiece(Bag bag);
    }

    public class StubAI : IQuartoAI
    {
        Random random = new Random();

        public List<(int x, int y)> GetAvailablePositions(Board board)
        {
            var list = new List<(int x, int y)>();

            for (int x = 0; x < board.SizeX; x++)
            {
                for (int y = 0; y < board.SizeY; y++)
                {
                    if (board.IsEmpty(x, y))
                    {
                        list.Add((x, y));
                    }
                }
            }

            return list;
        }

        public void Move(Board board, Piece piece) 
        {
            List<(int x, int y)> positions = GetAvailablePositions(board);
            if (positions.Count == 0) throw new InvalidOperationException("No position available.");

            var randomPosition = positions[random.Next(positions.Count)];
            board.InsertPiece(piece, randomPosition.x, randomPosition.y);

        }

        public Piece ChooseNextPiece(Bag bag)
        {
            int index = random.Next(bag.Baglist.Count); 
            return bag.GetPiece(index);
        }
    }
}
