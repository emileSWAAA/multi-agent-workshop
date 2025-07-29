using Azure;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Orchestration.Concurrent;
using Microsoft.SemanticKernel.Agents.Orchestration.GroupChat;
using Microsoft.SemanticKernel.Agents.Orchestration.Sequential;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
using Microsoft.SemanticKernel.ChatCompletion;

namespace MultiAgentWorkshop.Exercises
{
    /// <summary>
    /// EX03: Multi-Agent Orchestration Exercise
    ///
    /// In this exercise, you will:
    /// - Create three specialized ChatCompletionAgent instances (Product Manager, Marketing Expert, CTO).
    /// - Orchestrate a brainstorming session using one of the orchestration types (Sequential, Concurrent, or GroupChat).
    /// - Use a ChatHistory to track the conversation and a response callback to capture agent responses.
    /// - Prompt the user for a topic and run the orchestration to generate a collaborative app idea.
    /// - Print the chat history to the console after the session.
    /// </summary>
    internal class Ex03_Orchestration : IExerciseRunner
    {
        private readonly Kernel _kernel;

        public Ex03_Orchestration(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task Run()
        {
            // EX03: Multi-Agent Orchestration
            //
            // Instructions:
            // 1. Create three ChatCompletionAgent instances:
            //    - Product Manager: Proposes creative app ideas.
            //    - Marketing Expert: Develops names, slogans, and audience profiles.
            //    - CTO: Evaluates technical feasibility and suggests a tech stack.
            //

            // 2. Create a ChatHistory to store the conversation.
            var chatHistory = new ChatHistory();

            // 3. Implement a response callback that adds each agent's response to the ChatHistory.
            ValueTask responseCallback(ChatMessageContent response)
            {
                chatHistory.Add(response);
                return ValueTask.CompletedTask;
            }

            // 4. Create at least one orchestration (SequentialOrchestration, ConcurrentOrchestration, or GroupChatOrchestration)
            //    and pass the agents and response callback.
            //    when using the GroupChatOrchestration, make sure to create a GroupChatManager (RoundRobinGroupChatManager)
            //
            // 5. Use InProcessRuntime to manage agent execution.
            //
            // 6. Prompt the user for a topic to brainstorm (e.g., "health").
            //
            // 7. Start the runtime, invoke the orchestration with the initial task (e.g., "Let's brainstorm a new {topic} app."),
            //    and wait for the orchestration to complete.
            //
            // 8. Print the chat history to the console.
            //
            // 9. Stop the runtime after completion.
            //
            // Implement the above steps below.
        }
    }
}
