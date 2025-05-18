using Manager.CustomEventArgs;
using System.IO.Pipelines;

namespace Manager
{
    public interface IGameManager
    {
        event EventHandler<AskPieceToPlayEventArgs>? AskPieceToPlay;
        void OnAskPieceToPlay(AskPieceToPlayEventArgs args);
        List<IPiece> GetAvailablePieces();
        void OnDisplayMessage(string message);
        (int row, int col) RequestCoordinates(IPlayer player);
        void Run();
    }
}
