using System.Linq;
using System.Text;
using SpiritIsland.Domain.Boards;
using SpiritIsland.Domain.Cards;

namespace SpiritIsland.Domain.Communication
{
    public class InvaderCardSender : IInvaderCardSender
    {
        private readonly IDeviceCommunication _deviceCommunication;

        public InvaderCardSender(IDeviceCommunication deviceCommunication)
        {
            _deviceCommunication = deviceCommunication;
        }

        public void Send(Board board, InvaderCard explore, InvaderCard build, InvaderCard ravage)
        {
            var builder = new StringBuilder();
            AppendInvaderCard(board, ravage, builder);
            AppendInvaderCard(board, explore, builder);
            AppendInvaderCard(board, build, builder);
            AppendEscalation(explore, builder);

            var str = builder.ToString();
            _deviceCommunication.Send(str);
        }

        private static void AppendInvaderCard(Board board, InvaderCard invaderCard, StringBuilder builder)
        {
            if (invaderCard == null)
            {
                builder.Append("00000000");
                return;
            }

            for (var i = 0; i < 8; i++)
            {
                var land = board.GetLandTypeById(i);
                builder.Append(invaderCard.Lands.Contains(land) || (i < 3 && invaderCard.Lands.Contains(Land.Coastal)) ? "1" : "0");
            }
        }

        private void AppendEscalation(InvaderCard explore, StringBuilder builder)
        {
            builder.Append((explore != null && explore.Escalation) ? "1" : "0");
        }
    }
}