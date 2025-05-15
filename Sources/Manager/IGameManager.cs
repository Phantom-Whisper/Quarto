using Manager.CustomEventArgs;
using System.IO.Pipelines;

namespace Manager
{
    public interface IGameManager
    {
        event EventHandler<MessageEventArgs> OnDisplayMessage;
        event EventHandler<PlayerNameRequestedEventArgs> OnPlayerNameRequested;

        void DisplayMessage(string message);
        void RequestInput(string prompt, Action<string?> callback);

        List<IPiece> GetAvailablePieces();


        void Run();
    }
}
