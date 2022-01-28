using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands
{
    [TestClass]
    public class CreatePaypalSubscriptionCommandTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var command = new CreatePaypalSubscriptionCommand();

            command.FirstName = "m";
            command.Validate();
            Assert.IsFalse(command.IsValid);
        }
    }
}