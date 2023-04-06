using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Exsm3944_MySqlAuthentication.Models;

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
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }

        public virtual DbSet<VehicleModel> VehicleModels { get; set; }

        public virtual DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=exsm3944_Vehicles",
                    new MySqlServerVersion(new Version(10, 4, 27)));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                //Setup instructions for the Manufacturer
                entity
                    .Property(model => model.Name)
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.CustomerEmail)
                    .IsRequired()
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

            });

            modelBuilder.Entity<VehicleModel>(entity =>
            {
                entity
                    .HasIndex(model => model.ManufacturerID)
                    .HasName($"FK_{nameof(VehicleModel)}_{nameof(Manufacturer)}");

                entity
                    .Property(model => model.Name)
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.CustomerEmail)
                    .IsRequired()
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity
                    .HasOne(x => x.Manufacturer)
                    .WithMany(y => y.VehicleModels)
                    .HasForeignKey(x => x.ManufacturerID)
                    .HasConstraintName($"FK_{nameof(VehicleModel)}_{nameof(Manufacturer)}")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity
                    .HasIndex(model => model.ModelID)
                    .HasName($"FK_{nameof(Vehicle)}_{nameof(VehicleModel)}");


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

                entity.Property(e => e.Colour)
                    .IsRequired()
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PurchaseDate)
                    .IsRequired();

                entity.Property(e => e.SaleDate);

                entity
                    .HasOne(x => x.VehicleModel)
                    .WithMany(y => y.Vehicles)
                    .HasForeignKey(x => x.ModelID).HasConstraintName($"FK_{nameof(Vehicle)}_{nameof(VehicleModel)}")
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
