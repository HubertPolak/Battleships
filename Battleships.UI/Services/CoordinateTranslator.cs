namespace Battleships.UI.Services;

using Microsoft.Extensions.Logging;

public class CoordinateTranslator : ICoordinateTranslator
{
    private readonly ILogger<CoordinateTranslator> _logger;

    public CoordinateTranslator(ILogger<CoordinateTranslator> logger)
    {
        _logger = logger;
    }

    public (int column, int row) TranslateCoordinates(string coordinates)
    {
        _logger.LogInformation("Translating UI coordinates {coordinates} into grid indexes.", coordinates);

        var column = TranslateColumnLetterToNumber(coordinates[0]);
        if (!int.TryParse(coordinates[1..], out var row))
        {
            _logger.LogError("Error occured while parsing UI row coordinates into a grid index. Coordinates {coordinates}.", coordinates);

            throw new ArgumentException($"Row number could not be parsed. Coordinates: {coordinates}", nameof(coordinates));
        }

        var gridIndexes = (column, row - 1);

        _logger.LogInformation("Returning grid indexes {gridIndexes}", gridIndexes);

        return gridIndexes;
    }

    public char TranslateColumnNumberToLetter(int columnNumber)
    {
        _logger.LogInformation("Translating column number {columnNumber} into column letter.", columnNumber);

        if (columnNumber >= Constants.ColumnLetters.Length)
        {
            _logger.LogError("Error occured while translating column number into column letter. Column number {columnNumber}", columnNumber);

            throw new ArgumentOutOfRangeException(nameof(columnNumber), $"Maximum allowed number of drawable columns is {Constants.ColumnLetters.Length}");
        }

        var columnLetter = Constants.ColumnLetters[columnNumber];

        _logger.LogInformation("Returning column letter {columnLetter}", columnLetter);

        return columnLetter;
    }

    private int TranslateColumnLetterToNumber(char columnLetter)
    {
        _logger.LogInformation("Translating column letter {columnLetter} into column number.", columnLetter);

        if (!Constants.ColumnLetters.Contains(columnLetter)) 
        {
            _logger.LogError("Error occured while translating column letter into column number. Column letter {columnLetter}", columnLetter);

            throw new ArgumentOutOfRangeException(nameof(columnLetter), $"Column letter {columnLetter} is not supported.");
        }

        var columnNumber = Constants.ColumnLetters.IndexOf(columnLetter);

        _logger.LogInformation("Returning column number {columnNumber}", columnNumber);

        return columnNumber;
    }
}
