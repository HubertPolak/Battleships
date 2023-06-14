namespace Battleships.Tests.Ship;

using Battleships.Core.Models.Ship;

public class ShipTests
{
    [Test]
    public void WhenShipCreated_WithZeroLength_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Ship.Create(0, Direction.Horizontal));
    }

    [Test]
    public void WhenShipCreated_WithLessThanZeroLength_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Ship.Create(-1, Direction.Horizontal));
    }

    [Test]
    public void WhenShipCreated_WithInvalidDirection_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Ship.Create(5, Direction.Invalid));
    }

    [Test]
    public void WhenShipCreated_WithValidParameters_ReturnsValidShip()
    {
        var ship = Ship.Create(5, Direction.Horizontal);

        Assert.That(ship.Length, Is.EqualTo(5));
        Assert.That(ship.Direction, Is.EqualTo(Direction.Horizontal));
    }

    [Test]
    public void WhenShipIsHit_AfterBeingSunk_ThrowsInvalidOperationException()
    {
        var ship = Ship.Create(1, Direction.Horizontal);
        ship.Hit();

        Assert.Throws<InvalidOperationException>(() => ship.Hit());
    }

    [Test]
    public void WhenShipIsHit_AndNumberOfHitsEqualsLength_ShipIsSunk()
    {
        var ship = Ship.Create(1, Direction.Horizontal);
        ship.Hit();

        Assert.That(ship.IsSunk(), Is.True);
    }

    [Test]
    public void WhenShipIsHit_AndNumberOfHitsIsLessThanLength_ShipIsNotSunk()
    {
        var ship = Ship.Create(2, Direction.Horizontal);
        ship.Hit();

        Assert.That(ship.IsSunk(), Is.False);
    }
}
