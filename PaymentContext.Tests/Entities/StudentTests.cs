using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enuns;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        private readonly Name _name;
        private readonly Email _email;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Student _student;
        private readonly Subscription _subscription;

        public StudentTests()
        {
            _name = new Name("Ricardo", "Rodrigues");
            _document = new Document("01960720007", EDocumentType.CPF);
            _email = new Email("ricardo.caiuba@gmail.com");
            _address = new Address("Rua Augusto Sacratin", "181", "Vila Omar", "Americana", "SP", "Brasil", "13465987");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            PayPalPayment payment = new PayPalPayment("02352144-2214", DateTime.Now, DateTime.Now.AddDays(5), 10, "RR Santos", _document, 10, _address, _email);

            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            Subscription subscription = new Subscription(null);
            _student.AddSubscription(subscription);
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            Subscription subscription = new Subscription(null);
            PayPalPayment payment = new PayPalPayment("02352144-2214", DateTime.Now, DateTime.Now.AddDays(5), 10, "RR Santos", _document, 10, _address, _email);

            subscription.AddPayment(payment);
            _student.AddSubscription(subscription);

            Assert.IsTrue(_student.Valid);

        }

    }
}
