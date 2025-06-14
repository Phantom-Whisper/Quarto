﻿namespace Manager.CustomEventArgs
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string message) 
        {
            Message = message;
        }

        public string Message { get; }
    }
}
