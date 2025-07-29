using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Orchestration.Concurrent;
using Microsoft.SemanticKernel.Agents.Orchestration.GroupChat;
using Microsoft.SemanticKernel.Agents.Orchestration.Sequential;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
using Microsoft.SemanticKernel.ChatCompletion;

namespace MultiAgentWorkshop.Solutions
{
    internal class Ex03_Orchestration_Solution : IExerciseRunner
    {
        private readonly Kernel _kernel;

        internal Ex03_Orchestration_Solution(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task Run()
        {
            // Create multiple agents with different roles to brainstorm a new app idea.
            var productAgent = new ChatCompletionAgent
            {
                Kernel = _kernel,
                Name = "product_agent",
                Description = "A creative product manager who proposes app ideas based on trends and user needs.",
                Instructions = "You are a creative product manager brainstorming a new mobile app idea. " +
                "Focus on practicality, solving real-world problems, or entertainment. Start the discussion with a fresh idea."
            };

            var marketingAgent = new ChatCompletionAgent
            {
                Kernel = _kernel,
                Name = "marketing_agent",
                Description = "An enthusiastic marketer who develops names, slogans, and audience profiles for products.",
                Instructions = "You are a marketing expert. Based on the app idea, come up with a catchy name, a slogan, and define the primary target audience." +
                " Make it engaging and memorable. Be critical on the product and think of what the market wants, but also find opportunities and propose these."
            };

            var ctoAgent = new ChatCompletionAgent
            {
                Kernel = _kernel,
                Name = "cto_agent",
                Description = "A pragmatic and detail-oriented CTO who evaluates technical feasibility and suggests a tech stack.",
                Instructions = "You are the CTO. Review the product idea and marketing plan," +
                " suggest a suitable tech stack, and flag any major implementation concerns or opportunities." +
                "Be critical and propose changes to the product if the complexity or expected costs are too high and unknown."
            };

            // Initialize a ChatHistory to store the conversation between agents.
            var chatHistory = new ChatHistory();

            // Create a callback to capture agent responses as the sequence progresses via the ResponseCallback property.
            ValueTask responseCallback(ChatMessageContent response)
            {
                chatHistory.Add(response);
                return ValueTask.CompletedTask;
            }

            // Create a SequentialOrchestration object, passing in the agents and the response callback.
            // The agents will take turns responding, refining the result.
            var sequentialOrchestration = new SequentialOrchestration(productAgent, marketingAgent, ctoAgent)
            {
                ResponseCallback = responseCallback
            };

            // Create a ConcurrentOrchestration object, passing in the agents and the response callback.
            // The ConcurrentOrchestration allows agents to respond in parallel, simulating a more dynamic brainstorming session.
            var concurrentOrchestration = new ConcurrentOrchestration(productAgent, marketingAgent, ctoAgent)
            {
                ResponseCallback = responseCallback
            };

            // Create a GroupChatOrchestration object, passing in the agents, a group chat manager (here, a RoundRobinGroupChatManager), and the response callback.
            // The manager controls the flow—here, it alternates turns in a round-robin fashion for a set number of rounds.
            var groupChatOrchestration = new GroupChatOrchestration(
                new RoundRobinGroupChatManager { MaximumInvocationCount = 6 },
                productAgent, marketingAgent, ctoAgent)
            {
                ResponseCallback = responseCallback
            };

            // A runtime is required to manage the execution of agents. Here, we use InProcessRuntime and start it before invoking the orchestration.
            var runtime = new InProcessRuntime();
            await runtime.StartAsync();

            // Prompt the user for a topic to brainstorm.
            Console.WriteLine("Brainstorm a new app idea. Enter a topic:");
            var topic = Console.ReadLine();

            // Invoke the orchestration with your initial task (e.g., "Let's brainstorm a new health app.").
            // The agents will take turns responding, refining the result.
            // For this example, we will use the SequentialOrchestration to demonstrate the concept.
            // You can switch to ConcurrentOrchestration or GroupChatOrchestration to see different behaviors.
            var sessionResult = await sequentialOrchestration.InvokeAsync($"Let's brainstorm a new {topic} app.", runtime);
            var output = await sessionResult.GetValueAsync(TimeSpan.FromSeconds(30));

            // Wait for the orchestration to complete and retrieve the final output.
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