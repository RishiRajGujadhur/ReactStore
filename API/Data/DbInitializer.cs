using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Data;

public static class DbInitializer
{
    public static async Task Initialize(StoreContext context, UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new User
            {
                UserName = "bob",
                Email = "bob@test.com"
            };

            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Member");

            var admin = new User
            {
                UserName = "admin",
                Email = "admin@test.com"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Member" });
        }

        if (!context.GeneralSettings.Any())
        {
            var generalSettings = new GeneralSettings
            {
                Logo = "default.png",
                AppName = "My App",
                CompanyName = "My Company",
                DefaultCurrency = "USD",
                DefaultLanguage = "en"
            };

            context.GeneralSettings.Add(generalSettings);
            context.SaveChanges();
        }

        if (!context.InvoiceSettings.Any())
        {
            var invoiceSettings = new InvoiceSettings
            {
                Currency = "MUR",
                BottomNotice = "Bottom Notice",
                Locale = "en-US",
                TaxNotation = "vat",
                MarginTop = 10,
                MarginRight = 10,
                MarginLeft = 10,
                MarginBottom = 10,
                Format = "A4",
                Height = "210mm",
                Width = "297mm",
                Orientation = "portrait",
            };

            context.InvoiceSettings.Add(invoiceSettings);
            context.SaveChanges();
        }

        if(!context.InvoiceSenders.Any()){
            var invoiceSender = new InvoiceSender
            {
                Company = "My Company",
                Address = "My Address",
                Zip = "12345",
                City = "My City",
                Country = "My Country",
                UserId = 2
            };

            context.InvoiceSenders.Add(invoiceSender);
            context.SaveChanges();
        }

        if (context.Products.Any()) return;

        var products = new List<Product>
            {
                new Product
                {
                    Name = "Angular Speedster Board 2000",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 20000,
                    PictureUrl = "/images/products/sb-ang1.png",
                    Brand = "Angular",
                    Type = "Boards",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Green Angular Board 3000",
                    Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
                    Price = 15000,
                    PictureUrl = "/images/products/sb-ang2.png",
                    Brand = "Angular",
                    Type = "Boards",
                    QuantityInStock = 100
                }
            };

        foreach (var product in products)
        {
            context.Products.Add(product);
        }

        context.SaveChanges();
    }
}