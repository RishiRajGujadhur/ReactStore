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
        
        public DbSet<Receipt> Receipts { get; set; } 
        public DbSet<CollectionList> CollectionLists { get; set; }
        public DbSet<CollectionListItem> CollectionListItems { get; set; }
        public DbSet<OrderDiscount> OrderDiscounts { get; set; }
        public DbSet<AdditionalDeliveryInfo> AdditionalDeliveryInfos { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Like> Likes { get; set; } 
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(u => u.Id);

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

            modelBuilder.Entity<Order>()
                .HasMany(o => o.ReturnRequests)
                .WithOne(rr => rr.Order)
                .OnDelete(DeleteBehavior.Cascade); 

            // Collection and Product relationship
            modelBuilder.Entity<CollectionList>()
                .HasMany(w => w.Products)
                .WithMany(p => p.CollectionLists)
                .UsingEntity(j => j.ToTable("CollectionProducts"));

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

            modelBuilder.Entity<Like>()
             .HasKey(l => l.LikeId);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.User)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.Product)
                .WithMany(p => p.Likes)
                .HasForeignKey(l => l.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}