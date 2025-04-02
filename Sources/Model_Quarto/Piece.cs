using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Piece
    {
        public bool IsSquare
        {
            get;
            init;
        }
        public bool IsLight
        {
            get;
            init;
        }
        public bool IsBig
        {
            get;
            init;
        }
        public bool IsFull
        {
            get;
            init;
        }

        public Piece(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            IsSquare = isSquare;
            IsLight = isLight;
            IsBig = isBig;
            IsFull = isFull;
        }
    }
}
