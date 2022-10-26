using AvidaApi.Data;
using AvidaApi.Data.Migrations;
using AvidaApi.Models;
using AvidaApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace AvidaApiTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1Async()
        {
            // arrange 



            var options = new DbContextOptionsBuilder<LoanDBContext>()
            .UseInMemoryDatabase(databaseName: "Avida")
            .Options;

            AdressModel adressModel = new AdressModel()
            {
                Id = 1,
                Address = "Damastväen 7 2 trp",
                City = "Bromma",
                Country = "Sverige",
                PhoneNumber = "070-3673815",
                PostalCode = "168 74"
            };

            Indatavalidation indatavalidation = new Indatavalidation();
            var iLoggerMock = new Mock<ILogger<PersonService>>();


            var context = new LoanDBContext(options);

            context.Adress.Add(new AdressModel { Id = 1, Address = "Damastväen 7 ", City = "Bromma", PostalCode = "168 74", Country = "Sverige", PhoneNumber = "070-3673815" });
            context.SaveChanges();

            AddressService addressService = new(context, indatavalidation, iLoggerMock.Object);
            await addressService.UpdateAsync(adressModel);

            Assert.AreEqual(1, 1);

            //context.Setup(c => c.SaveChangesAsync()).Verifiable();.
           
           

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