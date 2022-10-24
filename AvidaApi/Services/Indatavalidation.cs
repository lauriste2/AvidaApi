using System.Text;
using AvidaApi.Models;

namespace AvidaApi.Services
{
    public class Indatavalidation : IIndatavalidation
    {
        StringBuilder errorMessageBuilder = new StringBuilder();

        public string ValidatePerson(PersonModel person)
        {
            string returnmessage = string.Empty;

           if(person == null)
            {
                errorMessageBuilder.Append("Personuppgigfter saknas");
                _ = errorMessageBuilder.Append("\n");

            }

           ValidateString(person.FirstName, "Förnamn är ej giltigt");
            ValidateString(person.LastName, "Efternamn är ej giltigt");
           ValidateString(person.PersonalNumber, "Personnummer är ej giltigt");

         
            if(errorMessageBuilder.Length > 0)
            {
                returnmessage = errorMessageBuilder.ToString();
            }

            return returnmessage;
        }

        public string ValidateAdress(AdressModel adress)
        {
            string returnmessage = string.Empty;

            if (adress == null)
            {
                errorMessageBuilder.Append("Adressuppgifter saknas");
                _ = errorMessageBuilder.Append("\n");

            }

            ValidateString(adress.Address, "Address är ej giltigt");
            ValidateString(adress.PhoneNumber, "Telefonnummer är ej giltigt");
            ValidateString(adress.Country, "Land är ej giltigt");
            ValidateString(adress.PostalCode, "Postnummer är ej giltigt");


            if (errorMessageBuilder.Length > 0)
            {
                returnmessage = errorMessageBuilder.ToString();
            }

            return returnmessage;
        }

        public void ValidateDouble(double amount, string errorMessage)
        {
            if (amount == 0)
            {
                errorMessageBuilder.Append(errorMessage);
                _ = errorMessageBuilder.Append("\n");
            }
        }

        public void ValidateDate(DateTime? date, string errorMessage)
        {
            
            if (date == null || date == DateTime.MinValue /*|| date < DateTime.Now.AddYears(10)*/)
            {
                errorMessageBuilder.Append(errorMessage);
                _ = errorMessageBuilder.Append("\n");
            }

        }

        public void ValidateString(string str,string errorMessage)
        {
            if(string.IsNullOrEmpty(str))
            {

               str = str.Replace("ej giltigt", "är obliagtotiriskt");
                errorMessageBuilder.Append(errorMessage);
                _ = errorMessageBuilder.Append("\n");
            }

            if (str == "string" )
            {
                errorMessageBuilder.Append(errorMessage);
                _ = errorMessageBuilder.Append("\n");
            }
              
        }

        public string ValidateLoan(LoanModel loan,bool? decision)
        {
            string returnmessage = string.Empty;

            if (loan == null)
            {
                errorMessageBuilder.Append("Låneuppgifter saknas");
                _ = errorMessageBuilder.Append("\n");

            }

            ValidateString(loan.CurrencyCode, "Valuta är ej giltigt");
            ValidateDouble(loan.LoanAmount, "Lånesumman  är ej giltigt");
            if (decision ==  true)
            ValidateDate(loan.LoanDuration, "Slutdatum är ej giltigt");


            if (errorMessageBuilder.Length > 0)
            {
                returnmessage = errorMessageBuilder.ToString();
            }

            return returnmessage;
        }

        public string ValidateLoanApplication(LoanApplicationModel loanApplication)
        {
            string returnmessage = string.Empty;
            ValidatePerson(loanApplication.Person);

            ValidateLoan(loanApplication.Loan, loanApplication.Decision);


            ValidateAdress(loanApplication.Person.Adress);

            if (errorMessageBuilder.Length > 0)
            {
                returnmessage = errorMessageBuilder.ToString();
            }

            return returnmessage;


        }
    }
}
