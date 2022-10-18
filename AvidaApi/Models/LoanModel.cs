using System.ComponentModel.DataAnnotations;

namespace AvidaApi.Models
{
    public class LoanModel
    {
        public int Id { get; set; } 
        [Required]
        public double LoanAmount { get; set; }
        [Required]
        public string CurrencyCode { get; set; } = "SEK";
        [Required]
        public DateTime LoanDuration { get; set; }

    }
}
