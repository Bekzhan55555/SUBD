using System.ComponentModel.DataAnnotations;

namespace SUBD.Models
{
    public class Units
    {
        [Key] public int Id { get; set; }
        
        public string Name { get; set; }


    }
}
