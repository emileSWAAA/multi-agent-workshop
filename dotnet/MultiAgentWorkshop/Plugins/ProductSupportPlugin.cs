using System.ComponentModel;
using Microsoft.SemanticKernel;
using MultiAgentWorkshop.Data;

namespace MultiAgentWorkshop.Plugins
{
    internal class ProductSupportPlugin
    {
        [KernelFunction, Description("Get detailed product info.")]
        internal string GetProductDetails(string productName)
        {
            PluginHelper.WriteToConsole(nameof(GetProductDetails), productName);
            var product = MockData.Products.FirstOrDefault(p => p.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
            if (product != null)
            {
                return $"Product: {product.Name}\nDescription: {product.Description}\nPrice: ${product.Price}";
            }
            else
            {
                return $"Product '{productName}' not found. Please check the name and try again.";
            }
        }

        [KernelFunction, Description("List all available products.")]
        internal string[] ListProducts()
        {
            PluginHelper.WriteToConsole(nameof(ListProducts));
            return MockData.Products.Select(p => $"Product: {p.Name}, Description: {p.Description}, Price: ${p.Price}").ToArray();
        }
    }
}
