using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Exsm3944_MySqlAuthentication.Data.Models;

namespace Exsm3944_MySqlAuthentication.Data
{
    public class VehicleContext : DbContext
    {
        public VehicleContext()
        {

        }

        public VehicleContext(DbContextOptions<VehicleContext> options) : base(options) 
        { 

        }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=exsm3944_Vehicle",
                    new MySqlServerVersion(new Version(10, 4, 27)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.CustomerEmail)
                .IsRequired()
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");


                entity.Property(e => e.VIN)
                .IsRequired()
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.ModelYear)
                .IsRequired();

                entity.Property(e => e.Manufacturer)
                .IsRequired()
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Model)
                .IsRequired()
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Colour)
                .IsRequired()
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PurchaseDate)
                .IsRequired();

                entity.Property(e => e.SaleDate);
            });
        }
    }
}
