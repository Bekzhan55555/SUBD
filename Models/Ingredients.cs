using System.ComponentModel.DataAnnotations;

namespace SUBD.Models
{
    public class Ingredients
    {
        [Key] public int Id { get; set; }
        
        public int ProductId { get; set; }
        public int RawMaterialID { get; set; }
        public float Quantity { get; set; }
        public int Unit { get; set; }

    }
}
