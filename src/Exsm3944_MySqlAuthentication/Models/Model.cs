using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exsm3944_MySqlAuthentication.Models
{
    [Table("model")]
    public class Model
    {
        //Primary Key
        [Key]
        // Auto incrementing
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // Name and type
        [Column("id", TypeName = "int(10)")]
        public int ID { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(50)")]
        [StringLength(50, ErrorMessage = "Name must be less than or equal to 50 characters long.")]
        public string Name { get; set; }

        // Name and type
        [Column("manufacturer_id", TypeName = "int(10)")]
        // Cannot be Null
        [Required]
        public int ManufacturerID { get; set; }

        [InverseProperty(nameof(Vehicle))]
        public virtual ICollection<Vehicle> Vehicles { get; set; }

        // Point to the connected property
        [ForeignKey(nameof(ManufacturerID))]
        [InverseProperty(nameof(Models.Manufacturer.Models))]
        public virtual Manufacturer Manufacturer { get; set; }


    }
}
