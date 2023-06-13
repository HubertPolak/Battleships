namespace Battleships.UI.Services;

using Battleships.App.Services;
using Battleships.Core.Models.Board;
using Microsoft.Extensions.Logging;

public class UIGenerator : IUIGenerator
{
    private readonly ILogger<UIGenerator> _logger;

    private readonly IGameService _gameService;

    private readonly IBoardUIGenerator _boardUIGenerator;
    private readonly IPromptUIGenerator _promptUIGenerator;
    private readonly ICoordinateTranslator _coordinateTranslator;

    private Cell? _lastCellHit;

    public UIGenerator(ILogger<UIGenerator> logger,
        IGameService gameService,
        IBoardUIGenerator boardUIGenerator,
        IPromptUIGenerator promptUIGenerator,
        ICoordinateTranslator coordinateTranslator)
    {
        _logger = logger;

        _gameService = gameService;

        _boardUIGenerator = boardUIGenerator;
        _promptUIGenerator = promptUIGenerator;
        _coordinateTranslator = coordinateTranslator;
    }

    public void RenderGame()
    {
        while (!_gameService.IsGameOver())
        {
            _logger.LogInformation("Starting round of play.");

            Console.Clear();
            GenerateUI();

            _logger.LogInformation("Generated UI for the round.");

            if (_lastCellHit != null && _lastCellHit.State == CellState.Hit)
            {
                _logger.LogInformation("Player hit last round.");

                Console.WriteLine($"{(_lastCellHit.Ship!.IsSunk() ? "Sunk!" : "Hit!" )} Ship's length: {_lastCellHit.Ship!.Length}.");
            }
            else if (_lastCellHit != null)
            {
                _logger.LogInformation("Player missed last round.");

                Console.WriteLine("Miss.");
            }

            _logger.LogInformation("Prompting player for a hit action.");

            PromptHitAction();

            _logger.LogInformation("Player input hit coordinates.");
        }

        Console.Clear();
        GenerateUI();

        _logger.LogInformation("Player won. Ending game.");

        Console.WriteLine("You win!");
    }

    private void GenerateUI()
    {
        var grid = _gameService.GetGrid();
        _boardUIGenerator.GenerateBoardUI(grid);
    }

    private void PromptHitAction()
    {
        _logger.LogInformation("Translating hit coordinates.");

        var hitCoordinates = _coordinateTranslator.TranslateCoordinates(_promptUIGenerator.ShowHitPrompt());

        _logger.LogInformation("Resolving hit action.");

        _gameService.ResolveHitAction(hitCoordinates.column, hitCoordinates.row);

        _logger.LogInformation("Saving hit result.");

        _lastCellHit = _gameService.GetCell(hitCoordinates.column, hitCoordinates.row);
    }
}
