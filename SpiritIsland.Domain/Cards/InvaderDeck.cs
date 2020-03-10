using System.Collections.Generic;

namespace SpiritIsland.Domain.Cards
{
    public class InvaderDeck
    {
        private readonly Queue<InvaderCard> _cards;

        public InvaderDeck(IEnumerable<InvaderCard> cards)
        {
            _cards = new Queue<InvaderCard>(cards);
        }

        public bool IsEmpty => _cards.Count > 0;

        public InvaderCard Dequeue()
        {
            return _cards.Dequeue();
        }
    }
}