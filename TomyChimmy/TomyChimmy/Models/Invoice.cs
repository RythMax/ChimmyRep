using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TomyChimmy.Areas.Identity.Data;

namespace TomyChimmy.Models
{
    public class Invoice
    {
        [Key]
        public int Invoice_ID { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [Display(Name = "Clientes")]
        [ForeignKey("Client")]
        public int Id { get; set; }
        public User User { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [Display(Name = "Forma de pago")]
        [ForeignKey("PayingMethod")]
        public int Method_Id { get; set; }
        public PayingMethod PayingMethod { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [Display(Name = "Fecha de Factura")]
        [DisplayFormat(DataFormatString = "0:MM/dd/yyyy")]
        [DataType(DataType.DateTime)]
        public DateTime FechaFactura { get; set; }

        [Display(Name = "Subtotal")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal Subtotal { get; set; }

        [Display(Name = "Valor de impuesto")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal ValorImpuesto { get; set; }

        [Display(Name = "Total")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal (18, 2)")]
        public decimal Total { get; set; }
    }
}
