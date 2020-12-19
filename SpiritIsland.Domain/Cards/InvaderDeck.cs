using System;
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

        public bool IsEmpty => _cards.Count == 0;

        public InvaderCard Dequeue()
        {
            var card = _cards.Dequeue();
            CardDequeued?.Invoke(new CardDequeuedEventArgs(card, _cards.Count));
            return card;
        }

        public event Action<CardDequeuedEventArgs> CardDequeued;
    }

    public class CardDequeuedEventArgs
    {
        public CardDequeuedEventArgs(InvaderCard card, int remainingCards)
        {
            Card = card;
            RemainingCards = remainingCards;
        }
        
        public InvaderCard Card { get; }
        public int RemainingCards { get; }
    }
}