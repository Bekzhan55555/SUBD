using System.ComponentModel.DataAnnotations;

namespace SUBD.Models
{
    public class FinishedProducts
    {
        [Key] public int Id { get; set; }
        
        public string Name { get; set; }
        public int UnitId { get; set; }
        public float Quantity { get; set; }
        public float TotalAmount { get; set; }

    }
}
