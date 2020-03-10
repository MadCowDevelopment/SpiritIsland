using System;
using System.IO.Ports;

namespace SpiritIsland.Domain.Communication
{
    public class SerialPortDeviceCommunication : IDeviceCommunication
    {
        private readonly SerialPort _serialPort;

        public SerialPortDeviceCommunication(string portName)
        {
            _serialPort = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
            _serialPort.DataReceived += SerialPortDataReceived;
            _serialPort.Open();
        }

        public void Send(string text)
        {
            _serialPort.WriteLine(text);
        }

        public event Action<byte[]> DataReceived;

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var dataLength = _serialPort.BytesToRead;
            var data = new byte[dataLength];
            var nbrDataRead = _serialPort.Read(data, 0, dataLength);
            if (nbrDataRead == 0) return;

            DataReceived?.Invoke(data);
        }
    }
}