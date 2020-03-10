using System.Collections.Generic;
using SpiritIsland.Domain.Cards;

namespace SpiritIsland.Domain.Boards
{
    public class InMemoryBoardRepository : IBoardRepository
    {
        readonly Dictionary<string, Board> _boards = new Dictionary<string, Board>();

        public InMemoryBoardRepository()
        {
            _boards.Add("C",
                new Board(new[]
                {
                    Land.Jungle, Land.Sand, Land.Mountain, Land.Jungle, Land.Wetland, Land.Sand, Land.Mountain, Land.Wetland
                }));
        }

        public Board Get(string boardId)
        {
            return _boards[boardId];
        }
    }
}