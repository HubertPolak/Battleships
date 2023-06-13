namespace Battleships.App.Services;

using Battleships.Core.Models.Board;
using Battleships.Core.Models.Ship;
using Microsoft.Extensions.Logging;

public class GameService : IGameService
{
    private readonly ILogger<GameService> _logger;

    private readonly Board _board;

    public GameService(ILogger<GameService> logger)
    {
        _logger = logger;

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
        _logger.LogInformation("Returning grid.");
        return _board.Grid;
    }

    public Cell GetCell(int column, int row)
    {
        _logger.LogInformation("Returning cell in column {column} row {row}.", column, row);
        return _board.Grid[column][row];
    }

    public void ResolveHitAction(int column, int row)
    {
        _logger.LogInformation("Resolving hit action for cell in column {column} row {row}.", column, row);
        _board.Hit(column, row);
    }

    public bool IsGameOver()
    {
        _logger.LogInformation("Checking whether game is over.");

        var isGameOver = _board.IsGameOver();

        _logger.LogInformation("Returning current state. Game over: {isGameOver}", isGameOver);

        return isGameOver;
    }
}
