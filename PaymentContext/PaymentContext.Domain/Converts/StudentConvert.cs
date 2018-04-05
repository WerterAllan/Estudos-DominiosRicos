using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.FluentBuilder;

namespace PaymentContext.Domain.Converts
{
    public static class StudentConvert
    {
        public static Student ToStudentEntity(this CreateBoletoSubscriptionCommand command)
        {
            var student = new StudentBuilder()
               .Name(command.FirstName, command.LastName)
               .Document(command.Document, command.PayerDocumentType)
               .Email(command.Email)
               .Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode)
               .Build();

            return student;
        }
    }
}
