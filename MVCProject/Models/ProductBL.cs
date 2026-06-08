namespace MVCProject.Models
{
    public class ProductBL
    {
        List<Product> products;

        public ProductBL()
        {
            products = new List<Product>()
            {
                new Product() { Id = 1, Name = "Laptop", Price = 25000, ImageURL = "/Images/Laptop.jpg", Description = "Dell Inspiron Laptop" },

                new Product() { Id = 2, Name = "Mouse", Price = 500, ImageURL = "/Images/Mouse.jpg", Description = "Wireless Mouse" },

                new Product() { Id = 3, Name = "Keyboard", Price = 1200, ImageURL = "/Images/Keyboard.jpg", Description = "Mechanical Keyboard" },

                new Product() { Id = 4, Name = "Monitor", Price = 7000, ImageURL = "/Images/Monitor.png", Description = "24 Inch LED Monitor" },

                new Product() { Id = 5, Name = "Headphones", Price = 1500, ImageURL = "/Images/Headphones.jpg", Description = "Bluetooth Headphones" },

                new Product() { Id = 6, Name = "Smartphone", Price = 18000, ImageURL = "/Images/Smartphone.jpg", Description = "Android Smartphone" },

                new Product() { Id = 7, Name = "Smart Watch", Price = 3500, ImageURL = "/Images/Smart Watch.jpg", Description = "Fitness Smart Watch" },

                new Product() { Id = 8, Name = "Tablet", Price = 12000, ImageURL = "/Images/Tablet.jpg", Description = "Android Tablet" },

                new Product() { Id = 9, Name = "Printer", Price = 4500, ImageURL = "/Images/Printer.jpg", Description = "Color Inkjet Printer" }

            };

        }

        public List<Product> GetAll()
        {
            return products;
        }

        public Product GetById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);

        }




    }
}

