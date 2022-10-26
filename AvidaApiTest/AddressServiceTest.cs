using AvidaApi.Data;
using AvidaApi.Models;
using AvidaApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace AvidaApiTest
{
    public class AddressServiceTest
    {
        private const string DatabaseName = "Avida";
        private DbContextOptions<LoanDBContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<LoanDBContext>()
           .UseInMemoryDatabase(databaseName: DatabaseName)
           .Options;
        }

        [Test]
        public async Task UpdateAsync_Log_Information_When_Adress_Is_Diifrent_inDatabase_and_inparameter()
        {
            // Arrange 

            AdressModel adressModel = new()
            {
                Id = 1,
                Address = "Damastväen 7 2 trp",
                City = "Bromma",
                Country = "Sverige",
                PhoneNumber = "070-3673815",
                PostalCode = "168 74"
            };

            var indatavalidation = new Indatavalidation();
            var iLoggerMock = new Mock<ILogger<PersonService>>();

            var context = new LoanDBContext(_options);

            context.Adress.Add(new AdressModel { Id = 1, Address = "Damastväen 7 ", City = "Bromma", PostalCode = "168 74", Country = "Sverige", PhoneNumber = "070-3673815" });
            context.SaveChanges();
            var addressService = new AddressService(context, indatavalidation, iLoggerMock.Object);


            // Act
            await addressService.UpdateAsync(adressModel);

            //Assert


            // TODO 
            // Assert _Context.SaveChangesAsync()
            // Se https://github.com/romantitov/MockQueryable  for help 

            iLoggerMock.Verify(logger => logger.Log(
        It.Is<LogLevel>(logLevel => logLevel == LogLevel.Information),
        It.Is<EventId>(eventId => eventId.Id == 0),
        It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "Adress med id är uppdaterad 1" && @type.Name == "FormattedLogValues"),
        It.IsAny<Exception>(),
        It.IsAny<Func<It.IsAnyType, Exception, string>>()),
    Times.Once);

           
        }


    }
}