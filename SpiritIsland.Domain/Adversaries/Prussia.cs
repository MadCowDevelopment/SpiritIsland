using System;

namespace SpiritIsland.Domain.Adversaries
{
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
