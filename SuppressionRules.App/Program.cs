using Cocona;
using Microsoft.Extensions.DependencyInjection;

namespace SuppressionRules.App;

internal class Program
{
    static void Main(string[] args)
    {
        var builder = CoconaApp.CreateBuilder();
        builder.Services.Configure<CoconaAppOptions>((options) => options.TreatPublicMethodsAsCommands = false);

        builder.Services.AddSingleton<RuleViolationStatisticsService>();
        builder.Services.AddSingleton<JsonReader>();
        builder.Services.AddSingleton<JsonWriter>();

        var app = builder.Build();
        app.AddCommands<RuleCommands>();

        app.Run();
    }
}
