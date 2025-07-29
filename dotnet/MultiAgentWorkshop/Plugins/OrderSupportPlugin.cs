using System.ComponentModel;
using Microsoft.SemanticKernel;
using MultiAgentWorkshop.Data;

namespace MultiAgentWorkshop.Plugins
{
    public class OrderSupportPlugin
    {
        [KernelFunction, Description("Track order by ID and return dynamic status info.")]
        public string TrackOrder(string orderId)
        {
            PluginHelper.WriteToConsole(nameof(TrackOrder), orderId);
            var order = MockData.Orders.FirstOrDefault(o => o.OrderId.Equals(orderId, StringComparison.OrdinalIgnoreCase));
            if (order == null)
            {
                return $"Order '{orderId}' not found. Please check the ID and try again.";
            }

            return $"Order ID: {order.OrderId}\n" +
                   $"Product ID: {order.ProductId}\n" +
                   $"Customer Name: {order.CustomerName}\n" +
                   $"Shipping Method: {order.Status}\n" +
                   "Status: Your order is being processed and will be shipped soon.";
        }

        [KernelFunction, Description("List all orders for a customer.")]
        public string[] ListOrdersByCustomer(string customerName)
        {
            PluginHelper.WriteToConsole(nameof(ListOrdersByCustomer), customerName);
            return MockData.Orders
                .Where(o => o.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase))
                .Select(o => $"{ o.OrderId}, { o.ProductId}, { o.Status}")
                .ToArray();
        }
    }
}
