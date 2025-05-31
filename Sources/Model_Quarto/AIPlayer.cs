using Manager;
using System;
using System.Security.Cryptography;

namespace Model
{
    /// <summary>
    /// Abstract class of an IA player 
    /// </summary>
    /// <remarks>
    /// Constructor based on the mother class
    /// </remarks>
    /// <param name="name">name of the IA player</param>
    public abstract class AIPlayer(string name) : Player(name)
    {
    }
}
