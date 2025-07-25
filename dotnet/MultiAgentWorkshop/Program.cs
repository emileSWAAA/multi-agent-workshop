using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiAgentWorkshop;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddExercises();
        services.AddSemanticKernel(context);
    })
    .Build();

var scope = host.Services.CreateScope();
var provider = scope.ServiceProvider;

while (true)
{
    Console.WriteLine("Choose an exercise (type the number in front to execute):");
    Console.WriteLine("1. Basic Agent");
    Console.WriteLine("2. Agent with Plugin");
    Console.WriteLine("3. Group Chat orchestration");
    Console.WriteLine("4. Concurrent orchestration");
    Console.WriteLine("5. Sequential orchestration");
    Console.WriteLine("6. Group Chat - Human in the loop");
    Console.WriteLine("7. Group Chat - AI Manager");
    Console.WriteLine("8. Agent hand-off");
    Console.WriteLine("--------");
    Console.WriteLine("Bonus 1: Telemetry");
    Console.WriteLine("Bonus 2: Structured output");
    Console.WriteLine("Type 'exit' to quit.");
    Console.WriteLine();

    Console.Write("> ");
    var choice = Console.ReadLine();
    if (choice == null || choice.Equals("exit", StringComparison.OrdinalIgnoreCase))
    {
        return 0;
    }

    var actions = ExerciseUtility.GetActions(provider, useSolutions: true);
    if (actions.TryGetValue(choice, out var instance) && instance is IExerciseRunner runner)
    {
        Console.WriteLine($"Starting exercise: {runner.GetType().Name}");
        await runner.Run();
        Console.WriteLine();
        Console.WriteLine($"Exercise ended.");
        Console.WriteLine($"Press Enter to return to the menu...");
        Console.ReadLine();
    }
    else
    {
        Console.WriteLine("Invalid option. Please try again.\n");
    }
}