using System.ComponentModel.DataAnnotations;

namespace Logic.Businesses.Models
{
    public class CreateBusinessDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Zip { get; set; }

    }
}