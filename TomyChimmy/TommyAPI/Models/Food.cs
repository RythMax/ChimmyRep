using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TommyAPI.Models
{
    public class Food
    {
        public Food()
        {
            Cart = new HashSet<Cart>();
            OrderDetail = new HashSet<OrderDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID_Comidas { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [MaxLength(40, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        [Display(Name = "Producto")]
        public string Descripción { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [Display(Name = "Tipo de comida")]
        [ForeignKey("FoodType")]
        public int FoodType_ID { get; set; }
        public FoodType FoodType { get; set; }

        [Required(ErrorMessage = "El campo {0} es un campo obligatorio")]
        [Display(Name = "Precio Unitario")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [Column(TypeName = "decimal (18, 2)")]
        [DataType(DataType.Currency)]
        public decimal PrecioUnitario { get; set; }

        [Display(Name = "Cantidad en inventario")]
        [Range(0, 999999999999999999.99, ErrorMessage = "Máximo 18 dígitos")]
        [Column(TypeName = "decimal (18, 2)")]
        [DataType(DataType.Currency)]
        public decimal Cantidad { get; set; }

        [InverseProperty("Food")]
        public virtual ICollection<Cart> Cart { get; set; }

        [InverseProperty("Food")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl)
            ? null
            : $"https://th.bing.com/th/id/OIP.G0_lZUzcgd1i0jbM3j1-8gHaHa?pid=ImgDet&rs=1{ImageUrl.Substring(1)}";
    }
}
