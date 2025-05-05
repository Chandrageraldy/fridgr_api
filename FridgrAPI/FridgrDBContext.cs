using FridgrAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FridgrAPI
{
    public class FridgrDBContext : DbContext
    {
        public FridgrDBContext() { }

        public FridgrDBContext(DbContextOptions<FridgrDBContext> options) : base(options) { }

        public virtual DbSet<Space> Space { get; set; }
        public virtual DbSet<FoodItem> FoodItem { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Grocery> Grocery { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=fridgrdbinstance.ce0wymt4lyde.us-east-1.rds.amazonaws.com;Initial Catalog=fridgrdb;User ID=admin;Password=Abc123123!;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Space>(entity => {
                entity.ToTable("Space");

                entity.HasKey(e => e.SpaceId);
                entity.Property(e => e.SpaceId).HasColumnName("spaceId").ValueGeneratedOnAdd();
                entity.Property(e => e.SpaceName).HasColumnName("spaceName").HasMaxLength(50);
                entity.Property(e => e.SpaceNote).HasColumnName("spaceNote").HasMaxLength(200);
                entity.Property(e => e.SpaceImageUrl).HasColumnName("spaceImageUrl").HasMaxLength(500);
                entity.Property(e => e.UserId).HasColumnName("userId");
            });


            modelBuilder.Entity<FoodItem>(entity => {
                entity.ToTable("FoodItem");

                entity.HasKey(e => e.FoodItemId);
                entity.Property(e => e.FoodItemId).HasColumnName("foodItemId").ValueGeneratedOnAdd();
                entity.Property(e => e.FoodItemName).HasColumnName("foodItemName").HasMaxLength(50);
                entity.Property(e => e.FoodItemNote).HasColumnName("foodItemNote").HasMaxLength(200);
                entity.Property(e => e.FoodItemQuantity).HasColumnName("foodItemQuantity");
                entity.Property(e => e.FoodItemUnit).HasColumnName("foodItemUnit").HasMaxLength(50);
                entity.Property(e => e.FoodItemImageUrl).HasColumnName("foodItemImageUrl").HasMaxLength(500);
                entity.Property(e => e.ExpiryDate).HasColumnName("expiryDate");
                entity.Property(e => e.AddedDate).HasColumnName("addedDate");
                entity.Property(e => e.SpaceId).HasColumnName("spaceId");
            });

            modelBuilder.Entity<User>(entity => {
                entity.ToTable("User");

                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasColumnName("userId");
                entity.Property(e => e.Username).HasColumnName("username").HasMaxLength(50);
                entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(50);
            });

            modelBuilder.Entity<Grocery>(entity => {
                entity.ToTable("Grocery");

                entity.HasKey(e => e.GroceryId);
                entity.Property(e => e.GroceryId).HasColumnName("groceryId");
                entity.Property(e => e.GroceryName).HasColumnName("groceryName").HasMaxLength(50);
                entity.Property(e => e.GroceryQuantity).HasColumnName("groceryQuantity").HasMaxLength(50);
                entity.Property(e => e.GroceryUnit).HasColumnName("groceryUnit").HasMaxLength(50);
                entity.Property(e => e.UserId).HasColumnName("userId");
            });
        }
    }
}
