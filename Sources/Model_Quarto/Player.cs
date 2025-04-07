namespace Model
{
    public class Player
    {

        public Player(string pseudo)
        {
            Pseudo = pseudo ?? throw new ArgumentNullException(nameof(pseudo));
            NbWin = 0;
        }

        public string Pseudo
        {
            get;
            init;
        }


        public int NbWin
        {
            get;
            set;
        }

        public void AddWin()
        {
            NbWin++;
        }        
    }
}
