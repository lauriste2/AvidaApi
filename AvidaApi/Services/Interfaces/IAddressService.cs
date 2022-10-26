using AvidaApi.Models;

namespace AvidaApi.Services
{
    public interface IAddressService
    {
        Task UpdateAsync(AdressModel adress);
    }
}