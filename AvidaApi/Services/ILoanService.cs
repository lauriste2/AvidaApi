using AvidaApi.Models;

namespace AvidaApi.Services
{
    public interface ILoanService
    {
        Task UpdateAsync(LoanModel loan, bool? decision);
    }
}