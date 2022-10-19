using AvidaApi.Models;

namespace AvidaApi.Services
{
    public interface ILoanService
    {
        void MakeDecision(LoanApplicationModel loanApplication);
    }
}