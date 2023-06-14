namespace Battleships.UI.Services;

public interface ICoordinateTranslator
{
    public (int column, int row) TranslateCoordinates(string coordinates);

    public char TranslateColumnNumberToLetter(int columnNumber);
}
