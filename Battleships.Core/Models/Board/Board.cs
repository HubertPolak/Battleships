namespace Battleships.Core.Models.Board;

using Battleships.Core.Models.Ship;
using System.Collections.Generic;

public class Board
{
    public readonly IList<IList<Cell>> Grid;

    private readonly List<(int, int)> _coordinates;

    protected Board(IList<IList<Cell>> grid)
    {
        Grid = grid;

        var columns = Enumerable.Range(0, Constants.Grid.Width - 1);
        var rows = Enumerable.Range(0, Constants.Grid.Height - 1);

        _coordinates = columns.Select(c => rows.Select(r => (c, r))).SelectMany(x => x).ToList();
    }

    public static Board Create(ICollection<Ship> ships)
    {
        var grid = new List<IList<Cell>>();

        for (int i = 0; i < Constants.Grid.Width; ++i)
        {
            grid.Add(new List<Cell>());

            for (int j = 0; j < Constants.Grid.Height; ++j)
            {
                grid[i].Add(Cell.Create());
            }
        }

        var board = new Board(grid);
        board.SetupBoard(ships);

        return board;
    }

    public void Hit(int column, int row)
    {
        Grid[column][row].Hit();
    }

    public bool IsGameOver()
    {
        foreach (var cell in Grid.SelectMany(x => x))
        {
            if (cell.Ship != null && cell.State != CellState.Hit)
            {
                return false;
            }
        }

        return true;
    }

    private void SetupBoard(ICollection<Ship> ships)
    {
        var randomGenerator = new Random();
        var occupiedCoordinates = new List<(int, int)>();

        foreach (var ship in ships)
        {
            var legalCoordinates = new List<(int, int)>(_coordinates);

            legalCoordinates.RemoveAll(c => IsCoordinateOutOfRange(c, ship));
            legalCoordinates.RemoveAll(c => IsCoordinateOccupied(c, occupiedCoordinates));
            legalCoordinates.RemoveAll(c => IsCoordinateOverlappingWithPlacedShip(c, occupiedCoordinates, ship));

            var chosenCoordinate = legalCoordinates[randomGenerator.Next(0, legalCoordinates.Count)];

            occupiedCoordinates.AddRange(PlaceShip(chosenCoordinate, ship));
        }
    }

    protected bool IsCoordinateOutOfRange((int column, int row) coordinate, Ship ship)
    {
        return ship.Direction == Direction.Horizontal ? coordinate.column + ship.Length > Constants.Grid.Width : coordinate.row + ship.Length > Constants.Grid.Height;
    }

    protected bool IsCoordinateOccupied((int, int) coordinate, IList<(int, int)> occupiedCoordinates)
    {
        return occupiedCoordinates.Any(o => o == coordinate);
    }

    protected bool IsCoordinateOverlappingWithPlacedShip((int column, int row) coordinate, IList<(int column, int row)> occupiedCoordinates, Ship ship)
    {
        return ship.Direction == Direction.Horizontal ? occupiedCoordinates.Any(o => o.row == coordinate.row && coordinate.column < o.column && coordinate.column + ship.Length - 1 >= o.column) :
            occupiedCoordinates.Any(o => o.column == coordinate.column && coordinate.row < o.row && coordinate.row + ship.Length - 1 >= o.row);
    }

    protected IList<(int, int)> PlaceShip((int column, int row) coordinate, Ship ship)
    {
        var occupiedCoordinates = new List<(int, int)>();

        for (int i = 0; i < ship.Length; ++i)
        {
            if (ship.Direction == Direction.Horizontal)
            {
                Grid[coordinate.column + i][coordinate.row].PlaceShip(ship);
                occupiedCoordinates.Add((coordinate.column + i, coordinate.row));
                continue;
            }

            Grid[coordinate.column][coordinate.row + i].PlaceShip(ship);
            occupiedCoordinates.Add((coordinate.column, coordinate.row + i));
        }

        return occupiedCoordinates;
    }
}
