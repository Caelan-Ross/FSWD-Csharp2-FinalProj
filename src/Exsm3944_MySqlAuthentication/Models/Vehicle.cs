using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Exsm3944_MySqlAuthentication.Data.Models
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
        [StringLength(18)]
        public string VIN { get; set; }

        [Required]
        [Column("model_year", TypeName = "int(4)")]
        public int ModelYear { get; set; }

        [Required]
        [Column("manufacturer", TypeName = "varchar(18)")]
        public string Manufacturer { get; set; }

        [Required]
        [Column("model", TypeName = "varchar(18)")]
        public string Model { get; set; }

        [Required]
        [Column("colour", TypeName = "varchar(18)")]
        public string Colour { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [AllowNull]
        public DateTime? SaleDate { get; set; }
    }
}
