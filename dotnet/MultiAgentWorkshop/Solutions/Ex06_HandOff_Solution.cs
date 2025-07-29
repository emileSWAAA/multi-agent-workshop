using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Orchestration.Handoff;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
using Microsoft.SemanticKernel.ChatCompletion;
using MultiAgentWorkshop.Plugins;

namespace MultiAgentWorkshop.Solutions
{
    public class Ex06_HandOff_Solution : IExerciseRunner
    {
        private readonly Kernel _kernel;

        public Ex06_HandOff_Solution(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task Run()
        {
            // Create multiple agents with different roles to handle customer support requests.
            // The SupportAgent will triage requests and forward them to the correct expert agent.
            var supportAgent = new ChatCompletionAgent
            {
                Name = "SupportAgent",
                Description = "General support triage bot.",
                Instructions = "You are the main support assistant. Triage requests and forward them to the correct expert agent.",
                Kernel = _kernel
            };

            // The ProductAgent will handle product-related questions.
            var productAgent = new ChatCompletionAgent
            {
                Name = "ProductAgent",
                Description = "Handles product questions and comparisons.",
                Instructions = "You help users with product questions, specifications, and recommendations.",
                Kernel = _kernel
            };
            productAgent.Kernel.ImportPluginFromType<ProductSupportPlugin>();

            // The OrderAgent will handle order-related inquiries.
            var orderAgent = new ChatCompletionAgent
            {
                Name = "OrderAgent",
                Description = "Manages order lookups and tracking.",
                Instructions = "You help customers with their past orders, including order lookup, tracking, and cancellations.",
                Kernel = _kernel
            };
            orderAgent.Kernel.ImportPluginFromType<OrderSupportPlugin>();

            // The ShippingAgent will handle shipping-related questions.
            var shippingAgent = new ChatCompletionAgent
            {
                Name = "ShippingAgent",
                Description = "Handles shipping questions and issues.",
                Instructions = "You provide support around shipping status, delays, and shipping methods.",
                Kernel = _kernel
            };
            shippingAgent.Kernel.ImportPluginFromType<ShippingSupportPlugin>();

            // The PaymentAgent will handle payment-related issues.
            var paymentAgent = new ChatCompletionAgent
            {
                Name = "PaymentAgent",
                Description = "Handles payments, refunds, and billing issues.",
                Instructions = "You support customers with billing, refunds, and payment concerns.",
                Kernel = _kernel
            };
            paymentAgent.Kernel.ImportPluginFromType<PaymentSupportPlugin>();

            // Initialize a ChatHistory to store the conversation between agents.
            var chatHistory = new ChatHistory();

            // Create a callback to capture agent responses as the sequence progresses via the ResponseCallback property.
            ValueTask responseCallback(ChatMessageContent response)
            {
                chatHistory.Add(response);
                return ValueTask.CompletedTask;
            }

            // Create a callback to capture user input for interactive handoff.
            async ValueTask<ChatMessageContent> interactiveCallback()
            {
                // Show the last agent message before prompting the user
                var lastMessage = chatHistory.LastOrDefault();
                lastMessage?.WriteAgentMessage();

                Console.Write("\nUser: ");
                var input = Console.ReadLine();
                return new ChatMessageContent(AuthorRole.User, input);
            }

            // Define handoff relationships between agents.
            // The SupportAgent can forward requests to the appropriate expert agent based on the topic.
            // Each expert agent can also hand back control to the SupportAgent if they cannot resolve the issue.
            // This allows for a flexible support system where the SupportAgent acts as a triage point.
            var handoffs = OrchestrationHandoffs
                .StartWith(supportAgent)
                .Add(supportAgent, productAgent, orderAgent, shippingAgent, paymentAgent)
                .Add(productAgent, supportAgent)
                .Add(orderAgent, supportAgent)
                .Add(shippingAgent, supportAgent)
                .Add(paymentAgent, supportAgent);

            // Create orchestration with the defined handoffs and agents
            // This orchestration will allow the support agent to triage requests and forward them to the appropriate expert agent.
            var orchestration = new HandoffOrchestration(
                handoffs,
                supportAgent,
                productAgent,
                orderAgent,
                shippingAgent,
                paymentAgent)
            {
                ResponseCallback = responseCallback,
                InteractiveCallback = interactiveCallback
            };

            // Create a runtime to execute the orchestration
            var runtime = new InProcessRuntime();
            await runtime.StartAsync();

            // Run the orchestration
            var result = await orchestration.InvokeAsync("Hello, I need help with my order and a product.", runtime);
            var output = await result.GetValueAsync(TimeSpan.FromSeconds(120));

            // Print the final response and conversation history
            Console.WriteLine("\n# FINAL RESPONSE:\n");
            Console.WriteLine(output);
            Console.WriteLine("\n# CONVERSATION HISTORY:\n");
            foreach (var msg in chatHistory)
            {
                msg.WriteAgentMessage();
            }

            // Stop the runtime after completion
            await runtime.RunUntilIdleAsync();
        }
    }
}
