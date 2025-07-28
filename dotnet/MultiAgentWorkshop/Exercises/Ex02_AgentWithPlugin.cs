using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace MultiAgentWorkshop.Exercises
{
    /// <summary>
    /// EX02: Agent With Plugin
    ///
    /// Workshop Instructions:
    /// - Implement a chat agent that specializes in helping users with bookings.
    /// - Register and import a BookingPlugin into the Kernel.
    /// - The BookingPlugin should expose a function to check if a date is available for booking.
    /// - Create a ChatCompletionAgent with:
    ///   - Kernel set to _kernel
    ///   - Name set to "BookingAgent"
    ///   - Instructions describing its booking specialty
    ///   - Arguments to enable function calling (see OpenAIPromptExecutionSettings)
    /// - Create a ChatHistoryAgentThread and add system/assistant messages to guide the user.
    /// - Prompt the user for input, print a blank line and "> " prompt, read input from the console.
    /// - If input is empty/whitespace or "EXIT" (case-insensitive), exit.
    /// - Create a ChatMessageContent with AuthorRole.User and the user's input.
    /// - Invoke the agent asynchronously with the message and thread, print each response.
    ///
    /// </summary>
    internal class Ex02_AgentWithPlugin : IExerciseRunner
    {
        private readonly Kernel _kernel;

        // The Kernel instance is injected via constructor
        public Ex02_AgentWithPlugin(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task Run()
        {
            // 1. Import the BookingPlugin into the Kernel.
            //    - Use _kernel.ImportPluginFromType<BookingPlugin>();

            // 2. Create a ChatCompletionAgent instance.
            //    - Set Kernel, Name, Instructions, and Arguments (enable function calling).

            // 3. Create a ChatHistoryAgentThread instance.
            //    - Add system and assistant messages to guide the user.

            // 4. Prompt the user for input.
            //    - Print a blank line and "> " prompt.
            //    - Read input from the console.
            //    - If input is empty/whitespace or "EXIT", exit.

            // 5. Create a ChatMessageContent with AuthorRole.User and the user's input.

            // 6. Invoke the agent asynchronously with the message and thread.
            //    - For each response, print the response content to the console.

            // Implement the above steps below.
        }
    }

    /// <summary>
    /// Plugin for booking functionality.
    /// - Expose a function to check if a booking date is available.
    /// - Use [KernelFunction] attribute and a description.
    /// </summary>
    internal class BookingPlugin
    {
        // Implement a list of available dates and a function to check availability.
        // See the solution for details.
        internal IEnumerable<DateTime> _availableDates = new List<DateTime>()
        {
            new DateTime(2025, 10, 1),
            new DateTime(2025, 10, 2),
            new DateTime(2025, 10, 3)
        };


        [KernelFunction("booking_check_availability")]
        [Description("Check if a booking date is available")]
        public bool IsDateAvailable(DateTime date)
        {
            // Set a breakpoint here to inspect the date parameter.
            // Try different ways of providing the date, such as: 2025-10-01, 2025/10/01, 2025.10.01, first of October 2025, etc.
            return _availableDates.Contains(date);
        }
    }
}
