using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlbelliWebApi.Data.Entities
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string OrderId { get; set; }
        [Required]
        public int ProductType { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
