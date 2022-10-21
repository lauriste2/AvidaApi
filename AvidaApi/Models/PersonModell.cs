using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvidaApi.Models
{
    public class PersonModel
    {
        [Key, ForeignKey("LoanApplicationModel")]
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

        [ForeignKey("Adress")]
        public int AdressID { get; internal set; }
        public AdressModel? Adress { get; set; } = new AdressModel();

    }


}