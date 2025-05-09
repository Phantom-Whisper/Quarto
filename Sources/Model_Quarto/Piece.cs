namespace Model
{
    public class Piece : IEquatable<Piece>
    {
        /// <summary>
        /// This is the <c>Ctor</c> of the Class <c>Piece</c>.
        /// </summary>
        /// <param name="isSquare"> boolean of the shape : true = Square and false = Round </param>
        /// <param name="isLight"> boolean of the color : true = Light and false = Dark </param>
        /// <param name="isBig"> boolean of the size : true = Big and false = Small </param>
        /// <param name="isFull"> boolean of the state : true = Full and false = Hollow </param>
        public Piece(bool isSquare, bool isLight, bool isBig, bool isFull)
        {
            IsSquare = isSquare;
            IsLight = isLight;
            IsBig = isBig;
            IsFull = isFull;
        }

        /// <summary>
        /// This property tells if the  is squared (true) or round (false).
        /// </summary>
        public bool IsSquare
        {
            get;
            init;
        }

        /// <summary>
        /// This property tells if the <c>Piece</c> is light (true) or dark (false).
        /// </summary>
        public bool IsLight
        {
            get;
            init;
        }

        /// <summary>
        /// This property tells if the <c>Piece</c> is big (true) or small (false).
        /// </summary>
        public bool IsBig
        {
            get;
            init;
        }

        /// <summary>
        /// This property tells if the <c>Piece</c> is full (true) or hollow (false).
        /// </summary>
        public bool IsFull
        {
            get;
            init;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the <c>Piece</c>.
        /// </returns>
        public override string ToString()
        {
            string piece = "";

            piece += IsSquare ? "Square" : "Round";
            piece += IsLight ? " Light" : " Dark";
            piece += IsBig ? " Big" : " Small";
            piece += IsFull ? " Full" : " Hollow";

            return piece;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Piece) return false;

            return this.IsSquare == ((Piece)obj).IsSquare
                && this.IsLight == ((Piece)obj).IsLight
                && this.IsBig == ((Piece)obj).IsBig
                && this.IsFull == ((Piece)obj).IsFull;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsSquare, IsLight, IsBig, IsFull);
        }
    }
}
