using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Exsm3944_MySqlAuthentication.Models
{
    [Table("vehicle")]
    public class Vehicle
    {
        //Primary Key
        [Key]
        // Auto incrementing
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // Name and type
        [Column("id", TypeName = "int(10)")]
        public int ID { get; set; }

        [Required]
        [Column("customer_email", TypeName = "varchar(128)")]
        public string CustomerEmail { get; set; }

        [Required]
        [Column("vin", TypeName = "varchar(18)")]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "VIN must be exactly 17 characters long.")]
        [RegularExpression(@"^[A-Z0-9]{3}(?:List)?$", ErrorMessage = "Only Capitols and numbers allowed.")]
        public string VIN { get; set; }

        [Required]
        [Column("model_year", TypeName = "int(4)")]
        [Range(1900, 2050, ErrorMessage = "Model Year must be between 1900 and 2050")]
        public int ModelYear { get; set; }

        [Required]
        [Column("colour", TypeName = "varchar(18)")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Colour must be between 3 and 50 characters long.")]
        public string Colour { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [AllowNull]
        public DateTime? SaleDate { get; set; }

        [Column("model_id", TypeName = "int(10)")]
        // Cannot be Null
        [Required]
        public int ModelID { get; set; }

        // Point to the connected property
        [ForeignKey(nameof(ModelID))]
        [InverseProperty(nameof(Models.VehicleModel.Vehicles))]
        public virtual VehicleModel VehicleModel { get; set; }
    }
}
