using static Model.IGameManager;

namespace Model
{
    public interface IGameManager
    {
        public delegate void DisplayMessage(string message);
        public delegate void AskingPlayerName(out string name);

        public event DisplayMessage OnDisplayMessage;
        public event AskingPlayerName NameAsked;
        void LaunchGame();
    }

    public class GameManager : IGameManager
    {
        public event DisplayMessage? OnDisplayMessage;
        public event AskingPlayerName? NameAsked;

        Bag bag = new() { };
        readonly Player[]? players;

        public void CreatePlayers(bool solo)
        {
            string OutName = "null";
            string name;
            if (!solo)
            {
                for (int i = 0; i < 2;  i++)
                {
                    name = "Player " + (i + 1).ToString();
                    OnDisplayMessage?.Invoke($"Enter your name {name}");
                    NameAsked?.Invoke(out OutName);
                    if (NameAsked != null) players[i] = new PlayerHumain(OutName);
                }
            }
        }

        public static void Init(Bag bag)
        {
            List<Piece> listPiece = new List<Piece> {
                new(true, true, true, true),
                new(true, true, true, false),
                new(true, true, false, true),
                new(true, true, false, false),
                new(true, false, true, true),
                new(true, false, true, false),
                new(true, false, false, true),
                new(true, false, false, false),
                new(false, true, true, true),
                new(false, true, true, false),
                new(false, true, false, true),
                new(false, true, false, false),
                new(false, false, true, true),
                new(false, false, true, false),
                new(false, false, false, true),
                new(false, false, false, false)
                };

            foreach (var piece in listPiece)
            {
                bag.AddPiece(piece.IsSquare, piece.IsLight, piece.IsBig, piece.IsFull);
            }
        }

        public void LaunchGame()
        {
            Init(bag);
            
        }
    }
}
