using AvidaApi.Data;
using AvidaApi.Models;

namespace AvidaApi.Services
{
    public class PersonService : IPersonService
    {
        private LoanDBContext _Context;
        private readonly IIndatavalidation _indatavalidation;
        private readonly ILogger<PersonService> _logger;
        public PersonService(LoanDBContext Context, IIndatavalidation indatavalidation, ILogger<PersonService> logger)
        {
            _Context = Context;
            _indatavalidation = indatavalidation;
            _logger = logger;
        }
        public async Task UpdateAsync(PersonModel person)
        {
            PersonModel? personToUpdate = await _Context.Person.FindAsync(person.Id);

            string errrorMessage = _indatavalidation.ValidatePerson(person);

            if (errrorMessage.Length > 0)
            {
                _logger.LogError("Nått ogiltigt person med id" + person.Id + " när den skulle sparas " + errrorMessage);
            }

            else if (personToUpdate != null && errrorMessage.Length == 0)
            {

                bool uppdaterad = false;

                if (personToUpdate.PersonalNumber != person.PersonalNumber)
                {
                    uppdaterad = true;
                    personToUpdate.PersonalNumber = person.PersonalNumber;
                }

                if (personToUpdate.FirstName != person.FirstName)
                {
                    uppdaterad = true;
                    personToUpdate.FirstName = person.FirstName;
                }

                if (personToUpdate.LastName != person.LastName)
                {
                    uppdaterad = true;
                    personToUpdate.LastName = person.LastName;
                }

                if (personToUpdate.MonthlyIncome != person.MonthlyIncome)
                {
                    uppdaterad = true;
                    personToUpdate.MonthlyIncome = person.MonthlyIncome;
                }

                try
                {
                    if (uppdaterad)
                    {
                        await _Context.SaveChangesAsync();

                        _logger.LogInformation("Person med " + person.Id + "är uppdaterad");
                    }



                    personToUpdate.PersonalNumber = person.PersonalNumber;
                    personToUpdate.FirstName = person.FirstName;
                    personToUpdate.MonthlyIncome = person.MonthlyIncome;
                    personToUpdate.LastName = person.LastName;


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
