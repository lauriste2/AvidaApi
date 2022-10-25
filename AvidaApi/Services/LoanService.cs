using AvidaApi.Data;
using AvidaApi.Models;

namespace AvidaApi.Services
{
    public class LoanService : ILoanService
    {
        private LoanDBContext _Context;
        private IIndatavalidation _indatavalidation;
        private readonly ILogger<LoanService> _logger;

        public LoanService(LoanDBContext Context, IIndatavalidation indatavalidation, ILogger<LoanService> logger)
        {
            _logger = logger;
            _Context = Context;
            _indatavalidation = indatavalidation;
        }

        public DateTime GetLoanDecisionApprovedValue
        {
            get
            {
               return DateTime.Now.AddYears(10);
            }
        }

        public DateTime GetLoanDecisionDeniedValue
        {
            get
            {
                return DateTime.MinValue;
            }
        }

        public async Task UpdateAsync(LoanModel loan,bool? decision)
        {
            string errrorMessage = _indatavalidation.ValidateLoan(loan, decision);

            LoanModel? loanToupdate = await _Context.LoanModel.FindAsync(loan.Id);

            if (loanToupdate != null && loan != null && errrorMessage.Length == 0)
            {
                bool uppdaterad = false;

                if (loanToupdate.LoanAmount != loan.LoanAmount)
                {
                    uppdaterad = true;
                    loanToupdate.LoanAmount = loan.LoanAmount;
                }

                if (loanToupdate.CurrencyCode != loan.CurrencyCode)
                {
                    uppdaterad = true;
                    loanToupdate.CurrencyCode = loan.CurrencyCode;
                }

                if((bool)!decision || decision == null)
                {
                    loanToupdate.LoanDuration = GetLoanDecisionDeniedValue;
                }

                else if((bool)decision)
                {
                    loanToupdate.LoanDuration = GetLoanDecisionApprovedValue;
                }

                if (loanToupdate.LoanDuration != loan.LoanDuration)
                {
                    uppdaterad = true;
                }

                    if (uppdaterad)
                {
                    await _Context.SaveChangesAsync();
                    _logger.LogInformation("Lån med med id är uppdaterad" + loan.Id);
                }
                else
                    _logger.LogInformation("Inget ändrat för Lån " + loan.Id);
            }
        }
    }
}
