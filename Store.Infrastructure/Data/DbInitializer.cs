using Microsoft.AspNetCore.Identity;
using Store.Infrastructure.Entities;

namespace Store.Infrastructure.Data;

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
                LogoURL = "default.png",
                AppName = "My App",
                CompanyName = "My Company",
                DefaultCurrency = "USD",
                DefaultLanguage = "en",
                CreatedAtTimestamp = DateTime.UtcNow
            };

            context.GeneralSettings.Add(generalSettings);
            context.SaveChanges();
        }

        if (!context.FeatureSettings.Any())
        {
            var featureSettings = new List<FeatureSettings>
            {
                new FeatureSettings
                {
                    IsFeatureEnabled = true,
                    FeatureName = "Summary",
                    FeatureDescription = "Displays a list of Summaries.",
                    DisplayOrder = 1,
                    FeatureIcon = "Summarize",
                    FeatureRoute = "/",
                    FeatureType = "Summary",
                    FeatureCategory = "Summary",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                CreatedAtTimestamp = DateTime.UtcNow,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Profile",
                    FeatureDescription = "Profile",
                    DisplayOrder = 1,
                    FeatureIcon = "Person",
                    FeatureRoute = "/customer/create",
                    FeatureType = "Profile",
                    FeatureCategory = "Profile",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Wishlist",
                    FeatureDescription = "Wishlist",
                    DisplayOrder = 1,
                    FeatureIcon = "Favorite",
                    FeatureRoute = "/",
                    FeatureType = "Wishlist",
                    FeatureCategory = "Wishlist",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Purchases",
                    FeatureDescription = "Purchases",
                    DisplayOrder = 1,
                    FeatureIcon = "ShoppingBasket",
                    FeatureRoute = "/",
                    FeatureType = "Purchases",
                    FeatureCategory = "Purchases",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Returns",
                    FeatureDescription = "Returns",
                    DisplayOrder = 1,
                    FeatureIcon = "Loop",
                    FeatureRoute = "/",
                    FeatureType = "Returns",
                    FeatureCategory = "Returns",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Collections",
                    FeatureDescription = "Collections",
                    DisplayOrder = 1,
                    FeatureIcon = "Collections",
                    FeatureRoute = "/",
                    FeatureType = "Collections",
                    FeatureCategory = "Collections",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                  new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Likes",
                    FeatureDescription = "Likes",
                    DisplayOrder = 1,
                    FeatureIcon = "ThumbUp",
                    FeatureRoute = "/",
                    FeatureType = "Likes",
                    FeatureCategory = "Likes",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Reviews",
                    FeatureDescription = "Reviews",
                    DisplayOrder = 1,
                    FeatureIcon = "Reviews",
                    FeatureRoute = "/",
                    FeatureType = "Reviews",
                    FeatureCategory = "Reviews",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Comments",
                    FeatureDescription = "Comments",
                    DisplayOrder = 1,
                    FeatureIcon = "CommentBank",
                    FeatureRoute = "/",
                    FeatureType = "Comments",
                    FeatureCategory = "Comments",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Settings",
                    FeatureDescription = "Settings",
                    DisplayOrder = 1,
                    FeatureIcon = "SettingsApplications",
                    FeatureRoute = "/",
                    FeatureType = "Settings",
                    FeatureCategory = "Settings",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Support",
                    FeatureDescription = "Support",
                    DisplayOrder = 1,
                    FeatureIcon = "SupportAgent",
                    FeatureRoute = "/",
                    FeatureType = "Support",
                    FeatureCategory = "Support",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "History",
                    FeatureDescription = "History",
                    DisplayOrder = 1,
                    FeatureIcon = "ManageHistory",
                    FeatureRoute = "/",
                    FeatureType = "History",
                    FeatureCategory = "History",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Saved Searches",
                    FeatureDescription = "Saved Searches",
                    DisplayOrder = 1,
                    FeatureIcon = "SavedSearch",
                    FeatureRoute = "/",
                    FeatureType = "Saved Searches",
                    FeatureCategory = "Saved Searches",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Messages",
                    FeatureDescription = "Messages",
                    DisplayOrder = 1,
                    FeatureIcon = "Chat",
                    FeatureRoute = "/",
                    FeatureType = "Messages",
                    FeatureCategory = "Messages",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                 new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Watch items",
                    FeatureDescription = "Watch items",
                    DisplayOrder = 1,
                    FeatureIcon = "Visibility",
                    FeatureRoute = "/",
                    FeatureType = "Watch items",
                    FeatureCategory = "Watch items",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Rewards",
                    FeatureDescription = "Rewards",
                    DisplayOrder = 1,
                    FeatureIcon = "Loyalty",
                    FeatureRoute = "/",
                    FeatureType = "Rewards",
                    FeatureCategory = "Rewards",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Gift cards",
                    FeatureDescription = "Gift cards",
                    DisplayOrder = 1,
                    FeatureIcon = "Redeem",
                    FeatureRoute = "/",
                    FeatureType = "Gift cards",
                    FeatureCategory = "Gift cards",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Recommendations",
                    FeatureDescription = "Recommendations",
                    DisplayOrder = 1,
                    FeatureIcon = "Assistant",
                    FeatureRoute = "/",
                    FeatureType = "Recommendations",
                    FeatureCategory = "Recommendations",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Invoices",
                    FeatureDescription = "Displays a list of invoices.",
                    DisplayOrder = 1,
                    FeatureIcon = "Receipt",
                    FeatureRoute = "/my-invoices",
                    FeatureType = "Accounting",
                    FeatureCategory = "Accounting",
                    ParentFeatureId = 0,
                    AdminFeature = false,
                    EnabledForRoles = new List<string> { "Admin", "Member" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Invoice Sender Profile",
                    FeatureDescription = "Invoice Sender Profile",
                    DisplayOrder = 2,
                    FeatureIcon = "AccountBox",
                    FeatureRoute = "/invoiceSenderProfile",
                    FeatureType = "Settings",
                    FeatureCategory = "Invoice Settings",
                    ParentFeatureId = 0,
                    AdminFeature = true,
                    EnabledForRoles = new List<string> { "Admin" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "General Settings",
                    FeatureDescription = "generalSettings",
                    DisplayOrder = 3,
                    FeatureIcon = "Settings",
                    FeatureRoute = "/generalSettings",
                    FeatureType = "Settings",
                    FeatureCategory = "Settings",
                    ParentFeatureId = 0,
                    AdminFeature = true,
                    EnabledForRoles = new List<string> { "Admin" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Invoice Settings",
                    FeatureDescription = "Invoice Settings",
                    DisplayOrder = 4,
                    FeatureIcon = "SettingsApplications",
                    FeatureRoute = "/invoiceSettings",
                    FeatureType = "Invoice",
                    FeatureCategory = "Invoice",
                    ParentFeatureId = 0,
                    AdminFeature = true,
                    EnabledForRoles = new List<string> { "Admin" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Manage Users",
                    FeatureDescription = "Manage Users",
                    DisplayOrder = 2,
                    FeatureIcon = "Person",
                    FeatureRoute = "/manageUsers",
                    FeatureType = "Users",
                    FeatureCategory = "Users",
                    ParentFeatureId = 0,
                    AdminFeature = true,
                    EnabledForRoles = new List<string> { "Admin" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Manage Orders",
                    FeatureDescription = "Manage Orders",
                    DisplayOrder = 5,
                    FeatureIcon = "Redeem",
                    FeatureRoute = "/manageOrders",
                    FeatureType = "Orders",
                    FeatureCategory = "Orders",
                    ParentFeatureId = 0,
                    AdminFeature = true,
                    EnabledForRoles = new List<string> { "Admin" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Summary",
                    FeatureDescription = "Summary",
                    DisplayOrder = 5,
                    FeatureIcon = "Summarize",
                    FeatureRoute = "/AdminSummary",
                    FeatureType = "Stats",
                    FeatureCategory = "Stats",
                    ParentFeatureId = 0,
                    AdminFeature = true,
                    EnabledForRoles = new List<string> { "Admin" }
                },
                new FeatureSettings
                {
                    CreatedAtTimestamp = DateTime.UtcNow,
                    IsFeatureEnabled = true,
                    FeatureName = "Feature Settings",
                    FeatureDescription = "Feature Settings",
                    DisplayOrder = 5,
                    FeatureIcon = "Summarize",
                    FeatureRoute = "/FeatureSettings",
                    FeatureType = "Stats",
                    FeatureCategory = "Stats",
                    ParentFeatureId = 0,
                    AdminFeature = true,
                    EnabledForRoles = new List<string> { "Admin" }
                },
            };

            foreach (var featureSetting in featureSettings)
            {
                context.FeatureSettings.Add(featureSetting);
            }

            context.SaveChanges();
        }

        if (!context.InvoiceSettings.Any())
        {
            var invoiceSettings = new InvoiceSettings
            {
                CreatedAtTimestamp = DateTime.UtcNow,
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

        if (!context.InvoiceSenders.Any())
        {
            var invoiceSender = new InvoiceSender
            {
                CreatedAtTimestamp = DateTime.UtcNow,
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
                    CreatedAtTimestamp = DateTime.UtcNow,
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
                    CreatedAtTimestamp = DateTime.UtcNow,
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