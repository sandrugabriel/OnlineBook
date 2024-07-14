using Microsoft.EntityFrameworkCore;
using OnlineBookShop.Books.Models;
using OnlineBookShop.Categories.Models;
using OnlineBookShop.Customers.Models;
using OnlineBookShop.Loans.Models;

namespace OnlineBookShop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");
                entity.Property(s => s.Email).IsRequired().HasMaxLength(256);
                entity.Property(s => s.NormalizedEmail).HasMaxLength(256);
                entity.Property(s => s.UserName).IsRequired().HasMaxLength(256);
                entity.Property(s => s.NormalizedUserName).HasMaxLength(256);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.PhoneNumber).IsRequired().HasMaxLength(256);

                entity.HasDiscriminator<string>("Discriminator").HasValue("Customer");

            });

            modelBuilder.Entity<Book>()
                .HasOne(a => a.Category)
                .WithMany(a => a.Books)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Loan>()
            .HasOne(a => a.Customer)
            .WithMany(a => a.Loans)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Loan>()
            .HasOne(a => a.Book)
            .WithMany(a => a.Loans)
            .HasForeignKey(a => a.BookId)
            .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
