using Manager;
using System;
using System.Security.Cryptography;

namespace Model
{
    /*public abstract class AIPlayer : Player
    {
        /// <summary>
        /// This is the <c>Ctor</c> of the Class <c>PlayerIA</c> with the pseudo : IA.
        /// </summary>
        
        public static void Move(Rules rules, Board board, Piece piece)
        {
            List<(int row, int col)> positions = rules.GetAvailablePositions(board); // Ask Rules the free positions
            if (positions.Count == 0) throw new InvalidOperationException("No position available.");
            using var randomGenerator = RandomNumberGenerator.Create();
            byte[] data = new byte[4];
            randomGenerator.GetBytes(data);

            int randomInt = BitConverter.ToInt32(data, 0);

            randomInt = Math.Abs(randomInt);

            var randomPosition = positions[randomInt % positions.Count];
            board.InsertPiece(piece, randomPosition.row, randomPosition.col); // Ask Rules if he can play a move here

        }

        public static Piece ChooseNextPiece(Bag bag)
        {
            using var randomGenerator = RandomNumberGenerator.Create();

            byte[] data = new byte[4];
            randomGenerator.GetBytes(data);

            int randomInt = BitConverter.ToInt32(data, 0);

            randomInt = Math.Abs(randomInt);

            //return bag.TakePiece(randomInt % bag.Baglist.Count);
            return null;
        }
    }*/

    public abstract class AIPlayer : Player
    {
        protected AIPlayer(string name) : base(name) { }
    }
}
