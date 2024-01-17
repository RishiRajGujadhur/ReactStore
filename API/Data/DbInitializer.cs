using API.Entities;

namespace API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(StoreContext context)
        {
            if(context.Products.Any()) return;

            var products = new List<Product>{
                new Product{
                    Name = "BMW x4",
                    Description = "Automatic",
                    Price = 19000,
                    Brand = "BMW",
                    Type = "Cars",
                    QuantityInStock = 8
                },
                 new Product{
                    Name = "Toyota Corrola",
                    Description = "Automatic",
                    Price = 12000,
                    Brand = "Toyota",
                    Type = "Cars",
                    QuantityInStock = 10
                }
            };

            foreach(Product product in products){
                context.Products.Add(product);
            };
            
            context.SaveChanges();
        }
    }
}