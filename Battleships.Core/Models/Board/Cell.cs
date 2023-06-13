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

    public void Hit()
    {
        if (Ship == null)
        {
            State = CellState.Miss;
            return;
        }

        if (State != CellState.Unknown)
        {
            return;
        }

        Ship.Hit();
        State = CellState.Hit;
    }

    public void PlaceShip(Ship ship)
    {
        Ship = ship;
    }
}

