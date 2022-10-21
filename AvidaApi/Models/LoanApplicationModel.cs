using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvidaApi.Models
{
    public class LoanApplicationModel
    {
        [Key]
        public int Id { get; set; }
        [Required]

  
        public PersonModel Person { get; set; } = new PersonModel();
        [Required]
        public LoanModel Loan { get; set; } = new LoanModel();
        [Required]
        public bool? Decision { get; set; }

        [ForeignKey("Person")]
        public int PersonId { get; internal set; }

        [ForeignKey("Loan")]
        public int LoanID { get; internal set; }
    }
}
