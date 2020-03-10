using System.Collections.Generic;

namespace SpiritIsland.Domain.Cards
{
    public class InvaderCard
    {
        public InvaderCard(int stage, IEnumerable<Land> lands, bool escalation)
        {
            Stage = stage;
            Lands = lands;
            Escalation = escalation;
        }

        public int Stage { get; }
        public IEnumerable<Land> Lands { get; }
        public bool Escalation { get; }
    }
}
