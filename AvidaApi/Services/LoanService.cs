using AvidaApi.Data;
using AvidaApi.Models;

namespace AvidaApi.Services
{
    public class LoanService : ILoanService
    {
        private LoanDBContext _Context;
        private IDecisionRulesService _decisionRulesService;
        private IIndatavalidation _indatavalidation;
        private readonly ILogger<LoanService> _logger;

        public LoanService(LoanDBContext Context, IIndatavalidation indatavalidation, IDecisionRulesService decisionRulesService, ILogger<LoanService> logger)
        {
            _logger = logger;
            _Context = Context;
            _decisionRulesService = decisionRulesService;
            _indatavalidation = indatavalidation;
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
                    loanToupdate.LoanDuration = DateTime.MinValue;
                }

                else if((bool)decision)
                {
                    loanToupdate.LoanDuration = DateTime.Now.AddYears(10);
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
