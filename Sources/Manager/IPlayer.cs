namespace Manager
{
    public interface IPlayer
    {
        string Name { get; }
        Task<(int, int)?> PlayTurn(IBoard board, IPiece currentPiece);
    }
}
