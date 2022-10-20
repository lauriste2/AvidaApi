using System.Text;
using AvidaApi.Models;

namespace AvidaApi.Services
{
    public interface IIndatavalidation
    {

        string ValidateLoanApplication(LoanApplicationModel Validate);
        //string ValidatePerson(PersonModel person);
        //string ValidateAdress(AdressModel adress);

        //string ValidateLoan(LoanModel loan);
    }
}