namespace Battleships.UI.Services;

public class CoordinateTranslator : ICoordinateTranslator
{
    public (int column, int row) TranslateCoordinates(string coordinates)
    {
        var column = TranslateColumnLetterToNumber(coordinates[0]);
        if (!int.TryParse(coordinates[1..], out var row))
        {
            throw new ArgumentException($"Row number could not be parsed. Coordinates: {coordinates}", nameof(coordinates));
        }

        return (column, row - 1);
    }

    public char TranslateColumnNumberToLetter(int columnNumber)
    {
        if (columnNumber >= Constants.ColumnNames.Length)
        {
            throw new ArgumentOutOfRangeException($"Maximum allowed number of drawable columns is {Constants.ColumnNames.Length}", nameof(columnNumber));
        }

        return Constants.ColumnNames[columnNumber];
    }

    public int TranslateColumnLetterToNumber(char columnLetter)
    {
        return Constants.ColumnNames.IndexOf(columnLetter);
    }
}
