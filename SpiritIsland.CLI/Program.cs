using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using SpiritIsland.Domain;
using SpiritIsland.Domain.Adversaries;
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
        private static Game game;
        private static Adversary _adversary;

        static void Main(string[] args)
        {
            _logger = CreateLogger();
            PrintIntro();
            SelectAdversary();

            var portName = args.Length == 1 ? args[0] : "COM3";
            GameLoop(portName);
        }

        private static void SelectAdversary()
        {
            var adversaryStore = new AdversaryStore();
            var adversaries = adversaryStore.Adversaries;
            _logger.Information("Choose adverary: ");
            _logger.Information("0 - None (Default)");
            for (int i = 0; i < adversaries.Count; i++)
            {
                _logger.Information($"{i + 1} - {adversaries[i].DisplayName}");
            }

            var line = Console.ReadLine();
            if (int.TryParse(line, out int selection) && selection > 0 && selection <= adversaries.Count)
            {
                InitializeAdversary(adversaries, selection);
            }
        }

        private static void InitializeAdversary(ReadOnlyCollection<Adversary> adversaries, int selection)
        {
            string line;
            _adversary = adversaries[selection - 1];
            _logger.Information($"Selected adversary: {_adversary.DisplayName}");
            _logger.Warning("Select level (1-6):");
            line = Console.ReadLine();
            if (int.TryParse(line, out int level) && level >= 1 && level <= 6) _adversary.Initialize(level);
            else _adversary.Initialize(1);
            _adversary.ShowMessageRequested += p => _logger.Warning(p);
        }

        private static void GameLoop(string portName)
        {
            StartGameLoop(portName);
            StartUserInputLoop();

            _startEvent.Wait();
            _resetEvent.Wait();
        }

        private static void StartGameLoop(string portName)
        {
            Task.Run(async () =>
            {

                var boardRepository = new InMemoryBoardRepository();
                var deviceCommunication = new SerialPortDeviceCommunication(portName, _logger);
                await deviceCommunication.Connect();
                var invaderCardSender = new InvaderCardSender(deviceCommunication);

                _logger.Information("Waiting for the game to start...");

                game = new Game(boardRepository, invaderCardSender, new InvaderDeckFactory(), _adversary);
                game.GameStarted += () =>
                {
                    _startEvent.Set();
                    Console.Clear();
                };

                game.GameLost += () => _resetEvent.Set();

                new DeviceCommandDispatcher(deviceCommunication, game);

                await game.Initialize(new GameSettings(new[] { "C" }));
            });
        }

        private static void StartUserInputLoop()
        {
            Task.Run(() =>
            {
                while (!_resetEvent.IsSet)
                {
                    var input = Console.ReadLine();
                    switch (input.ToUpper())
                    {
                        case "EXPLORE":
                        case "E":
                            game.Explore();
                            break;
                        case "ADVANCE":
                        case "A":
                            game.Advance();
                            break;
                        case "HELP":
                        case "H":
                            PrintHelp();
                            break;
                        default:
                            _logger.Warning($"Command '{input}' is not valid.)");
                            PrintHelp();
                            break;
                    }
                }
            });
        }

        private static void PrintHelp()
        {
            _logger.Information("Supported commands:");
            _logger.Information("-------------------");
            _logger.Information("EXPLORE (E) - Explore the next card");
            _logger.Information("ADVANCE (A) - Advance the invader deck");
            _logger.Information("HELP    (H) - Show this help.");
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
