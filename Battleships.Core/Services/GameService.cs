namespace Battleships.App.Services;

using Battleships.Core.Models.Board;
using Battleships.Core.Models.Ship;

public class GameService : IGameService
{
    private readonly Board _board;

    public GameService()
    {
        var directions = Enum.GetValues(typeof(Direction)).Cast<Direction>().ToList();
        var random = new Random();

        _board = Board.Create(new List<Ship>()
        {
            Ship.Create(5, directions[random.Next(1, directions.Count)]),
            Ship.Create(4, directions[random.Next(1, directions.Count)]),
            Ship.Create(4, directions[random.Next(1, directions.Count)])
        });
    }

    public IList<IList<Cell>> GetGrid()
    {
        return _board.Grid;
    }

    public Cell GetCell(int column, int row)
    {
        return _board.Grid[column][row];
    }

    public bool ResolveHitAction(int column, int row)
    {
        return _board.Hit(column, row);
    }

    public bool IsGameOver()
    {
        return _board.IsGameOver();
    }
}
