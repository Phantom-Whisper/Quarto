namespace Manager
{
    public interface IPlayer
    {
        string Name { get; }
        void PlayTurn(IBoard board, IPiece currentPiece, IGameManager gameManager);
    }
}
