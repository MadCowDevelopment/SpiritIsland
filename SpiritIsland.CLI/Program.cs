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
        private static readonly ManualResetEventSlim _resetEvent = new ManualResetEventSlim();

        static void Main(string[] args)
        {
            PrintIntro();

            var portName = args.Length == 1 ? args[0] : "COM1";
            GameLoop(portName);
        }

        private static void GameLoop(string portName)
        {
            Task.Run(async () =>
            {
                var boardRepository = new InMemoryBoardRepository();
                var deviceCommunication = new SerialPortDeviceCommunication(portName);
                var invaderCardSender = new InvaderCardSender(deviceCommunication);
                
                var game = new Game(boardRepository, invaderCardSender, new InvaderDeckFactory());
                game.GameLost += () => _resetEvent.Set();

                new DeviceCommandDispatcher(deviceCommunication, game);

                await game.Initialize(new GameSettings(new []{"C"}));
                game.Start();
            });

            _resetEvent.Wait();
        }

        private static void PrintIntro()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Spirit Island!");
            Console.WriteLine("-------------------------");
            Console.WriteLine();
            Console.WriteLine("Press any key to start a new game.");
            Console.ReadKey(true);
            Console.Clear();
        }
    }
}
