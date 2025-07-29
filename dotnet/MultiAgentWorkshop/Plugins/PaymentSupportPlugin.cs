using System.ComponentModel;
using Microsoft.SemanticKernel;
using MultiAgentWorkshop.Data;

namespace MultiAgentWorkshop.Plugins
{
    public class PaymentSupportPlugin
    {
        [KernelFunction, Description("Process a refund for an order if it exists.")]
        public string ProcessRefund(string orderId)
        {
            PluginHelper.WriteToConsole(nameof(ProcessRefund), orderId);
            var order = MockData.Orders.FirstOrDefault(o => o.OrderId.Equals(orderId, StringComparison.OrdinalIgnoreCase));
            if (order == null)
            {
                return $"Order '{orderId}' not found. Cannot process refund.";
            }
            return $"Refund for order {order.OrderId} (Customer: {order.CustomerName}) has been initiated and will be processed within 5–7 business days.";
        }

        [KernelFunction, Description("List all customers with orders.")]
        public string[] ListCustomersWithOrders()
        {
            PluginHelper.WriteToConsole(nameof(ListCustomersWithOrders));
            return MockData.Orders.Select(o => o.CustomerName).Distinct().ToArray();
        }
    }
}
