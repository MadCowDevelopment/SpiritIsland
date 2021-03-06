﻿
using System.Text;

namespace SpiritIsland.Domain.Communication
{
    public class DeviceCommandDispatcher : IDeviceCommandDispatcher
    {
        private readonly IGame _game;

        public DeviceCommandDispatcher(IDeviceCommunication deviceCommunication, IGame game)
        {
            _game = game;
            deviceCommunication.CommandReceived += CommandReceived;
        }

        private void CommandReceived(string command)
        {
            if (command.Contains("CMD:START")) _game.Start();
            else if (command.Contains("CMD:EXPLORE")) _game.Explore();
            else if (command.Contains("CMD:ADVANCE")) _game.Advance();            
        }
    }
}
