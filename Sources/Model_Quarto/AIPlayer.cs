using Manager;
using System;
using System.Security.Cryptography;

namespace Model
{
    public abstract class AIPlayer : Player
    {
        protected AIPlayer(string name) : base(name) { }
    }
}
