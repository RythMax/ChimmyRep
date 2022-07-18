using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TomyChimmy.Areas.Identity.Data;

namespace TomyChimmy.Models
{
    public class Cart
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string CartId { get; set; }

        public int ID_Comidas { get; set; }

        [ForeignKey(nameof(ID_Comidas))]
        [InverseProperty("Cart")]
        public virtual Food Food { get; set; }

        public int Cantidad { get; set; }

        [Display(Name = "Precio de Carro")]
        [DataType(DataType.Currency)]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal PreciodeCarro { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }


        [ForeignKey("Queue")]
        public int Pedido_ID { get; set; }
        public Queue Queue { get; set; }

        /*[Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [Display(Name = "Clientes")]
        [ForeignKey("Client")]
        public int UserId { get; set; }
        public User User { get; set; }*/

    }
}
