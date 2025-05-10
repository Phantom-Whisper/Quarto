using System;
using System.Security.Cryptography;

namespace Model
{
    public class PlayerIA : Player
    {
        /// <summary>
        /// This is the <c>Ctor</c> of the Class <c>PlayerIA</c> with the pseudo : IA.
        /// </summary>
        public PlayerIA() : base("IA")
        {
        }

        public static List<(int x, int y)> GetAvailablePositions(Board board)
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

        public static void Move(Board board, Piece piece)
        {
            List<(int x, int y)> positions = GetAvailablePositions(board);
            if (positions.Count == 0) throw new InvalidOperationException("No position available.");
            using (var randomGenerator = RandomNumberGenerator.Create())
            {

                byte[] data = new byte[4];
                randomGenerator.GetBytes(data);

                int randomInt = BitConverter.ToInt32(data, 0);

                randomInt = Math.Abs(randomInt);

                var randomPosition = positions[randomInt % positions.Count];
                board.InsertPiece(piece, randomPosition.x, randomPosition.y);
            }

        }

        public static Piece ChooseNextPiece(Bag bag)
        {
            using (var randomGenerator = RandomNumberGenerator.Create())
            {

                byte[] data = new byte[4];
                randomGenerator.GetBytes(data);

                int randomInt = BitConverter.ToInt32(data, 0);

                randomInt = Math.Abs(randomInt);

                return bag.GetPiece(randomInt % bag.Baglist.Count);
            }
        }
    }
}
