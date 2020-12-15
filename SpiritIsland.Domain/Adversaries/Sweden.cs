using System.Linq;

namespace SpiritIsland.Domain.Adversaries
{
    public class Sweden : Adversary, IBeforeInitialExplore
    {
        public override string DisplayName { get; } = "The Kingdom of Sweden";

        public void Handle(Game game)
        {
            var nextCard = game.InvaderDeck.Dequeue();
            ShowMessageRequested?.Invoke($"Royal Backing: On each board, before explore add 1 town to the {nextCard.Lands.First()} with the fewest invaders.");
        }
    }
}
