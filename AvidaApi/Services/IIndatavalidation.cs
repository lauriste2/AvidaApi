using AvidaApi.Models;

namespace AvidaApi.Services
{
    public interface IIndatavalidation
    {
        bool ValidatePerson(PersonModel person);
    }
}