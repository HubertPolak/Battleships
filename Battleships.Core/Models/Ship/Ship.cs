namespace Battleships.Core.Models.Ship;

public class Ship
{
    public readonly int Length;
    public readonly Direction Direction;

    public int CellsHit { get; private set; }

    private Ship(int length, Direction direction)
    {
        Length = length;
        Direction = direction;
        CellsHit = 0;
    }

    public static Ship Create(int length, Direction direction)
    {
        if (length <= 0 | length > Constants.Grid.Width || length > Constants.Grid.Height)
        {
            throw new ArgumentException($"Ship's length cannot exceed grid dimensions. Given length: {length}", nameof(length));
        }

        if (direction == default)
        {
            throw new ArgumentException($"Ship's direction needs to be defined. Current direction: {direction}", nameof(direction));
        }
        
        return new Ship(length, direction);
    }

    public void Hit()
    {
        if (Length <= CellsHit) 
        {
            throw new InvalidOperationException($"Ship is already sunk");
        }

        ++CellsHit;
    }

    public bool IsSunk()
    {
        return Length == CellsHit;
    }
}

