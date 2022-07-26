using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomyChimmyAPI.Models
{
    public class FoodType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int FoodType_ID { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [MaxLength(15, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Detalle { get; set; }
    }
}
