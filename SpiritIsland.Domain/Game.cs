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
            Explore();
        }

        public void Explore()
        {
            if (_invaderDeck.IsEmpty) GameLost?.Invoke();
            _currentExplore = _invaderDeck.Dequeue();
            SendCardData();
        }

        public void Advance()
        {
            _currentRavage = _currentBuild;
            _currentBuild = _currentExplore;
            _currentExplore = null;
            SendCardData();
        }

        public event Action GameLost;

        private void SendCardData()
        {
            foreach (var board in _boards)
            {
                _invaderCardSender.Send(board, _currentExplore, _currentBuild, _currentRavage);
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
