namespace Model
{
    public class PlayerHumain(string pseudo) : Player(pseudo)
    {
        public void AddWin() => NbWin++;
    }
}
