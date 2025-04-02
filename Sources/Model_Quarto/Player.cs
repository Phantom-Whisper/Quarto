using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Player
    {

        public Player(string pseudo)
        {
            Pseudo = pseudo;
        }

        public required string Pseudo
        {
            get;
            init;
        }

        private int nbWin;

        public int NbWin
        {
            get => nbWin;
            init
            {
                nbWin = 0;
            }
        }

        public void AddWin()
        {
            ++nbWin;
        }
    }
}
