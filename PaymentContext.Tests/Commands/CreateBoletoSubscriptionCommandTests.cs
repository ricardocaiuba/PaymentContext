using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PaymentContext.Domain.Commands
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTests
    {
        // Red, Green, Refector
        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "";
            command.Validade();
            Assert.AreEqual(false, command.Valid);


        }
    }
}
