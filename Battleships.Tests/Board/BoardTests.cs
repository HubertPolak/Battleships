namespace Battleships.Tests.Board;

using Battleships.Core;
using Battleships.Core.Models.Board;
using Battleships.Core.Models.Ship;
using Battleships.Tests.Mocks;

public class BoardTests
{
    [Test]
    public void WhenBoardCreated_WithShips_ShipsArePlaced()
    {
        var ships = new List<Ship>
        {
            Ship.Create(5, Direction.Horizontal),
            Ship.Create(4, Direction.Vertical)
        };

        var board = Board.Create(ships);

        var distinctShipsPlaced = board.Grid.SelectMany(x => x).Where(c => c.Ship != null).Select(c => c.Ship).Distinct();

        Assert.That(distinctShipsPlaced.Count(), Is.EqualTo(2));
    }

    [Test]
    public void WhenCheckingIfSpaceIsOutOfRange_CoordinateProvidedIsOutOfRange_ReturnsTrue()
    {
        var board = new BoardMock(GetGrid());

        var ship = Ship.Create(4, Direction.Horizontal);

        Assert.That(board.IsCoordinateOutOfRangeWrapper((7, 0), ship), Is.True);
    }

    [Test]
    public void WhenCheckingIfSpaceIsOutOfRange_CoordinateProvidedIsNotOutOfRange_ReturnsFalse()
    {
        var board = new BoardMock(GetGrid());

        var ship = Ship.Create(4, Direction.Horizontal);

        Assert.That(board.IsCoordinateOutOfRangeWrapper((6, 0), ship), Is.False);
    }

    [Test]
    public void WhenCheckingIfSpaceIsOccupied_CoordinateProvidedIsOccupied_ReturnsTrue()
    {
        var board = new BoardMock(GetGrid());

        Assert.That(board.IsCoordinateOccupiedWrapper((0, 0), new List<(int, int)> { (0, 0), (0, 1) }), Is.True);
    }

    [Test]
    public void WhenCheckingIfSpaceIsOccupied_CoordinateProvidedIsNotOccupied_ReturnsFalse()
    {
        var board = new BoardMock(GetGrid());

        Assert.That(board.IsCoordinateOccupiedWrapper((0, 0), new List<(int, int)> { (0, 1), (0, 2) }), Is.False);
    }

    [Test]
    public void WhenCheckingIfSpaceIsOverlappingWithPlacedShips_CoordinateProvidedIsOverlapping_ReturnsTrue()
    {
        var board = new BoardMock(GetGrid());

        var ship = Ship.Create(4, Direction.Horizontal);

        Assert.That(board.IsCoordinateOverlappingWithPlacedShipWrapper((0, 0), new List<(int, int)> { (3, 0), (3, 1) }, ship), Is.True);
    }

    [Test]
    public void WhenCheckingIfSpaceIsOverlappingWithPlacedShips_CoordinateProvidedIsNotOverlapping_ReturnsFalse()
    {
        var board = new BoardMock(GetGrid());

        var ship = Ship.Create(4, Direction.Horizontal);

        Assert.That(board.IsCoordinateOverlappingWithPlacedShipWrapper((0, 0), new List<(int, int)> { (4, 0), (4, 1) }, ship), Is.False);
    }

    [Test]
    public void WhenPlacingShip_PlacesShipInCorrectCoordinates()
    {
        var board = new BoardMock(GetGrid());

        var ship = Ship.Create(4, Direction.Horizontal);

        board.PlaceShipWrapper((0, 0), ship);

        for (int i = 0; i < ship.Length; ++i)
        {
            Assert.That(board.Grid[i][0].Ship, Is.EqualTo(ship));
        }
    }

    [Test]
    public void WhenPlacingShip_GivenCoordinateIsAtTheEdge_PlacesShipInCorrectCoordinates()
    {
        var board = new BoardMock(GetGrid());

        var ship = Ship.Create(4, Direction.Horizontal);

        (int column, int row) startingCoordinate = (6, 9);
        board.PlaceShipWrapper(startingCoordinate, ship);
        
        for (int i = 0; i < ship.Length; ++i)
        {
            Assert.That(board.Grid[startingCoordinate.column + i][startingCoordinate.row].Ship, Is.EqualTo(ship));
        }
    }

    private IList<IList<Cell>> GetGrid()
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

        return grid;
    }
}
