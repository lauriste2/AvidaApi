using System.ComponentModel.DataAnnotations;

namespace AvidaApi.Models
{
    public class LoanApplicationModel
    {
        
        public int Id { get; set; }
        [Required]
        public PersonModel Person { get; set; } = new PersonModel();
        [Required]
        public LoanModel Loan { get; set; } = new LoanModel();
        [Required]
        public bool? Decision { get; set; } 


    }
}
