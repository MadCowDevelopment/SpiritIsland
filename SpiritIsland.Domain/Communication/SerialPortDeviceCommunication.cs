using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace SpiritIsland.Domain.Communication
{
    public class SerialPortDeviceCommunication : IDeviceCommunication
    {
        private readonly SerialPort _serialPort;

        public SerialPortDeviceCommunication(string portName)
        {
            _serialPort = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
            _serialPort.DataReceived += SerialPortDataReceived;
        }

        public Task Connect()
        {
            return Task.Run(() =>
            {
                bool connected = false;
                Console.Write("Trying to connect");
                do
                {
                    try
                    {
                        Console.Write(".");
                        _serialPort.Open();
                        _serialPort.Write("CONNECT");
                        connected = true;
                    }
                    catch
                    {
                        Thread.Sleep(1000);
                    }
                } while (!connected);

                Console.WriteLine();
                Console.WriteLine("Connection successful!");
            });
        }

        public void Send(string text)
        {
            _serialPort.Write(text);
        }

        public event Action<byte[]> DataReceived;

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);
            var dataLength = _serialPort.BytesToRead;
            var data = new byte[dataLength];
            var nbrDataRead = _serialPort.Read(data, 0, dataLength);
            if (nbrDataRead == 0) return;

            DataReceived?.Invoke(data);
        }
    }
}