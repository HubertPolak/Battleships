namespace Battleships.Tests.Mocks;

using Battleships.Core.Models.Board;
using Battleships.Core.Models.Ship;

public class BoardMock : Board
{
    public BoardMock(IList<IList<Cell>> grid) 
        : base(grid)
    {
    }

    public bool IsCoordinateOutOfRangeWrapper((int column, int row) coordinate, Ship ship)
    {
        return IsCoordinateOutOfRange(coordinate, ship);
    }

    public bool IsCoordinateOccupiedWrapper((int, int) coordinate, IList<(int, int)> occupiedCoordinates)
    {
        return IsCoordinateOccupied(coordinate, occupiedCoordinates);
    }

    public bool IsCoordinateOverlappingWithPlacedShipWrapper((int column, int row) coordinate, IList<(int column, int row)> occupiedCoordinates, Ship ship)
    {
        return IsCoordinateOverlappingWithPlacedShip(coordinate, occupiedCoordinates, ship);
    }

    public IList<(int, int)> PlaceShipWrapper((int column, int row) coordinate, Ship ship)
    {
        return PlaceShip(coordinate, ship);
    }
}
