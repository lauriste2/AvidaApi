using AvidaApi.Data;
using AvidaApi.Models;

namespace AvidaApi.Services
{
    public class PersonService : IPersonService
    {
        private LoanDBContext _Context;
        private readonly IIndatavalidation _indatavalidation;
        private readonly ILogger<PersonService> _logger;
        public PersonService(LoanDBContext Context, IIndatavalidation indatavalidation ,ILogger<PersonService> logger)
        {
            _Context = Context;
            _indatavalidation = indatavalidation;
            _logger = logger;
        }
        public async Task UpdateAsync(PersonModel person)
        {
            PersonModel? personToUpdate = await _Context.Person.FindAsync(person.Id);

            string errrorMessage = _indatavalidation.ValidatePerson(person);

            if (personToUpdate != null && errrorMessage.Length == 0)
            {

                personToUpdate.PersonalNumber = person.PersonalNumber;
                personToUpdate.FirstName = person.FirstName;
                personToUpdate.MonthlyIncome = person.MonthlyIncome;
                personToUpdate.LastName = person.LastName;

                try
                {
                    await _Context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Nått gick fel när en person med id" + person.Id + " Skulle sparas ");
                }
            }
        }
    }
}
