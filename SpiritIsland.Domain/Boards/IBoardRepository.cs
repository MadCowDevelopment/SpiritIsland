namespace SpiritIsland.Domain.Boards
{
    public interface IBoardRepository
    {
        Board Get(string boardId);
    }
}