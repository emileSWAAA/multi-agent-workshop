using Microsoft.Extensions.DependencyInjection;
using MultiAgentWorkshop.Exercises;
using MultiAgentWorkshop.Solutions;

namespace MultiAgentWorkshop
{
    internal static class ExerciseUtility
    {
        internal static IServiceCollection AddExercises(this IServiceCollection services)
        {
            // Exercises
            services.AddTransient<Ex01_BasicAgent>();
            services.AddTransient<Ex02_AgentWithPlugin>();
            services.AddTransient<Ex03_GroupChat>();
            services.AddTransient<Ex04_Concurrent>();
            services.AddTransient<Ex05_Sequential>();
            services.AddTransient<Ex06_GroupChat_HumanInTheLoop>();
            services.AddTransient<Ex07_GroupChat_AIManager>();
            services.AddTransient<Ex08_HandOff>();
            services.AddTransient<Bonus01_Telemetry>();
            services.AddTransient<Bonus02_StructuredOutput>();

            // Solutions
            services.AddTransient<Ex01_BasicAgent_Solution>();
            services.AddTransient<Ex02_AgentWithPlugin_Solution>();
            services.AddTransient<Ex03_GroupChat_Solution>();
            services.AddTransient<Ex04_Concurrent_Solution>();
            services.AddTransient<Ex05_Sequential_Solution>();
            services.AddTransient<Ex06_GroupChat_HumanInTheLoop_Solution>();
            services.AddTransient<Ex07_GroupChat_AIManager_Solution>();
            services.AddTransient<Ex08_HandOff_Solution>();
            services.AddTransient<Bonus01_Telemetry_Solution>();
            services.AddTransient<Bonus02_StructuredOutput_Solution>();

            return services;
        }

        internal static Dictionary<string, IExerciseRunner> GetActions(IServiceProvider provider, bool useSolutions = false)
        {
            if (!useSolutions)
            {
                return new()
                {
                    ["1"] = provider.GetRequiredService<Ex01_BasicAgent>(),
                    ["2"] = provider.GetRequiredService<Ex02_AgentWithPlugin>(),
                    ["3"] = provider.GetRequiredService<Ex03_GroupChat>(),
                    ["4"] = provider.GetRequiredService<Ex04_Concurrent>(),
                    ["5"] = provider.GetRequiredService<Ex05_Sequential>(),
                    ["6"] = provider.GetRequiredService<Ex06_GroupChat_HumanInTheLoop>(),
                    ["7"] = provider.GetRequiredService<Ex07_GroupChat_AIManager>(),
                    ["8"] = provider.GetRequiredService<Ex08_HandOff>(),
                    ["9"] = provider.GetRequiredService<Bonus01_Telemetry>(),
                    ["10"] = provider.GetRequiredService<Bonus02_StructuredOutput>(),
                };
            }
            else
            {
                return new()
                {
                    ["1"] = provider.GetRequiredService<Ex01_BasicAgent_Solution>(),
                    ["2"] = provider.GetRequiredService<Ex02_AgentWithPlugin_Solution>(),
                    ["3"] = provider.GetRequiredService<Ex03_GroupChat_Solution>(),
                    ["4"] = provider.GetRequiredService<Ex04_Concurrent_Solution>(),
                    ["5"] = provider.GetRequiredService<Ex05_Sequential_Solution>(),
                    ["6"] = provider.GetRequiredService<Ex06_GroupChat_HumanInTheLoop_Solution>(),
                    ["7"] = provider.GetRequiredService<Ex07_GroupChat_AIManager_Solution>(),
                    ["8"] = provider.GetRequiredService<Ex08_HandOff_Solution>(),
                    ["9"] = provider.GetRequiredService<Bonus01_Telemetry>(),
                    ["10"] = provider.GetRequiredService<Bonus02_StructuredOutput>(),
                };
            }
        }
    }
}