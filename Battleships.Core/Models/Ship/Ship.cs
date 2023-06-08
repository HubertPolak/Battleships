namespace Battleships.Core.Models.Ship;

public class Ship
{
    public int Length { get; }

    private Ship(int length)
    {
        Length = length;
    }

    public static Ship Create(int length)
    {
        return new Ship(length);
    }
}

