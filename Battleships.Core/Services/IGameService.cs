using Battleships.Core.Models.Board;

namespace Battleships.App.Services;

public interface IGameService
{
    public IList<IList<Cell>> GetGrid();

    public Cell GetCell(int column, int row);

    public bool ResolveHitAction(int column, int row);

    public bool IsGameOver();
}
