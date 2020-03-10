using System;
using System.Threading;
using System.Threading.Tasks;
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

        static void Main(string[] args)
        {
            PrintIntro();

            var portName = args.Length == 1 ? args[0] : "COM3";
            GameLoop(portName);
        }

        private static void GameLoop(string portName)
        {
            Task.Run(async () =>
            {
                var boardRepository = new InMemoryBoardRepository();
                var deviceCommunication = new SerialPortDeviceCommunication(portName);
                await deviceCommunication.Connect();
                var invaderCardSender = new InvaderCardSender(deviceCommunication);

                Console.WriteLine("Waiting for the game to start...");

                var game = new Game(boardRepository, invaderCardSender, new InvaderDeckFactory());
                game.GameStarted += () =>
                {
                    _startEvent.Set();
                    Console.Clear();
                };

                game.GameLost += () => _resetEvent.Set();

                new DeviceCommandDispatcher(deviceCommunication, game);

                await game.Initialize(new GameSettings(new []{"C"}));
            });

            _startEvent.Wait();
            _resetEvent.Wait();
        }

        private static void PrintIntro()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Spirit Island!");
            Console.WriteLine("-------------------------");
            Console.WriteLine();           
        }
    }
}
