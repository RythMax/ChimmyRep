using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TomyChimmy.Areas.Identity.Data;

namespace TomyChimmy.Models
{
    public class Queue
    {
        [Key]
        public int Pedido_ID { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [Display(Name = "Clientes")]
        [ForeignKey("Client")]
        public int Id { get; set; }
        public User User { get; set; }

        [Display(Name = "Fecha del pedido")]
        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd H:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DatePedido { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [Display(Name = "Forma de pago")]
        [ForeignKey("PayingMethod")]
        public int Method_Id { get; set; }
        public PayingMethod PayingMethod { get; set; }

        [DataType(DataType.MultilineText)]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Anotaciones { get; set; }

        [Display(Name = "Subtotal")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal Subtotal { get; set; }

        [Display(Name = "Valor de impuesto")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal Valor_Impuesto { get; set; }

        [Display(Name = "Total")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal Total { get; set; }


        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [Display(Name = "Estado del pedido")]
        [ForeignKey("Status")]
        public int Status_ID { get; set; }
        public Status Status { get; set; }
    }
}
