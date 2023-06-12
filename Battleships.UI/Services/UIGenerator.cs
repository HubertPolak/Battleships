namespace Battleships.UI.Services;

using Battleships.App.Services;
using Battleships.Core.Models.Board;

public class UIGenerator : IUIGenerator
{
    private readonly IGameService _gameService;

    private readonly IBoardUIGenerator _boardUIGenerator;
    private readonly IPromptUIGenerator _promptUIGenerator;
    private readonly ICoordinateTranslator _coordinateTranslator;

    private Cell? _lastCellHit;

    public UIGenerator(IGameService gameService,
        IBoardUIGenerator boardUIGenerator,
        IPromptUIGenerator promptUIGenerator,
        ICoordinateTranslator coordinateTranslator)
    {
        _gameService = gameService;

        _boardUIGenerator = boardUIGenerator;
        _promptUIGenerator = promptUIGenerator;
        _coordinateTranslator = coordinateTranslator;
    }

    public void RenderGame()
    {

        while (!_gameService.IsGameOver())
        {
            Console.Clear();
            GenerateUI();

            if (_lastCellHit != null && _lastCellHit.State == CellState.Hit)
            {
                Console.WriteLine($"{(_lastCellHit.Ship!.IsSunk() ? "Sunk!" : "Hit!" )} Ship's length: {_lastCellHit.Ship!.Length}.");
            }
            else if (_lastCellHit != null)
            {
                Console.WriteLine("Miss.");
            }

            PromptHitAction();
        }

        Console.Clear();
        GenerateUI();
        Console.WriteLine("You win!");
    }

    private void GenerateUI()
    {
        var grid = _gameService.GetGrid();
        _boardUIGenerator.GenerateBoardUI(grid);
    }

    private void PromptHitAction()
    {
        var hitCoordinates = _coordinateTranslator.TranslateCoordinates(_promptUIGenerator.ShowHitPrompt());

        _gameService.ResolveHitAction(hitCoordinates.column, hitCoordinates.row);

        _lastCellHit = _gameService.GetCell(hitCoordinates.column, hitCoordinates.row);
    }
}
