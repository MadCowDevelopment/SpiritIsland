using SpiritIsland.Domain.Boards;
using SpiritIsland.Domain.Cards;

namespace SpiritIsland.Domain.Communication
{
    public interface IInvaderCardSender
    {
        void Send(Board board, InvaderCard explore, InvaderCard build, InvaderCard ravage);
    }
}