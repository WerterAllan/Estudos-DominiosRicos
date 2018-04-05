using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.FluentBuilder;

namespace PaymentContext.Domain.Converts
{
    public static class BoletoPaymentConverter
    {
        public static Payment ToBoletoPaymentEntity(this CreateBoletoSubscriptionCommand command)
        {
            var boleto = new BoletoPaymentBuilder()
                 .BarCode(command.BarCode)
                 .PaidDate(command.PaidDate)
                 .ExpireDate(command.ExpireDate)
                 .Total(command.Total)
                 .TotalPaid(command.TotalPaid)
                 .Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode)
                 .Payer(command.Payer)
                 .Document(command.Document, command.PayerDocumentType)
                 .Email(command.PayerEmail)
                 .BoletoNumber(command.BoletoNumber)
                 .Build();

            return boleto;
        }
    }
}
