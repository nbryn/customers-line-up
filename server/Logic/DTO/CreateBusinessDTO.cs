using System.ComponentModel.DataAnnotations;

using Logic.BusinessOwners;

namespace Logic.DTO
{
    public class CreateBusinessDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Zip { get; set; }

        public BusinessOwner Owner {get; set;}

        [Required]
        public double OpeningTime { get; set; }

        [Required]
        public double ClosingTime { get; set; }

        [Required]
        public int Capacity {get; set;}

        [Required]
        public string Type {get; set;}

    }
}