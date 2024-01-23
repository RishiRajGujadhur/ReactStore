using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class StoreContext : IdentityDbContext<User, Role, int>
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionUsage> PromotionUsages { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<ReturnRequest> ReturnRequests { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Logistics> Logistics { get; set; }
        public DbSet<Invoice> Invoices { get; set; } 
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<OrderDiscount> OrderDiscounts { get; set; }
        public DbSet<AdditionalDeliveryInfo> AdditionalDeliveryInfos { get; set; }
        public DbSet<Basket> Baskets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
           .HasOne(a => a.Address)
           .WithOne()
           .HasForeignKey<UserAddress>(a => a.Id)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole { Name = "Member", NormalizedName = "MEMBER" },
                    new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
                );

            modelBuilder.Entity<Role>()
          .HasData(
              new Role { Id = 1, Name = "Member", NormalizedName = "MEMBER" },
              new Role { Id = 2, Name = "Admin", NormalizedName = "ADMIN" }
          );

            // One-to-Many relationship between Customer and Order
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerID);

            // One-to-Many relationship between Order and OrderItem
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderID);

            // One-to-Many relationship between Order and ReturnRequest
            modelBuilder.Entity<Order>()
                .HasMany(o => o.ReturnRequests)
                .WithOne(rr => rr.Order)
                .HasForeignKey(rr => rr.OrderID);

            // One-to-Many relationship between Product and Review
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Reviews)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductID);

            // One-to-Many relationship between Customer and Review
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Reviews)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerID);

            // Configure cascading delete behavior if needed
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.ReturnRequests)
                .WithOne(rr => rr.Order)
                .OnDelete(DeleteBehavior.Cascade); 

            // Payment and Order relationship
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderID);

            // Wishlist and Product relationship
            modelBuilder.Entity<Wishlist>()
                .HasMany(w => w.Products)
                .WithMany(p => p.Wishlists)
                .UsingEntity(j => j.ToTable("WishlistProducts"));

            // OrderDiscount and Order relationship
            modelBuilder.Entity<OrderDiscount>()
                .HasOne(od => od.Order)
                .WithMany(o => o.Discounts)
                .HasForeignKey(od => od.OrderID);

            // AdditionalDeliveryInfo and Order relationship
            modelBuilder.Entity<AdditionalDeliveryInfo>()
                .HasOne(adi => adi.Order)
                .WithMany(o => o.AdditionalDeliveryInfos)
                .HasForeignKey(adi => adi.OrderID);


            // Configure one-to-one relationship between Customer and User
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne(u => u.Customer)
                .HasForeignKey<Customer>(c => c.CustomerID); // Assuming CustomerID is the primary key

            modelBuilder.Entity<User>()
                .HasOne(u => u.Customer)
                .WithOne(c => c.User)
                .HasForeignKey<User>(u => u.Id); // Assuming Id is the primary key of User (IdentityUser<int>)

        }

    }
}