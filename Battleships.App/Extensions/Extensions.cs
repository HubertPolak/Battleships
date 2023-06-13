namespace Battleships.App.Extensions;

using Battleships.App.Services;
using Battleships.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

public static class Extensions
{
    public static IServiceCollection AddGameServices(this IServiceCollection services)
    {
        services.AddSingleton<IGameService, GameService>();

        return services;
    }

    public static IServiceCollection AddUIServices(this IServiceCollection services)
    {
        services.AddSingleton<IUIGenerator, UIGenerator>()
                .AddSingleton<IBoardUIGenerator, BoardUIGenerator>()
                .AddSingleton<IPromptUIGenerator, PromptUIGenerator>()
                .AddSingleton<ICoordinateTranslator, CoordinateTranslator>();

        return services;
    }

    public static IServiceCollection AddSerilogLogging(this IServiceCollection services) 
    {
        var serilogLogger = new LoggerConfiguration().WriteTo.File("log.txt").CreateLogger();

        services.AddLogging(builder =>
        {
            builder.SetMinimumLevel(LogLevel.Debug);
            builder.AddSerilog(serilogLogger, true);
        });

        return services;
    }
}
