using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;

namespace MultiAgentWorkshop.Solutions
{
    public class Ex01_BasicAgent_Solution : IExerciseRunner
    {
        private readonly Kernel _kernel;

        public Ex01_BasicAgent_Solution(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task Run()
        {
            var agent = new ChatCompletionAgent()
            {
                Kernel = _kernel,
                Name = "ComedianAgent",
                Instructions = "You are a comedian specialized in telling short story jokes."
            };

            ChatHistoryAgentThread agentThread = new();

            Console.WriteLine();
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
}