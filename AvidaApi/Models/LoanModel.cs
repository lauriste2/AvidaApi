using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AvidaApi.Models
{
    public class LoanModel
    {

       


        [ForeignKey("LoanApplicationModel")]
        [Required]
        public int Id { get; set; } 
        [Required]
        public double LoanAmount { get; set; }
        [Required]
        public string CurrencyCode { get; set; } = "SEK";
        [Required]
        public DateTime? LoanDuration { get; set; }

    }

}
