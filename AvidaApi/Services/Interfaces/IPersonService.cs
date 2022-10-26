using AvidaApi.Models;

namespace AvidaApi.Services
{
    public interface IPersonService
    {
        Task UpdateAsync(PersonModel person);
    }
}