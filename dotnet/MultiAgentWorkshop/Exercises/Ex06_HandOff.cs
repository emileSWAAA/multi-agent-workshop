using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Orchestration.Handoff;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
using Microsoft.SemanticKernel.ChatCompletion;
using MultiAgentWorkshop.Plugins;

namespace MultiAgentWorkshop.Exercises
{
    /// <summary>
    /// EX06: Agent Handoff Orchestration Exercise
    ///
    /// In this exercise, you will:
    /// - Create a support triage agent and several expert agents (Product, Order, Shipping, Payment).
    /// - Use plugins to extend expert agent capabilities for their respective domains.
    /// - Define handoff relationships so the support agent can forward requests to the correct expert.
    /// - Implement a handoff orchestration that routes user requests to the right agent and returns control to support as needed.
    /// - Use a ChatHistory to track the conversation and callbacks to capture agent responses and user input.
    /// - Run a sample support session and print the conversation history.
    ///
    /// - Use Mock data in the folder: 'Data' to simulate user requests.
    /// - Use the pre-defined Plugins in the folder: 'Plugins' to handle specific support requests and map to the correct Agent.
    /// </summary>
    internal class Ex06_HandOff : IExerciseRunner
    {
        private readonly Kernel _kernel;

        public Ex06_HandOff(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task Run()
        {
            // EX06: Agent Handoff Orchestration
            //
            // Instructions:
            // 1. Create a support triage agent (SupportAgent) and expert agents (ProductAgent, OrderAgent, ShippingAgent, PaymentAgent).
            //   - SupportAgent: General support triage bot.
            //
            // 2. Use plugins to extend expert agent capabilities for their respective domains.
            //   - ProductAgent: Handles product-related questions using ProductSupportPlugin.
            //   - OrderAgent: Manages order lookups and tracking using OrderSupportPlugin.
            //   - ShippingAgent: Handles shipping-related questions using ShippingSupportPlugin.
            //   - PaymentAgent: Handles payment-related issues using PaymentSupportPlugin.
            //
            // 3. Define handoff relationships so the support agent can forward requests to the correct expert.
            //   - Use the HandoffOrchestration to manage the handoff process.
            //
            // 4. Implement a handoff orchestration that routes user requests to the right agent and returns control to support as needed.
            //   - Create a HandoffOrchestration instance and add the agents.
            //   - Use InProcessRuntime to manage agent execution.
            //
            // 5. Use a ChatHistory to track the conversation and callbacks to capture agent responses and user input.
            //
            // 6. Run a sample support session and print the conversation history.
            //
            // 7. Prompt the user for a support request, invoke the handoff orchestration, and print the chat history.
            //    - Example support request: "I have a question about my order status."
            //    - Check the 'Data' folder for Mock data
            //
            // 8. Stop the runtime when done
        }
    }
}
