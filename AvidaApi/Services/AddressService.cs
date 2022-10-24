using AvidaApi.Data;
using AvidaApi.Models;

namespace AvidaApi.Services
{
    public class AddressService : IAddressService
    {
        private LoanDBContext _Context;
        private IIndatavalidation _indatavalidation;
        private readonly ILogger<PersonService> _logger;

        public AddressService(LoanDBContext Context, IIndatavalidation indatavalidation, ILogger<PersonService> logger)
        {
            _logger = logger;
            _Context = Context;
            _indatavalidation = indatavalidation;
        }
        public async Task UpdateAsync(AdressModel adress)
        {
            string errrorMessage = _indatavalidation.ValidateAdress(adress);

            AdressModel? adressToupdate = await _Context.Adress.FindAsync(adress.Id);

            if (adressToupdate != null && adress != null && errrorMessage.Length == 0)
            {
                bool uppdaterad = false;

                if (adressToupdate.Country != adress.Country)
                {
                    uppdaterad = true;
                    adressToupdate.Country = adress.Country;
                }

                if (adressToupdate.City != adress.City)
                {
                    uppdaterad = true;
                    adressToupdate.City = adress.City;
                }

                if (adressToupdate.PhoneNumber != adress.PhoneNumber)
                {
                    uppdaterad = true;
                    adressToupdate.PhoneNumber = adress.PhoneNumber;
                }

                if (adressToupdate.Address != adress.Address)
                {
                    uppdaterad = true;
                    adressToupdate.Address = adress.Address;
                }

                if (adressToupdate.PostalCode != adress.PostalCode)
                {
                    uppdaterad = true;
                    adressToupdate.PostalCode = adress.PostalCode;
                }


                try
                {
                    if (uppdaterad)
                    {
                        await _Context.SaveChangesAsync();
                        _logger.LogInformation("adress med id är uppdaterad" + adress.Id);
                    }
                    else
                    {
                        _logger.LogInformation("Inget ändrat för address " + adress.Id);
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Nått gick fel när en Adress med id" + adress.Id + " Skulle sparas ");
                }
            }

            else
            {
                throw new InvalidOperationException("Hitta ingen adress att uppdatera");
            }


        }

    }
}

