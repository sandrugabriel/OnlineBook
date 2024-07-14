using FluentMigrator;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace OnlineBookShop.Data.Migrations
{
    [Migration(2)]
    public class CreateTabels : Migration
    {
        public override void Down()
        {
            Delete.Table("Loans");
            Delete.Table("Books");
            Delete.Table("Categories");
            Delete.Table("Customers");
        }

        private void createTabels()
        {
            Create.Table("Categories")
              .WithColumn("Id").AsInt32().PrimaryKey().Identity()
              .WithColumn("Name").AsString().NotNullable();

            Create.Table("Books")
             .WithColumn("Id").AsInt32().PrimaryKey().Identity()
             .WithColumn("Title").AsString().NotNullable()
             .WithColumn("Author").AsString().NotNullable()
             .WithColumn("CategoryId").AsInt32().NotNullable()
             .WithColumn("PublicationYear").AsInt32().NotNullable()
             .WithColumn("AvailableCopies").AsInt32().NotNullable();

            Create.Table("Loans")
             .WithColumn("Id").AsInt32().PrimaryKey().Identity()
             .WithColumn("CustomerId").AsInt32().NotNullable()
             .WithColumn("BookId").AsInt32().NotNullable()
             .WithColumn("BorrowDate").AsDateTime().NotNullable()
             .WithColumn("ReturnDate").AsDateTime().NotNullable();
        }

        public override void Up()
        {


            Create.Table("Customers")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserName").AsString(256).Nullable()
            .WithColumn("NormalizedUserName").AsString(256).Nullable()
            .WithColumn("Email").AsString(256).Nullable()
            .WithColumn("NormalizedEmail").AsString(256).Nullable()
            .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
            .WithColumn("PasswordHash").AsString().Nullable()
            .WithColumn("SecurityStamp").AsString().Nullable()
            .WithColumn("ConcurrencyStamp").AsString().Nullable()
            .WithColumn("PhoneNumber").AsString().Nullable()
            .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable()
            .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
            .WithColumn("LockoutEnd").AsDateTime().Nullable()
            .WithColumn("LockoutEnabled").AsBoolean().NotNullable()
            .WithColumn("AccessFailedCount").AsInt32().NotNullable()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Discriminator").AsString().NotNullable();

            Create.Table("AspNetRoles")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(256).Nullable()
                .WithColumn("NormalizedName").AsString().Nullable()
                .WithColumn("ConcurrencyStamp").AsInt32().Nullable();

            Create.Table("AspNetUserRoles")
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("RoleId").AsInt32().NotNullable()
                .ForeignKey("FK_AspNetUserRoles_Customers", "Customers", "Id")
                .ForeignKey("FK_AspNetUserRoles_AspNetRoles", "AspNetRoles", "Id");

            Create.Table("AspNetRolesClaims")
             .WithColumn("Id").AsInt32().PrimaryKey().Identity()
             .WithColumn("RoleId").AsInt32().NotNullable()
             .WithColumn("ClaimType").AsString(256).Nullable()
             .WithColumn("ClaimValue").AsInt32().Nullable()
             .ForeignKey("FK_AspNetRolesClaims_AspNetRoles", "AspNetRoles", "Id");

            Create.Table("AspNetUserClaims")
             .WithColumn("Id").AsInt32().PrimaryKey().Identity()
             .WithColumn("UserId").AsInt32().NotNullable()
             .WithColumn("ClaimType").AsString().Nullable()
             .WithColumn("ClaimValue").AsInt32().Nullable()
             .ForeignKey("FK_AspNetRolesClaims_Customers", "Customers", "Id");

            Create.Table("AspNetUserLogins")
           .WithColumn("LoginProvider").AsString(256).PrimaryKey()
           .WithColumn("ProviderKey").AsString(256).PrimaryKey()
           .WithColumn("ProviderDisplayName").AsString().Nullable()
           .WithColumn("UserId").AsInt32().NotNullable()
           .ForeignKey("FK_AspNetUserLogins_Customers", "Customers", "Id");


            Create.Table("AspNetUserTokens")
           .WithColumn("UserId").AsInt32().PrimaryKey()
           .WithColumn("LoginProvider").AsString(256).PrimaryKey()
           .WithColumn("Name").AsString(256).PrimaryKey()
           .WithColumn("Value").AsInt32().Nullable()
           .ForeignKey("FK_AspNetUserTokens_Customers", "Customers", "Id");


            createTabels();

          //  CreateCustomers();
            CreateBooks();
            CreatCategory();
            CreateLoans();

            CreateRoles();
            AddPermissionsToRoles();
        }

        private void CreateRoles()
        {
            Insert.IntoTable("AspNetRoles").Row(new { Name = "Admin", NormalizedName = "ADMIN" });
            Insert.IntoTable("AspNetRoles").Row(new { Name = "Editor", NormalizedName = "EDITOR" });
        }

        private void CreateLoans()
        {
            Insert.IntoTable("Loans").Row(new { CustomerId = 1, BookId = 2, BorrowDate = new DateTime(2024, 7, 1), ReturnDate = new DateTime(2024, 7, 15) });
            Insert.IntoTable("Loans").Row(new { CustomerId = 2, BookId = 1, BorrowDate = new DateTime(2024, 7, 5), ReturnDate = new DateTime(2024, 7, 20) });
            Insert.IntoTable("Loans").Row(new { CustomerId = 3, BookId = 3, BorrowDate = new DateTime(2024, 7, 10), ReturnDate = new DateTime(2024, 7, 25) });

        }

      /*  private void CreateCustomers()
        {
            Insert.IntoTable("Customers").Row(new { Name = "gabi", Email = "gabi@gmail.com", Password = "gabi1234", PhoneNumber = "07737777" });
            Insert.IntoTable("Customers").Row(new { Name = "filip", Email = "fil@gmail.com", Password = "filip1234", PhoneNumber = "07757777" });
            Insert.IntoTable("Customers").Row(new { Name = "ana", Email = "ana@gmail.com", Password = "ana1234", PhoneNumber = "07767777" });
            Insert.IntoTable("Customers").Row(new { Name = "david", Email = "d@gmail.com", Password = "davi1234", PhoneNumber = "07797777" });

        }
*/
  
        private void CreateBooks()
        {
            Insert.IntoTable("Books").Row(new { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", CategoryId = 1, PublicationYear = 1925, AvailableCopies = 3 });
            Insert.IntoTable("Books").Row(new { Title = "A Brief History of Time", Author = "Stephen Hawking", CategoryId = 2, PublicationYear = 1988, AvailableCopies = 5 });
            Insert.IntoTable("Books").Row(new { Title = "Sapiens: A Brief History of Humankind", Author = "Yuval Noah Harari", CategoryId = 3, PublicationYear = 2014, AvailableCopies = 4 });
        }

        public void CreatCategory()
        {
            Insert.IntoTable("Categories").Row(new { Name = "Fiction" });
            Insert.IntoTable("Categories").Row(new { Name = "Science" });
            Insert.IntoTable("Categories").Row(new { Name = "History" });
        }

        private void AddPermissionsToRoles()
        {
            Insert.IntoTable("AspNetRolesClaims").Row(new
            {
                RoleId = "1",
                ClaimType = "Permission",
                ClaimValue = "1"
            });

            Insert.IntoTable("AspNetRolesClaims").Row(new
            {
                RoleId = "2",
                ClaimType = "Permission",
                ClaimValue = "1"
            });
        }
    }
}
