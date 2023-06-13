namespace Battleships.UI.Services;

using Microsoft.Extensions.Logging;
using Sharprompt;

public class PromptUIGenerator : IPromptUIGenerator
{
    private readonly ILogger<PromptUIGenerator> _logger;

    public PromptUIGenerator(ILogger<PromptUIGenerator> logger)
    {
        _logger = logger;
    }

    public string ShowHitPrompt()
    {
        _logger.LogInformation("Showing hit promopt to user.");

        var columnNamesRange = $"{Constants.ColumnLetters.First()}-{Constants.ColumnLetters.Last()}";
        var hitCoordinates = Prompt.Input<string>("Enter cell to target",
            validators: new[] 
            { 
                Validators.Required(), 
                Validators.MinLength(2), 
                Validators.MaxLength(3), 
                Validators.RegularExpression(@$"[{columnNamesRange}]10|[{columnNamesRange}][1-9]$", "Value does not match a pattern for valid coordinates.") 
            });

        _logger.LogInformation("Hit coordinates chosen {hitCoordinates}", hitCoordinates);

        return hitCoordinates;
    }
}
