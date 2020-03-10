
namespace SpiritIsland.Domain.Communication
{
    public class DeviceCommandDispatcher : IDeviceCommandDispatcher
    {
        private readonly IGame _game;

        public DeviceCommandDispatcher(IDeviceCommunication deviceCommunication, IGame game)
        {
            _game = game;
            deviceCommunication.DataReceived += DeviceCommunication_DataReceived;
        }

        private void DeviceCommunication_DataReceived(byte[] data)
        {
            var command = data.ToString();
            if (command == "EXPLORE") _game.Explore();
            else if (command == "ADVANCE") _game.Advance();
        }
    }
}
