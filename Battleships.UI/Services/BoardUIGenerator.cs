namespace Battleships.UI.Services;

using Battleships.Core.Models.Board;
using Microsoft.Extensions.Logging;

public class BoardUIGenerator : IBoardUIGenerator
{
    private readonly ILogger<BoardUIGenerator> _logger;

    private readonly ICoordinateTranslator _columnLetterTranslator;

    public BoardUIGenerator(ILogger<BoardUIGenerator> logger,
        ICoordinateTranslator columnLetterTranslator)
    {
        _logger = logger;

        _columnLetterTranslator = columnLetterTranslator;
    }

    public void GenerateBoardUI(IList<IList<Cell>> grid)
    {
        _logger.LogInformation("Generating current board UI.");

        GenerateBoardHeaderUI(grid);
        GenerateBoardRowsUI(grid);

        _logger.LogInformation("Finished generating current board UI.");
    }

    private void GenerateBoardHeaderUI(IList<IList<Cell>> grid)
    {
        _logger.LogInformation("Generating board header UI.");

        Console.Write("\t");
        for (int i = 0; i < grid.Count; ++i)
        {
            Console.Write($"|{_columnLetterTranslator.TranslateColumnNumberToLetter(i)}");
        }
        Console.Write("\n");

        _logger.LogInformation("Finished generating board header UI.");
    }

    private void GenerateBoardRowsUI(IList<IList<Cell>> grid)
    {
        _logger.LogInformation("Generating current board rows UI.");

        for (int i = 0; i < Core.Constants.Grid.Height; ++i)
        {
            _logger.LogInformation("Generating row {row}.", i);

            Console.Write($"\t");
            var rowNumber = i + 1;
            for (int j = 0; j < rowNumber.ToString().Length; ++j)
            {
                Console.Write("\b");
            }
            Console.Write($"{rowNumber}");

            for (int j = 0; j < Core.Constants.Grid.Width; ++j)
            {
                _logger.LogInformation("Generating row {row} column {column}.", i, j);

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
                        throw new ArgumentOutOfRangeException(nameof(grid), $"Cell state of {cellState} is outside of allowed range. Cell column: {j}. Cell row: {i}.");
                }

                Console.ResetColor();

                _logger.LogInformation("Finished generating row {row} column {column}.", i, j);
            }

            Console.Write('\n');

            _logger.LogInformation("Finished generating row {row}.", i);
        }

        _logger.LogInformation("Finished generating current board rows UI.");
    }
}
