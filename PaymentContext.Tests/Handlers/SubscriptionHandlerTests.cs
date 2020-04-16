using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enuns;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Moks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            SubscriptionHandler handler = new SubscriptionHandler(
                new FakeStudentRepository(),
                new FakeEmailService()
            );

            CreateBoletoSubscriptionCommand cmd = new CreateBoletoSubscriptionCommand()
            {
                FirstName = "Ricardo",
                LastName = "Rodrigues",
                Document = "99999999999",
                Email = "ricardo.caiuba@gmail.com",
                BarCode = "875897345898374",
                BoletoNumber = "324324234324",
                PaymentNumber = "1234567",
                PaidDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddMonths(1),
                Total = 1000.00m,
                TotalPaid = 1000.00m,
                Payer = "RR Santos",
                PayerDocument = "12345678910",
                PayerDocumentType = EDocumentType.CPF,
                PayerEmail = "rrsantos@rrsantos.com.br",
                Street = "Rua AAA",
                Number = "111",
                Neighborhood = "Vila Omar",
                City = "Americana",
                State = "SP",
                Country = "Brasil",
                ZipCode = "13450897",
            };
            handler.Handle(cmd);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}
