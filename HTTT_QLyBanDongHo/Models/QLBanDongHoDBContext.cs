namespace HTTT_QLyBanDongHo.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QLBanDongHoDBContext : DbContext
    {
        public QLBanDongHoDBContext()
            : base("name=QLBanDongHoDBContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<Manufacture> Manufactures { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.AccountID)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Customer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Emails)
                .WithMany(e => e.Customers)
                .Map(m => m.ToTable("Email_Customer").MapLeftKey("CustomerID").MapRightKey("EmailID"));

            modelBuilder.Entity<CustomerType>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.CustomerType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Email>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Manufacture>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Manufacture)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.CustomerID)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderStatus>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.OrderStatus)
                .HasForeignKey(e => e.OrderStatusID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PaymentType>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.PaymentType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Thumbnails)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Birthday)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.RoleID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.AccountID)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.OrderID)
                .IsUnicode(false);
        }
    }
}
