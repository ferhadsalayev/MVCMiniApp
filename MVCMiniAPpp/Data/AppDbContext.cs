using Microsoft.EntityFrameworkCore;
using MVCMiniAPpp.Models;

namespace MVCMiniApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Blog> Blogs => Set<Blog>();
        public DbSet<Basket> Baskets => Set<Basket>();
        public DbSet<BasketItem> BasketItems => Set<BasketItem>();
        public DbSet<Wishlist> Wishlists => Set<Wishlist>();
        public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();
        public DbSet<Setting> Settings => Set<Setting>();
        public DbSet<Slider> Sliders => Set<Slider>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Testimonial> Testimonials => Set<Testimonial>();
        public DbSet<ContactMessage> ContactMessages => Set<ContactMessage>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasIndex(user => user.Email).IsUnique();
                entity.Property(user => user.Email).IsRequired();
                entity.HasOne(user => user.Basket)
                    .WithOne(basket => basket.User)
                    .HasForeignKey<Basket>(basket => basket.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(user => user.Wishlist)
                    .WithOne(wishlist => wishlist.User)
                    .HasForeignKey<Wishlist>(wishlist => wishlist.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(category => category.Slug).IsUnique();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(product => product.Slug).IsUnique();
                entity.HasIndex(product => product.SKU).IsUnique();
                entity.Property(product => product.Price).HasPrecision(18, 2);
                entity.Property(product => product.DiscountPrice).HasPrecision(18, 2);
                entity.HasOne(product => product.Category)
                    .WithMany(category => category.Products)
                    .HasForeignKey(product => product.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasIndex(blog => blog.Slug).IsUnique();
                entity.HasOne(blog => blog.Category)
                    .WithMany(category => category.Blogs)
                    .HasForeignKey(blog => blog.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<BasketItem>(entity =>
            {
                entity.HasIndex(item => new { item.BasketId, item.ProductId }).IsUnique();
                entity.Property(item => item.UnitPrice).HasPrecision(18, 2);
                entity.HasOne(item => item.Basket)
                    .WithMany(basket => basket.Items)
                    .HasForeignKey(item => item.BasketId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(item => item.Product)
                    .WithMany(product => product.BasketItems)
                    .HasForeignKey(item => item.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<WishlistItem>(entity =>
            {
                entity.HasIndex(item => new { item.WishlistId, item.ProductId }).IsUnique();
                entity.HasOne(item => item.Wishlist)
                    .WithMany(wishlist => wishlist.Items)
                    .HasForeignKey(item => item.WishlistId)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(item => item.Product)
                    .WithMany(product => product.WishlistItems)
                    .HasForeignKey(item => item.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.HasIndex(setting => setting.Key).IsUnique();
            });

            modelBuilder.Entity<Slider>(entity =>
            {
                entity.HasIndex(slider => new { slider.IsActive, slider.DisplayOrder });
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasIndex(team => new { team.IsActive, team.DisplayOrder });
            });

            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.HasIndex(testimonial => new { testimonial.IsActive, testimonial.DisplayOrder });
            });

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                         .Where(type => typeof(BaseEntity).IsAssignableFrom(type.ClrType)))
            {
                var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "entity");
                var property = System.Linq.Expressions.Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                var condition = System.Linq.Expressions.Expression.Equal(
                    property,
                    System.Linq.Expressions.Expression.Constant(false));
                var filter = System.Linq.Expressions.Expression.Lambda(condition, parameter);
                entityType.SetQueryFilter(filter);
            }
        }
    }
}