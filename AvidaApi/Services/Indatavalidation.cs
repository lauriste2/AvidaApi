using AvidaApi.Models;

namespace AvidaApi.Services
{
    public class Indatavalidation : IIndatavalidation
    {

        public bool ValidatePerson(PersonModel person)
        {
            if (person.FirstName == "string" || person.LastName == "string" || person.PersonalNumber == "string" || person.Adress == null)
                return false;

            return true;
        }
    }
}
