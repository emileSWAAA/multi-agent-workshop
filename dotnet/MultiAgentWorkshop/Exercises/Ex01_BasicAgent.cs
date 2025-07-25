using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;

namespace MultiAgentWorkshop.Exercises
{
    /// <summary>
    /// EX01: Basic ChatCompletion Agent
    ///
    /// Workshop Instructions:
    /// - Implement a chat agent that specializes in telling short story jokes.
    /// - The agent should use dependency injection to receive a Kernel instance.
    /// - Prompt the user for input, send it to the agent, and print the response.
    /// - If the user enters "EXIT", the program should terminate.
    ///
    /// Setup:
    /// - Register this exercise runner in Program.cs:
    ///   services.AddTransient<IExerciseRunner, Ex01_BasicAgent>();
    /// - Ensure Kernel is registered and available for injection.
    /// </summary>
    public class Ex01_BasicAgent : IExerciseRunner
    {
        private readonly Kernel _kernel;

        // The Kernel instance is injected via constructor
        public Ex01_BasicAgent(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task Run()
        {
            // 1. Create a ChatCompletionAgent instance.
            //    - Set the Kernel property to _kernel.
            //    - Set the Name to "ComedianAgent".
            //    - Set the Instructions to "You are a comedian specialized in telling short story jokes."

            // 2. Create a ChatHistoryAgentThread instance to track the conversation.

            // 3. Prompt the user for input.
            //    - Print a blank line and "> " prompt.
            //    - Read input from the console.
            //    - If input is empty or whitespace, exit.
            //    - If input is "EXIT" (case-insensitive), exit.

            // 4. Create a ChatMessageContent with AuthorRole.User and the user's input.

            // 5. Invoke the agent asynchronously with the message and thread.
            //    - For each response, print the response content to the console.

            // Implement the above steps below.
        }
    }
}
