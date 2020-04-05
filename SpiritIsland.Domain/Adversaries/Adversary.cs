using System;
using System.Linq;

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

    public class Sweden : Adversary, IBeforeInitialExplore
    {
        public override string DisplayName { get; } = "The Kingdom of Sweden";

        public void Handle(Game game)
        {
            var nextCard = game.InvaderDeck.Dequeue();
            ShowMessageRequested?.Invoke($"Royal Backing: On each board, before explore add 1 town to the {nextCard.Lands.First()} with the fewest invaders.");
        }
    }

    public class Prussia : Adversary, IAffectsInvaderDeckOrder
    {
        public override string DisplayName { get; } = "The Kingdom of Brandenburg-Prussia";

        public string InvaderDeckOrder
        {
            get
            {
                switch (Level)
                {
                    case 1: return "111222233333";
                    case 2: return "111322223333";
                    case 3: return "11322223333";
                    case 4: return "1132223333";
                    case 5: return "132223333";
                    case 6: return "32223333";
                    default: throw new InvalidOperationException("Only levels 1-6 are supported.");
                }
            }
        }
    }
}
