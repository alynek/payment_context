using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var cnpj = new Document("123", EDocumentType.CNPJ);
            Assert.IsTrue(!cnpj.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCNPJIsValid()
        {
            var cnpj = new Document("12343212343667", EDocumentType.CNPJ);
            Assert.IsTrue(cnpj.IsValid);
        }
        
        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var cpf = new Document("7676767", EDocumentType.CPF);
            Assert.IsTrue(!cpf.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCPFIsValid()
        {
            var cpf = new Document("76767670999", EDocumentType.CPF);
            Assert.IsTrue(cpf.IsValid);
        }
    }
}