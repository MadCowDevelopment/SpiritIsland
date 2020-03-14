using System;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using SpiritIsland.Domain;
using SpiritIsland.Domain.Boards;
using SpiritIsland.Domain.Cards;
using SpiritIsland.Domain.Communication;

namespace SpiritIsland.CLI
{
    class Program
    {
        private static readonly ManualResetEventSlim _startEvent = new ManualResetEventSlim();

        private static readonly ManualResetEventSlim _resetEvent = new ManualResetEventSlim();

        private static ILogger _logger;

        static void Main(string[] args)
        {
            _logger = CreateLogger();
            PrintIntro();

            var portName = args.Length == 1 ? args[0] : "COM3";
            GameLoop(portName);
        }

        private static void GameLoop(string portName)
        {
            Task.Run(async () =>
            {
                
                var boardRepository = new InMemoryBoardRepository();
                var deviceCommunication = new SerialPortDeviceCommunication(portName, _logger);
                await deviceCommunication.Connect();
                var invaderCardSender = new InvaderCardSender(deviceCommunication);

                _logger.Information("Waiting for the game to start...");

                var game = new Game(boardRepository, invaderCardSender, new InvaderDeckFactory());
                game.GameStarted += () =>
                {
                    _startEvent.Set();
                    Console.Clear();
                };

                game.GameLost += () => _resetEvent.Set();

                new DeviceCommandDispatcher(deviceCommunication, game);

                await game.Initialize(new GameSettings(new[] { "C" }));
            });

            _startEvent.Wait();
            _resetEvent.Wait();
        }

        private static ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        private static void PrintIntro()
        {
            _logger.Information("Welcome to Spirit Island!");
            _logger.Information("-------------------------");
        }
    }
}
