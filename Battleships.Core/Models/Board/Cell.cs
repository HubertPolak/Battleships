namespace Battleships.Core.Models.Board;

using Battleships.Core.Models.Ship;

public class Cell
{
    public Ship? Ship { get; private set; }
    public CellState State { get; private set; }

    private Cell(CellState state)
    {
        State = state;
    }

    public static Cell Create()
    {
        return new Cell(CellState.Unknown);
    }

    public bool Hit()
    {
        if (Ship == null)
        {
            State = CellState.Miss;
            return false;
        }

        if (State != CellState.Unknown)
        {
            return false;
        }

        Ship.Hit();
        State = CellState.Hit;

        return Ship.IsSunk();
    }

    public void PlaceShip(Ship ship)
    {
        Ship = ship;
    }
}

