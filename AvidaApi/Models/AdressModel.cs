using System.ComponentModel.DataAnnotations;

namespace AvidaApi.Models
{
    public class AdressModel
    {
        public int Id { get; set; }
        [Required]
        public string Country { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string PostalCode { get; set; } = string.Empty;
    }
}
