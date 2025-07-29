namespace MultiAgentWorkshop.Data
{
    public class MockData
    {
        public class Product
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }

            public Product(string id, string name, string description, decimal price)
            {
                Id = id;
                Name = name;
                Description = description;
                Price = price;
            }
        }

        public class Order
        {
            public string OrderId { get; set; }
            public string ProductId { get; set; }
            public string CustomerName { get; set; }
            public ShippingMethod Status { get; set; }

            public Order(string orderId, string productId, string customerName, ShippingMethod status)
            {
                OrderId = orderId;
                ProductId = productId;
                CustomerName = customerName;
                Status = status;
            }
        }

        public static List<Product> Products { get; } = new()
        {
            new Product("P001", "Wireless Mouse", "Ergonomic wireless mouse with 2.4GHz receiver", 29.99m),
            new Product("P002", "Mechanical Keyboard", "RGB mechanical keyboard with blue switches", 89.99m),
            new Product("P003", "Noise Cancelling Headphones", "Over-ear headphones with ANC", 199.99m)
        };

        public static List<Order> Orders { get; } = new()
        {
            new Order("S001", "P001", "Alice Smith", ShippingMethod.Standard),
            new Order("S002", "P003", "Bob Johnson", ShippingMethod.Express),
            new Order("S003", "P002", "Charlie Brown", ShippingMethod.SameDay)
        };
    }

    public enum ShippingMethod
    {
        Standard,
        Express,
        SameDay
    }
}
