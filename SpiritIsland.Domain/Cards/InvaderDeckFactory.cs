using System.Collections.Generic;
using System.Linq;
using SpiritIsland.Domain.Utils;

namespace SpiritIsland.Domain.Cards
{
    public class InvaderDeckFactory : IInvaderDeckFactory
    {
        private readonly List<InvaderCard> Stage1Cards;
        private readonly List<InvaderCard> Stage2Cards;
        private readonly List<InvaderCard> Stage3Cards;
        
        public InvaderDeckFactory()
        {
            Stage1Cards = new List<InvaderCard>
            {
                new InvaderCard(1, new []{ Land.Jungle }, false),
                new InvaderCard(1, new []{ Land.Mountain }, false),
                new InvaderCard(1, new []{ Land.Sand }, false),
                new InvaderCard(1, new []{ Land.Wetland }, false)
            };

            Stage2Cards = new List<InvaderCard>
            {
                new InvaderCard(2, new []{ Land.Jungle }, true),
                new InvaderCard(2, new []{ Land.Mountain }, true),
                new InvaderCard(2, new []{ Land.Sand }, true),
                new InvaderCard(2, new []{ Land.Wetland }, true),
                new InvaderCard(2, new []{ Land.Coastal }, false)
            };

            Stage3Cards = new List<InvaderCard>
            {
                new InvaderCard(3, new []{ Land.Jungle, Land.Sand }, false),
                new InvaderCard(3, new []{ Land.Jungle, Land.Wetland }, false),
                new InvaderCard(3, new []{ Land.Mountain, Land.Jungle }, false),
                new InvaderCard(3, new []{ Land.Mountain, Land.Wetland }, false),
                new InvaderCard(3, new []{ Land.Sand, Land.Mountain }, false),
                new InvaderCard(3, new []{ Land.Sand, Land.Wetland }, false)
            };
        }

        public InvaderDeck Create(string order)
        {
            var numbers = order.ToCharArray().Select(p => int.Parse(p.ToString())).ToList();

            Stage1Cards.Shuffle();
            Stage2Cards.Shuffle();
            Stage3Cards.Shuffle();

            var stage1 = new Queue<InvaderCard>(Stage1Cards);
            var stage2 = new Queue<InvaderCard>(Stage2Cards);
            var stage3 = new Queue<InvaderCard>(Stage3Cards);
            var invaderQueues = new List<Queue<InvaderCard>> { stage1, stage2, stage3 };

            var invaderCards = new List<InvaderCard>();
            foreach (var number in numbers)
            {
                invaderCards.Add(invaderQueues[number - 1].Dequeue());
            }

            return new InvaderDeck(invaderCards);
        }
    }
}