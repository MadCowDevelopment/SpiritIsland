using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SpiritIsland.Domain.Adversaries;
using SpiritIsland.Domain.Boards;
using SpiritIsland.Domain.Cards;
using SpiritIsland.Domain.Communication;

namespace SpiritIsland.Domain
{
    public class Game : IGame
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IInvaderCardSender _invaderCardSender;
        private readonly IInvaderDeckFactory _invaderDeckFactory;
        private readonly Adversary _adversary;
        private readonly string _invaderDeckOrder;
        private readonly List<Board> _boards = new List<Board>();
        private InvaderCard _currentExplore;
        private InvaderCard _currentBuild;
        private InvaderCard _currentRavage;
        private bool _isRunning;

        public Game(
            IBoardRepository boardRepository,
            IInvaderCardSender invaderCardSender,
            IInvaderDeckFactory invaderDeckFactory,
            Adversary adversary)
        {
            _boardRepository = boardRepository;
            _invaderCardSender = invaderCardSender;
            _invaderDeckFactory = invaderDeckFactory;
            _adversary = adversary;

            _invaderDeckOrder = (_adversary as IAffectsInvaderDeckOrder)?.InvaderDeckOrder ?? "111222233333";
        }

        public InvaderDeck InvaderDeck { get; private set; }

        public void Start()
        {
            if(_isRunning)
            {
                return;
            }

            _isRunning = true;
            GameStarted?.Invoke();
            if (_adversary is IBeforeInitialExplore beforeInitialExplore) beforeInitialExplore.Handle(this);
            Explore();
        }

        public void Explore()
        {
            if (InvaderDeck.IsEmpty)
            {
                SendGameEndData();               
                GameLost?.Invoke();
                return;
            }

            _currentExplore = InvaderDeck.Dequeue();
            SendExploreData();
        }

        public void Advance()
        {
            _currentRavage = _currentBuild;
            _currentBuild = _currentExplore;
            _currentExplore = null;
            SendAdvanceData();
        }

        public event Action GameStarted;
        public event Action GameLost;
        
        private void SendExploreData()
        {
            foreach (var board in _boards)
            {
                _invaderCardSender.Send(board, _currentExplore, null, null);
            }
        }

        private void SendAdvanceData()
        {
            foreach (var board in _boards)
            {
                _invaderCardSender.Send(board, null, _currentBuild, _currentRavage);
            }
        }

        private void SendGameEndData()
        {
            foreach (var board in _boards)
            {
                _invaderCardSender.Send(board, null, null, null);
            }
        }

        public Task Initialize(GameSettings settings)
        {
            return Task.Run(() =>
            {
                foreach (var boardId in settings.BoardIds)
                {
                    _boards.Add(_boardRepository.Get(boardId));
                }

                InvaderDeck = _invaderDeckFactory.Create(_invaderDeckOrder);
            });
        }
    }
}
