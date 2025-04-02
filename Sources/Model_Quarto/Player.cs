using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Player
    {

        public Player(string pseudo, int nbWin)
        {
            Pseudo = pseudo;
            NbWin = nbWin;
        }

        public required string Pseudo
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
