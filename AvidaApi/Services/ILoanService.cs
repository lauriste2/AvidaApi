using AvidaApi.Models;

namespace AvidaApi.Services
{
    public interface IDecisionRulesService
    {
        void MakeDecision(LoanApplicationModel loanApplication);
    }
}