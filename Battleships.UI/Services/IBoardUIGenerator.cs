namespace Battleships.UI.Services;

using Battleships.Core.Models.Board;

public interface IBoardUIGenerator
{
    public void GenerateBoardUI(IList<IList<Cell>> grid);
}
