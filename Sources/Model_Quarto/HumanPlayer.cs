using Manager;

namespace Model
{
    public class HumanPlayer  : Player
    {
        public HumanPlayer(string name) : base(name) { }

        public override void PlayTurn(IBoard board)
        {
            // La logique pour demander à l'utilisateur de jouer (via la console, interface, etc.)
        }
    }
}
