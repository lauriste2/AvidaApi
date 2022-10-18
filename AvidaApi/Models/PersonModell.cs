using System.ComponentModel.DataAnnotations;

namespace AvidaApi.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        [Required]
        public string PersonalNumber { get; set; } = string.Empty;
        [Required]
        public string? FirstName { get; set; } = string.Empty;
        [Required]
        public string? LastName { get; set; } = string.Empty;
        [Required]
        public double? MonthlyIncome { get; set; } = 0;
        [Required]
        public AdressModel? Adress { get; set; } = new AdressModel();
    }


}