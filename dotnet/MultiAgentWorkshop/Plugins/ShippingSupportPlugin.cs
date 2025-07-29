using System.ComponentModel;
using Microsoft.SemanticKernel;
using MultiAgentWorkshop.Data;

namespace MultiAgentWorkshop.Plugins
{
    public class ShippingSupportPlugin
    {
        [KernelFunction, Description("List available shipping methods.")]
        public string GetShippingOptions()
        {
            PluginHelper.WriteToConsole(nameof(GetShippingOptions));
            return "We offer Standard (3–5 days), Express (1–2 days), and Same-Day shipping in select locations.";
        }

        [KernelFunction, Description("Get shipping status for an order.")]
        public string GetShippingStatus(string orderId)
        {
            PluginHelper.WriteToConsole(nameof(GetShippingStatus), orderId);
            var order = MockData.Orders.FirstOrDefault(o => o.OrderId.Equals(orderId, StringComparison.OrdinalIgnoreCase));
            if (order == null)
            {
                return $"Order '{orderId}' not found.";
            }
            return $"Order {order.OrderId} shipping method: {order.Status}.";
        }

        [KernelFunction, Description("Get estimated delivery date for an order.")]
        public string GetEstimatedDeliveryDate(string orderId)
        {
            PluginHelper.WriteToConsole(nameof(GetEstimatedDeliveryDate), orderId);
            var order = MockData.Orders.FirstOrDefault(o => o.OrderId.Equals(orderId, StringComparison.OrdinalIgnoreCase));
            if (order == null)
            {
                return $"Order '{orderId}' not found.";
            }

            // Simulate estimated delivery date based on shipping method
            var estimatedDays = order.Status switch
            {
                ShippingMethod.Standard => 5,
                ShippingMethod.Express => 2,
                ShippingMethod.SameDay => 0,
                _ => 5
            };

            return $"Estimated delivery for order {order.OrderId} is in {estimatedDays} days.";
        }
    }
}
