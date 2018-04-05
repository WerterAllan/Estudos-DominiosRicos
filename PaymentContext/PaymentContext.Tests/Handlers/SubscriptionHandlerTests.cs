using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests : TestBase
    {
        [TestMethod]
        public void DeveRetornarUmErroQuandoDocumentoJaExistir()
        {
            var studentRepository = Substitute.For<IStudentRepository>();
            var emailService = Substitute.For<IEmailService>();
            studentRepository.DocumentExists("99999999999").Returns(true);
            studentRepository.EmailExists(Email_Fake).Returns(true);
            var handler = new SubscriptionHandler(studentRepository, emailService);

            var command = new CreateBoletoSubscriptionCommand
            {
                FirstName = Faker.NameFaker.FirstName(),
                LastName = Faker.NameFaker.LastName(),
                Document = "99999999999",
                Email = Email_Fake,
                City = Faker.LocationFaker.City(),
                Street = Faker.LocationFaker.StreetName(),
            };

            handler.Handler(command);
            handler.Invalid.Should().BeTrue(ExtractNotifications(handler.Notifications));
            
        }

        [TestMethod]
        public void DeveRetornarSucessoParaOHanderValido()
        {
            var studentRepository = Substitute.For<IStudentRepository>();
            var emailService = Substitute.For<IEmailService>();
            studentRepository .DocumentExists("14655312386").Returns(false);            
            studentRepository.EmailExists(Email_Fake).Returns(false);
            var subscriptionHandler = new SubscriptionHandler(studentRepository, emailService);

            var command = new CreateBoletoSubscriptionCommand
            {
                FirstName = Faker.NameFaker.FirstName(),
                LastName = Faker.NameFaker.LastName(),
                Document = "14655312386",
                PayerDocumentType = Domain.Enums.EDocumentType.CPF,
                Email = Email_Fake,
                City = Faker.LocationFaker.City(),
                Street = Faker.LocationFaker.StreetName(),
                Total = 60,
                TotalPaid = 60
            };

            subscriptionHandler.Handler(command);
            subscriptionHandler.Valid.Should().BeTrue(ExtractNotifications(subscriptionHandler.Notifications));
        }
    }
}
