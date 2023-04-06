using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Exsm3944_MySqlAuthentication.Models
{
    [Table("manufacturer")]
    public class Manufacturer
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
        [Column("name", TypeName = "varchar(50)")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "VIN must be exactly 17 characters long.")]
        [RegularExpression(@"^[A-Za-z]+ -$", ErrorMessage = "Only Alphabetic characters, hyphens (-), and spaces allowed.")]
        public string Name { get; set; }

        // Point to the connected property
        [InverseProperty(nameof(VehicleModel.Manufacturer))]
        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
