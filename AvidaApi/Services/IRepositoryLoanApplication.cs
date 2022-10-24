using AvidaApi.Models;

namespace AvidaApi.Services
{
    public interface IRepositoryLoanApplication
    {
        Task<LoanApplicationModel> Create(LoanApplicationModel loanApplication);
        void Delete(LoanApplicationModel loanApplication);
        Task<LoanApplicationModel> GetById(int Id);
        Task Update(LoanApplicationModel loanApplicationModel);

        Task<LoanApplicationModel> PupulateLoanApplicationModel(int id);
    }
}