using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace MultiAgentWorkshop.Solutions
{
    internal class Ex02_AgentWithPlugin_Solution : IExerciseRunner
    {
        private readonly Kernel _kernel;

        public Ex02_AgentWithPlugin_Solution(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task Run()
        {
            _kernel.ImportPluginFromType<BookingPlugin>();
            var agent = new ChatCompletionAgent()
            {
                Kernel = _kernel,
                Name = "BookingAgent",
                Instructions = "You are a booking agent specialized in helping people with creating or managing bookings.",
                Arguments = new KernelArguments(
                new OpenAIPromptExecutionSettings()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                })
            };

            ChatHistoryAgentThread agentThread = new();
            agentThread.ChatHistory.AddSystemMessage("You are a booking agent specialized in helping people with creating or managing bookings. You can check availability of dates using the booking_check_availability function.");
            agentThread.ChatHistory.AddAssistantMessage("You can ask me to check if a date is available for booking. For example, you can say 'Is 2025-10-01 available?'");

            Console.WriteLine("You can ask me to check if a date is available for booking. For example, you can say 'Is 2025-10-01 available?'");
            Console.Write("> ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }
            if (input.Trim().Equals("EXIT", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            var message = new ChatMessageContent(AuthorRole.User, input);
            await foreach (ChatMessageContent response in agent.InvokeAsync(message, agentThread))
            {
                response.WriteAgentMessage();
            }
        }
    }

    internal class BookingPlugin
    {
        private IEnumerable<DateTime> _availableDates = new List<DateTime>()
        {
            new DateTime(2025, 10, 1),
            new DateTime(2025, 10, 2),
            new DateTime(2025, 10, 3),
            new DateTime(2025, 10, 4),
            new DateTime(2025, 10, 5)
        };

        [KernelFunction("booking_check_availability")]
        [Description("Check if a booking date is available")]
        public bool IsDateAvailable(DateTime date)
        {
            return _availableDates.Contains(date);
        }
    }
}
