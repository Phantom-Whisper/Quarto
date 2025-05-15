using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.CustomEventArgs
{
    public class InputRequestedEventArgs(string prompt, Action<string?> callback) : EventArgs
    {
        public string Prompt { get; } = prompt;
        public Action<string?> Callback { get; } = callback;
    }
}
