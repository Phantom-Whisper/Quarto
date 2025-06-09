namespace Model
{
    public class PlayerData
    {
        public string Name { get; set; } = "";
        public string Type { get; set; } = "Human";
    }

    public class GameState
    {
        public Piece[][]? Board { get; set; }
        public string? CurrentPlayerName { get; set; }
        public List<PlayerData>? Players { get; set; }
        public Bag? Bag { get; set; }
        public int Turn { get; set; }
        public Piece? CurrentPiece { get; set; }
        public Rules? Rules { get; set; }

        public GameState() { }
    }

}