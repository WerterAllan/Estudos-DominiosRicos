using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using FluentAssertions;
using System.Linq;

namespace PaymentContext.Tests
{
    [TestClass]
    public class DocumentTests : TestBase
    {
        [TestMethod]
        public void DeveRetornarUmErroQuandoOCNPJeInvalido()
        {
            var doc = new Document("123", EDocumentType.CNPJ);
            doc.Valid.Should().BeFalse();
        }

        [TestMethod]
        public void DeveRetornarSucessoQuandoCNPJEValido()
        {
            var doc = new Document("99412814000109", EDocumentType.CNPJ);
            doc.Valid.Should().BeTrue();
        }

        [TestMethod]
        public void DeveRetornarErroQuandoQuandoOCpfEInvalido()
        {
            var doc = new Document("123", EDocumentType.CPF);
            doc.Invalid.Should().BeTrue();
        }

        [TestMethod]        
        public void DeveRetonarSucessoQuandoOCpfEValido()
        {
            var doc = new Document("72377184014", EDocumentType.CPF);
            doc.Valid.Should().BeTrue(ExtractNotifications(doc.Notifications));
        }

        
    }
}
