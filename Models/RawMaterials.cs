using System.ComponentModel.DataAnnotations;

namespace SUBD.Models
{
    public class RawMaterials
    {
        [Key] public int Id { get; set; }
        
        public string Name { get; set; }
        public int UnitId { get; set; }

        public float Quantity { get; set; }
        public int TotalAmount { get; set; }

    }
}
