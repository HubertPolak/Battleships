namespace Battleships.UI.Services;

using Battleships.Core.Models.Board;

public class BoardUIGenerator : IBoardUIGenerator
{
    private readonly ICoordinateTranslator _columnLetterTranslator;

    public BoardUIGenerator(ICoordinateTranslator columnLetterTranslator)
    {
        _columnLetterTranslator = columnLetterTranslator;
    }

    public void GenerateBoardUI(IList<IList<Cell>> grid)
    {
        GenerateBoardHeaderUI(grid);
        GenerateBoardRowsUI(grid);
    }

    private void GenerateBoardHeaderUI(IList<IList<Cell>> grid)
    {
        Console.Write("\t");
        for (int i = 0; i < grid.Count; ++i)
        {
            Console.Write($"|{_columnLetterTranslator.TranslateColumnNumberToLetter(i)}");
        }
        Console.Write("\n");
    }

    private void GenerateBoardRowsUI(IList<IList<Cell>> grid)
    {
        for (int i = 0; i < Core.Constants.Grid.Height; ++i)
        {
            Console.Write($"\t");
            var rowNumber = i + 1;
            for (int j = 0; j < rowNumber.ToString().Length; ++j)
            {
                Console.Write("\b");
            }
            Console.Write($"{rowNumber}");

            for (int j = 0; j < Core.Constants.Grid.Width; ++j)
            {
                Console.Write('|');

                var cellState = grid[j][i].State;

                switch (cellState)
                {
                    case CellState.Unknown:
                        Console.Write('~');
                        break;
                    case CellState.Miss:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write('o');
                        break;
                    case CellState.Hit:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('x');
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Cell state of {cellState} is outside of allowed range.", nameof(cellState));
                }

                Console.ResetColor();
            }

            Console.Write('\n');
        }
    }
}
