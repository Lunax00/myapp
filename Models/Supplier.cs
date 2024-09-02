using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public string Address { get; set; }
    }
}
