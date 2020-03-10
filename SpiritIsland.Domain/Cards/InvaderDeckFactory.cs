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

        public InvaderDeck Create()
        {
            Stage1Cards.Shuffle();
            Stage2Cards.Shuffle();
            Stage3Cards.Shuffle();

            var invaderCards = new List<InvaderCard>();
            invaderCards.AddRange(Stage1Cards.Take(3));
            invaderCards.AddRange(Stage2Cards.Take(4));
            invaderCards.AddRange(Stage3Cards.Take(5));
            return new InvaderDeck(invaderCards);
        }
    }
}