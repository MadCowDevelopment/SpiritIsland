using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        
        private readonly List<Board> _boards = new List<Board>();
        private InvaderDeck _invaderDeck;
        private InvaderCard _currentExplore;
        private InvaderCard _currentBuild;
        private InvaderCard _currentRavage;
        private bool _isRunning;

        public Game(
            IBoardRepository boardRepository,
            IInvaderCardSender invaderCardSender,
            IInvaderDeckFactory invaderDeckFactory)
        {
            _boardRepository = boardRepository;
            _invaderCardSender = invaderCardSender;
            _invaderDeckFactory = invaderDeckFactory;
        }

        public void Start()
        {
            if(_isRunning)
            {
                return;
            }

            _isRunning = true;
            GameStarted?.Invoke();
            Explore();
        }

        public void Explore()
        {
            if (_invaderDeck.IsEmpty)
            {
                SendGameEndData();               
                GameLost?.Invoke();
                return;
            }

            _currentExplore = _invaderDeck.Dequeue();
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

                _invaderDeck = _invaderDeckFactory.Create();
            });
        }
    }
}
