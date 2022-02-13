using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExist()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "Bilbo";
            command.LastName = "Baggins";
            command.Document = "99999999999";
            command.Email = "bilbo.baggins@gmail.com";
            command.BarCode = "12";
            command.BoletoNumber = "1212137";
            command.PaymentNumber = "123121";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "Bilbo Baggins";
            command.PayerDocument = "12345678911";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "shire@dotcom.com";
            command.Street = "Baggin's street";
            command.Number = "77";
            command.Neighborhood = "Center";
            command.City = "Shire";
            command.State = "Eriador";
            command.Country = "Arnor";
            command.ZipCode = "12345678";

            handler.Handle(command);
            Assert.AreEqual(false, handler.IsValid);
        }
    }
}