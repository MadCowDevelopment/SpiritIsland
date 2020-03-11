using System;
using System.Collections.Generic;
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
                        Send("CONNECT");
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
            _serialPort.WriteLine(text);
        }

        public event Action<string> CommandReceived;

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (_serialPort.BytesToRead > 0)
            {
                var command = _serialPort.ReadLine();
                command = command.Replace("\r", string.Empty);
                CommandReceived?.Invoke(command);
            }
        }
    }
}