using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomyChimmy.Models
{
    public class PayingMethod
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Method_Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [MaxLength(10, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Forma de Pago")]
        public string FormaDePago { get; set; }
    }
}
