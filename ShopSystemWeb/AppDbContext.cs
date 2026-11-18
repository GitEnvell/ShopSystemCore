using System.IO;
using Microsoft.EntityFrameworkCore;

namespace ShopSystemWeb
{
    public class AppDbContext : DbContext
    {
        // Таблицы базы данных
        public DbSet<Product> Products { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<SupplyItem> SupplyItems { get; set; }
        public DbSet<DiscrepancyAct> Acts { get; set; }
        public DbSet<User> Users { get; set; }

        // Настройка подключения к SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Файл БД будет лежать в папке проекта
            string dbPath = Path.Combine(AppContext.BaseDirectory, "shop.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        // Настройка связей между таблицами
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Связь: Sale (1) → (*) SaleItem
            modelBuilder.Entity<SaleItem>()
                .HasOne<Sale>()
                .WithMany()
                .HasForeignKey("SaleId");

            // Связь: Product (1) → (*) SaleItem
            modelBuilder.Entity<SaleItem>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey("ProductId");

            // Связь: Supply (1) → (*) SupplyItem
            modelBuilder.Entity<SupplyItem>()
                .HasOne<Supply>()
                .WithMany()
                .HasForeignKey("SupplyId");

            // Связь: Product (1) → (*) SupplyItem
            modelBuilder.Entity<SupplyItem>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey("ProductId");

            // Связь: Manufacturer (1) → (*) Supply
            modelBuilder.Entity<Supply>()
                .HasOne<Manufacturer>()
                .WithMany()
                .HasForeignKey("ManufacturerId");
        }
    }
}
