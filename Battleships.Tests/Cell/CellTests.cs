namespace Battleships.Tests.Cell;

using Battleships.Core.Models.Board;
using Battleships.Core.Models.Ship;

public class CellTests
{
    [Test]
    public void WhenCellCreated_StateIsUnknown()
    {
        var cell = Cell.Create();

        Assert.That(cell.State, Is.EqualTo(CellState.Unknown));
    }

    [Test]
    public void WhenCellHit_WithoutShip_StateIsMiss()
    {
        var cell = Cell.Create();
        cell.Hit();

        Assert.That(cell.State, Is.EqualTo(CellState.Miss));
    }

    [Test]
    public void WhenCellHit_WithShip_StateIsHit()
    {
        var cell = Cell.Create();
        cell.PlaceShip(Ship.Create(5, Direction.Horizontal));
        cell.Hit();

        Assert.That(cell.State, Is.EqualTo(CellState.Hit));
    }
}
