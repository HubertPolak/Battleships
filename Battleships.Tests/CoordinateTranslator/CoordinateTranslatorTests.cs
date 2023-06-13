namespace Battleships.Tests.CoordinateTranslator;

using Microsoft.Extensions.Logging;
using Moq;
using Battleships.UI.Services;

public class CoordinateTranslatorTests
{
    private readonly Mock<ILogger<CoordinateTranslator>> _logger;

    public CoordinateTranslatorTests()
    {
        _logger = new Mock<ILogger<CoordinateTranslator>>();
    }

    [Test]
    public void WhenTranslatingColumnNumberToLetter_WithColumnNumberOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        var coordinateTranslator = new CoordinateTranslator(_logger.Object);

        Assert.Throws<ArgumentOutOfRangeException>(() => coordinateTranslator.TranslateColumnNumberToLetter(10));
    }

    [Test]
    public void WhenTranslatingColumnNumberToLetter_WithColumnNumberInRange_ReturnsColumnLetter()
    {
        var coordinateTranslator = new CoordinateTranslator(_logger.Object);

        Assert.That(coordinateTranslator.TranslateColumnNumberToLetter(0), Is.EqualTo('A'));
    }

    [Test]
    public void WhenTranslatingCoordinates_WithRowNumberMalformed_ThrowsArgumentException()
    {
        var coordinateTranslator = new CoordinateTranslator(_logger.Object);

        Assert.Throws<ArgumentException>(() => coordinateTranslator.TranslateCoordinates("A1-"));
    }

    [Test]
    public void WhenTranslatingCoordinates_WithColumnLetterOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        var coordinateTranslator = new CoordinateTranslator(_logger.Object);

        Assert.Throws<ArgumentOutOfRangeException>(() => coordinateTranslator.TranslateCoordinates("Z1"));
    }

    [Test]
    public void WhenTranslatingCoordinates_WithCorrectCoordinates_ReturnsCoordinates()
    {
        var coordinateTranslator = new CoordinateTranslator(_logger.Object);

        Assert.That(coordinateTranslator.TranslateCoordinates("A1"), Is.EqualTo((0, 0)));
    }
}
