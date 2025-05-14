using Manager.CustomEventArgs;

namespace Manager
{
    public interface IGameManager
    {
        event EventHandler<MessageEventArgs> OnDisplayMessage;
        event EventHandler<PlayerNameRequestedEventArgs> OnPlayerNameRequested;

        void Run();
    }
}
