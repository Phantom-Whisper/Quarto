using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class TurnLog
    {
        public int TurnNumber { get; set; }
        public string? CurrentPlayerName { get; set; }
        public string? PiecePlayed { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        // Required by XmlSerializer
        public TurnLog() { }

        public TurnLog(int turnNumber, string playerName, Piece piece, int row, int col)
        {
            TurnNumber = turnNumber;
            CurrentPlayerName = playerName;
            PiecePlayed = piece.ToString();
            Row = row;
            Col = col;
        }
    }
}
