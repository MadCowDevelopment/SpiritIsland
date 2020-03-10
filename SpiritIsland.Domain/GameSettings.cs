using System.Collections.Generic;
using System.Linq;

namespace SpiritIsland.Domain
{
    public class GameSettings
    {
        public GameSettings(IEnumerable<string> boardIds)
        {
            BoardIds = boardIds.ToList();
        }

        public IEnumerable<string> BoardIds { get; }
    }
}