namespace Manager
{
    public interface IPlayer
    {
        string Name { get; }
        IPiece? PlayTurn(IBoard board, IPiece currentPiece, IGameManager gameManager);
    }
}
