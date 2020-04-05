namespace SpiritIsland.Domain.Adversaries
{
    internal interface IBeforeInitialExplore
    {
        void Handle(Game game);
    }
}