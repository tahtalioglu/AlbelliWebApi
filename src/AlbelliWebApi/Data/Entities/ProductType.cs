using System.ComponentModel.DataAnnotations;

namespace AlbelliWebApi.Data.Entities
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double PackageWidth { get; set; }
        [Required]
        public int StackUpLimit { get; set; }
    }
}
