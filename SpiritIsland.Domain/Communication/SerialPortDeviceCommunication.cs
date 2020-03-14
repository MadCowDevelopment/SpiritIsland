using Serilog;
using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace SpiritIsland.Domain.Communication
{
    public class SerialPortDeviceCommunication : IDeviceCommunication
    {
        private readonly SerialPort _serialPort;
        private readonly ILogger _logger;

        public SerialPortDeviceCommunication(string portName, ILogger logger)
        {
            _logger = logger;
            _serialPort = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
            _serialPort.DataReceived += SerialPortDataReceived;
        }

        public Task Connect()
        {
            return Task.Run(() =>
            {
                bool connected = false;
                _logger.Information("Trying to connect...");
                do
                {
                    try
                    {
                        _logger.Information("Retry connect...");
                        _serialPort.Open();
                        Send("CONNECT");
                        connected = true;
                    }
                    catch
                    {
                        Thread.Sleep(1000);
                    }
                } while (!connected);

                _logger.Information("Connection successful!");
            });
        }

        public void Send(string text)
        {
            _logger.Information($"SND: {text}");
            _serialPort.WriteLine(text);
        }

        public event Action<string> CommandReceived;

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (_serialPort.BytesToRead > 0)
            {
                var command = _serialPort.ReadLine();
                _logger.Information($"RCV: {command}");
                command = command.Replace("\r", string.Empty);
                CommandReceived?.Invoke(command);
            }
        }
    }
}