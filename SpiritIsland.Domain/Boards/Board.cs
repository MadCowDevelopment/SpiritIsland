using System.Collections.Generic;
using System.Linq;
using SpiritIsland.Domain.Cards;

namespace SpiritIsland.Domain.Boards
{
    public class Board
    {
        private readonly IReadOnlyList<Land> _lands;

        public Board(IEnumerable<Land> lands)
        {
            _lands = lands.ToList();
        }

        public Land GetLandTypeById(int i)
        {
            return _lands[i];
        }
    }
}