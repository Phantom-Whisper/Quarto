using Manager;
using System;
using System.Security.Cryptography;

namespace Model
{
    /// <summary>
    /// Abstract class of an IA player 
    /// </summary>
    public abstract class AIPlayer : Player
    {
        /// <summary>
        /// Constructor based on the mother class
        /// </summary>
        /// <param name="name">name of the IA player</param>
        protected AIPlayer(string name) : base(name) { }
    }
}
