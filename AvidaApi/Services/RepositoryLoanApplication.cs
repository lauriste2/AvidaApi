using AvidaApi.Data;
using AvidaApi.Models;

namespace AvidaApi.Services
{
    public class RepositoryLoanApplication : IRepositoryLoanApplication
    {
        private LoanDBContext _Context;
        private IDecisionRulesService _loanService;
        private IIndatavalidation _indatavalidation;
        private ILogger<PersonService> _logger;

        public RepositoryLoanApplication(LoanDBContext Context, IDecisionRulesService loanService, IIndatavalidation indatavalidation, ILogger<PersonService> logger)
        {
            _Context = Context;
            _loanService = loanService;
            _indatavalidation = indatavalidation;
            _logger = logger;
        }
        public async Task<LoanApplicationModel> Create(LoanApplicationModel loanApplication)
        {
            string errorMessage = _indatavalidation.ValidateLoanApplication(loanApplication);

            if (errorMessage.Length <= 0)
            {
                _loanService.MakeDecision(loanApplication);
                var obj = await _Context.LoanApplication.AddAsync(loanApplication);
                await _Context.SaveChangesAsync();

                return obj.Entity;
            }

            else
            {
                return loanApplication;
            }
        }

        public void Delete(LoanApplicationModel loanApplication)
        {
            _Context.Remove(loanApplication);
            _Context.SaveChanges();
        }

        public async Task<LoanApplicationModel> GetById(int Id)
        {
            LoanApplicationModel? loanApplication = await PupulateLoanApplicationModel(Id);
            return loanApplication;
        }


        public async Task Update(LoanApplicationModel loanApplicationModel)
        {
            LoanApplicationModel? LoanApplicationModelToupdate = await _Context.LoanApplication.FindAsync(loanApplicationModel.Id);

         
            if(LoanApplicationModelToupdate == null)
            {
                _logger.LogInformation("Hitta ingen Låneansökan att uppdatera");
                throw new InvalidOperationException("Hitta ingen Låneansökan att uppdatera");
            }
            _loanService.MakeDecision(loanApplicationModel);

            if (loanApplicationModel.Decision != loanApplicationModel.Decision)
            {
                _logger.LogInformation("Låneansökan med id " + loanApplicationModel.Id + "Har fått ett ny beslut");

                LoanApplicationModelToupdate.Decision = loanApplicationModel.Decision;
                _Context.LoanApplication.Update(LoanApplicationModelToupdate);
                _Context.SaveChanges();
            }


        }

        private async Task<LoanApplicationModel> PupulateLoanApplicationModel(int id)
        {
            LoanApplicationModel? loanApplication = await _Context.LoanApplication.FindAsync(id);

            if (loanApplication != null)
            {
                PersonModel? person = await _Context.Person.FindAsync(loanApplication.PersonId);

                if (person != null)
                {

                    var adress = await _Context.Adress.FindAsync(person.AdressID);
                    loanApplication.Person = person;

                    if (adress != null)

                        loanApplication.Person.Adress = adress;
                }

                LoanModel? loan = await _Context.LoanModel.FindAsync(loanApplication.LoanID);

                if (loan != null)
                {
                    loanApplication.Loan = loan;
                }
            }

            return loanApplication;
        }
    }
}

