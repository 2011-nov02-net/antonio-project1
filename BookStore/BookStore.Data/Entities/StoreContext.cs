using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BookStore.Data.Entities
{
    public partial class StoreContext : DbContext
    {
        public StoreContext()
        {
        }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookEntity> Books { get; set; }
        public virtual DbSet<CartitemEntity> Cartitems { get; set; }
        public virtual DbSet<CustomerEntity> Customers { get; set; }
        public virtual DbSet<InventoryEntity> Inventories { get; set; }
        public virtual DbSet<LocationEntity> Locations { get; set; }
        public virtual DbSet<OrderEntity> Orders { get; set; }
        public virtual DbSet<OrderlineEntity> Orderlines { get; set; }
        public virtual DbSet<ShoppingcartEntity> Shoppingcarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEntity>(entity =>
            {
                entity.HasKey(e => e.Isbn)
                    .HasName("PK__book__99F9D0A5ADB0C00B");

                entity.ToTable("book");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(15)
                    .HasColumnName("isbn");

                entity.Property(e => e.AuthorFirstName)
                    .HasMaxLength(50)
                    .HasColumnName("author_first_name");

                entity.Property(e => e.AuthorLastName)
                    .HasMaxLength(50)
                    .HasColumnName("author_last_name");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(19, 4)")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<CartitemEntity>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__cartitem__52020FDD1FD3311B");

                entity.ToTable("cartitem");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.BookIsbn)
                    .HasMaxLength(15)
                    .HasColumnName("book_isbn");

                entity.Property(e => e.DataAdded)
                    .HasColumnType("datetime")
                    .HasColumnName("data_added")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.ShoppingcartId).HasColumnName("shoppingcart_id");

                entity.HasOne(d => d.BookIsbnNavigation)
                    .WithMany(p => p.Cartitems)
                    .HasForeignKey(d => d.BookIsbn)
                    .HasConstraintName("FK__cartitem__book_i__60924D76");

                entity.HasOne(d => d.Shoppingcart)
                    .WithMany(p => p.Cartitems)
                    .HasForeignKey(d => d.ShoppingcartId)
                    .HasConstraintName("FK__cartitem__shoppi__627A95E8");
            });

            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.ToTable("customer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.LocationId)
                    .HasColumnName("location_id")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__customer__locati__4AA30C57");
            });

            modelBuilder.Entity<InventoryEntity>(entity =>
            {
                entity.HasKey(e => new { e.LocationId, e.BookIsbn })
                    .HasName("PK__inventor__939A7875FD390A1B");

                entity.ToTable("inventory");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.BookIsbn)
                    .HasMaxLength(15)
                    .HasColumnName("book_isbn");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.BookIsbnNavigation)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.BookIsbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__inventory__book___515009E6");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__inventory__locat__505BE5AD");
            });

            modelBuilder.Entity<LocationEntity>(entity =>
            {
                entity.ToTable("location");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orders__customer__542C7691");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orders__location__55209ACA");
            });

            modelBuilder.Entity<OrderlineEntity>(entity =>
            {
                entity.ToTable("orderline");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookIsbn)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("book_isbn");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(19, 4)")
                    .HasColumnName("total");

                entity.HasOne(d => d.BookIsbnNavigation)
                    .WithMany(p => p.Orderlines)
                    .HasForeignKey(d => d.BookIsbn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orderline__book___59E54FE7");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderlines)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__orderline__order__58F12BAE");
            });

            modelBuilder.Entity<ShoppingcartEntity>(entity =>
            {
                entity.HasKey(e => e.CartId)
                    .HasName("PK__shopping__2EF52A275C93A451");

                entity.ToTable("shoppingcart");

                entity.Property(e => e.CartId).HasColumnName("cart_id");

                entity.Property(e => e.CreateData)
                    .HasColumnType("datetime")
                    .HasColumnName("create_data")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Shoppingcarts)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__shoppingc__custo__5CC1BC92");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
