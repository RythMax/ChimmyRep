using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TomyChimmy.Models
{
    public class QueueDetail
    {
        [Key]
        public int QueueDetail_ID { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [Display(Name = "Artículos")]
        [ForeignKey("Food")]
        public int ID_Comidas { get; set; }
        public Food Food { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [Display(Name = "Cantidad")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [Column(TypeName = "decimal (18,2)")]
        public decimal Cantidad { get; set; }

        [Display(Name = "Valor Unitario")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal ValorUnitario { get; set; }

        [Display(Name = "Valor Total")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal ValorTotal { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [Display(Name = "Pedido")]
        [ForeignKey("Queue")]
        public int Pedido_ID { get; set; }
        public Queue Queue { get; set; }
    }
}
