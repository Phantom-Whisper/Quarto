namespace Model
{
    public class PlayerHumain(string pseudo) : Player(pseudo)
    {

        /// <summary>
        /// This method increment the number of games won
        /// </summary>
        public void AddWin() => NbWin++;
    }
}
