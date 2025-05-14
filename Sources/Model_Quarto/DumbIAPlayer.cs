using Manager;

namespace Model
{
    public class DumbAIPlayer : AIPlayer
    {
        public DumbAIPlayer() : base("Dumb AI") { }

        public override void PlayTurn(IBoard board)
        {
            // Implémentation d'une IA bête (ex: choisir un coup au hasard)
        }
    }
}
