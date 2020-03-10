using System;

namespace SpiritIsland.Domain
{
    public interface IGame 
    {
        void Explore();
        void Advance();
        event Action GameLost;

        void Start();
    }
}