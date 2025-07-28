using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Orchestration.GroupChat;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
using Microsoft.SemanticKernel.ChatCompletion;

namespace MultiAgentWorkshop.Exercises
{
    /// <summary>
    /// EX03: Group Chat Orchestration
    ///
    /// Workshop Instructions:
    /// - Implement a group chat orchestration to brainstorm a new app idea using multiple agents.
    /// - Create three ChatCompletionAgent instances:
    ///   - Product Manager: Proposes creative app ideas.
    ///   - Marketing Expert: Develops names, slogans, and audience profiles.
    ///   - CTO: Evaluates technical feasibility and suggests a tech stack.
    /// - Set Kernel, Name, Description, and Instructions for each agent.
    /// - Create a ChatHistory to store the conversation between agents.
    /// - Create a callback to capture agent responses and add them to the chat history.
    /// - Create a GroupChatOrchestration with the agents, a RoundRobinGroupChatManager, and the response callback.
    /// - Use InProcessRuntime to manage agent execution and start it before invoking the orchestration.
    /// - Prompt the user for a topic to brainstorm (e.g., health, finance, travel).
    /// - Invoke the orchestration with an initial task (e.g., "Let's brainstorm a new health app.").
    /// - Print the final summary and the chat history after orchestration completes.
    /// - Stop the runtime after the orchestration is complete.
    /// </summary>
    internal class Ex03_GroupChat : IExerciseRunner
    {
        private readonly Kernel _kernel;

        // The Kernel instance is injected via constructor
        public Ex03_GroupChat(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task Run()
        {
            // 1. Create three ChatCompletionAgent instances for Product Manager, Marketing Expert, and CTO.
            //    - Set Kernel, Name, Description, and Instructions for each agent.
            // 2. Create a ChatHistory to store the conversation between agents.
            var chatHistory = new ChatHistory();

            // 3. Create a callback to capture agent responses and add them to the chat history.
            // 4. Create a GroupChatOrchestration with the agents, a RoundRobinGroupChatManager, and the response callback.
            // 5. Use InProcessRuntime to manage agent execution and start it before invoking the orchestration.
            var runtime = new InProcessRuntime();
            await runtime.StartAsync();

            // 6. Prompt the user for a topic to brainstorm (e.g., health, finance, travel).
            // 7. Invoke the orchestration with an initial task (e.g., "Let's brainstorm a new health app.").
            // 8. Print the final summary and the chat history after orchestration completes.
            Console.WriteLine("Chat History:");
            foreach (var msg in chatHistory)
            {
                msg.WriteAgentMessage();
            }

            // Stop the runtime after the orchestration is complete.
            await runtime.RunUntilIdleAsync();
        }
    }
}
