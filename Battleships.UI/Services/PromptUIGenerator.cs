using Sharprompt;

namespace Battleships.UI.Services;

public class PromptUIGenerator : IPromptUIGenerator
{
    public string ShowHitPrompt()
    {
        var columnNamesRange = $"{Constants.ColumnNames.First()}-{Constants.ColumnNames.Last()}";
        var hitCoordinates = Prompt.Input<string>("Enter cell to target",
            validators: new[] 
            { 
                Validators.Required(), 
                Validators.MinLength(2), 
                Validators.MaxLength(3), 
                Validators.RegularExpression(@$"[{columnNamesRange}]10|[{columnNamesRange}][1-9]$", "Value does not match a pattern for valid coordinates.") 
            });

        Console.WriteLine($"Targeted: {hitCoordinates}");

        return hitCoordinates;
    }
}
