using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.FluentBuilder;
using PaymentContext.Domain.ValueObjects;
using FluentAssertions;
using PaymentContext.Domain.Entities;
using System;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentsTests : TestBase
    {
        [TestMethod]
        public void DeveRetornarErroQuandoOEstutanteJaTemUmaAssinaturaAtiva()
        {
            var student = new StudentBuilder()
                .Name("Andrea", "Marlene")
                .Document("55743813701", EDocumentType.CPF)
                .Email("andreasimonemarlenedamata@lctour.com.br")
                .Address("Avenida Getúlio Vargas", "226", "Bosque", "Rio Branco", "AC", "Brasil", "69900469")
                .Build();
            
            var subscription = new Subscription(null);
            var payment = new PayPalPaymentBuilder()
                .TransactionCode("12345678")
                .PaidDate(DateTime.Now)
                .ExpireDate(DateTime.Now.AddDays(5))
                .Total(10)
                .TotalPaid(10)
                .Address("Avenida Getúlio Vargas", "226", "Bosque", "Rio Branco", "AC", "Brasil", "69900469")
                .Payer("Empresa ABC")
                .Document("35111507795", EDocumentType.CPF)
                .Email("adc@empresa.com.br")
                .Build();

            subscription.AddPayment(payment);
            student.AddSubscriptions(subscription);
            student.AddSubscriptions(subscription);

            Console.WriteLine(ExtractNotifications(student.Notifications));
            student.Invalid.Should().BeTrue(ExtractNotifications(student.Notifications));

            
        }

        [TestMethod]
        public void TheDataOfStudantShoudBeTrue()
        {
            var student = new StudentBuilder()
                .Name("Werter", "Allan")
                .Document("25346920039", EDocumentType.CPF)
                .Email("werter@hotmail.com.br")
                .Build();

            student.Valid.Should().BeTrue();
        }

        [TestMethod]
        public void NomeDeveSerInvalido()
        {
            var name = new Name("teste", "teste");
            name.Valid.Should().BeTrue();
        }
    }
}
