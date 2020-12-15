using System;

namespace SpiritIsland.Domain.Adversaries
{
    public abstract class Adversary
    {
        public abstract string DisplayName { get; }
        public int Level { get; private set; }

        public Action<string> ShowMessageRequested;

        public void Initialize(int level)
        {
            Level = level;
        }
    }
}
