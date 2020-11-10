using System.ComponentModel.DataAnnotations;

using Logic.BusinessOwners;

namespace Logic.Businesses
{
    public class Business
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string OwnerEmail { get; set; }

        [Required]
        public BusinessOwner Owner { get; set; }

        [Required]
        public string Zip { get; set; }
    }
}