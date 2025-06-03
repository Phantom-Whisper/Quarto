using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Model
{
    [Serializable]
    [XmlRoot("GameLog")]
    public class GameLog
    {
        [XmlAttribute]
        public DateTime GameStartTime { get; set; }

        [XmlElement("Turn")]
        public List<TurnLog> Turns { get; set; } = [];

        [XmlElement("Winner")]
        public string? Winner { get; set; }

        public GameLog() { }

        public GameLog(DateTime gameStartTime)
        {
            GameStartTime = gameStartTime;
        }

        public void AddTurn(TurnLog turn)
        {
            Turns.Add(turn);
        }
    }
}
