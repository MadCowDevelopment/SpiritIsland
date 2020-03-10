using System;

namespace SpiritIsland.Domain.Communication
{
    public interface IDeviceCommunication
    {
        void Send(string text);

        event Action<byte[]> DataReceived;
    }
}