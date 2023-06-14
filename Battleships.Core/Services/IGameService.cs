namespace Battleships.App.Services;

using Battleships.Core.Models.Board;

public interface IGameService
{
    public IList<IList<Cell>> GetGrid();

    public Cell GetCell(int column, int row);

    public void ResolveHitAction(int column, int row);

    public bool IsGameOver();
}
