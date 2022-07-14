using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomyChimmy.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [Display(Name = "Artículo")]
        [ForeignKey("Food")]
        public int ID_Comidas { get; set; }
        public Food Food { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [Display(Name = "Cantidad de Artículos")]
        [DataType(DataType.Currency)]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal CantidadDeArticulos { get; set; }

        [Display(Name = "Valor unitario")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal ValorUnitario { get; set; }

        [Display(Name = "Valor total")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [Display(Name = "Orden")]
        [ForeignKey("Order")]
        public int ID_Orden { get; set; }
        public Order Order { get; set; }
    }
}
