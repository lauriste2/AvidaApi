using AvidaApi.Models;

namespace AvidaApi.Services
{
    public class DecisionRulesService : IDecisionRulesService
    {

        public void MakeDecision(LoanApplicationModel loanApplication)
        {
            var person = loanApplication.Person;
            var loan = loanApplication.Loan;

            if (person.MonthlyIncome > 31000 && loan.LoanAmount > 0)
            {
                loanApplication.Decision = true;
            }
            else
            {
                loanApplication.Decision = false;
            }

        }
    }
}
