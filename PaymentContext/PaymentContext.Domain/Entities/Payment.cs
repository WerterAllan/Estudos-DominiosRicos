using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Domain.Entities
{
    public abstract class Payment : Entity
    {
        public DateTime PaidDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public decimal Total { get; private set; }
        public decimal TotalPaid { get; private set; }
        public Address Address { get; private set; }
        public string Payer { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }

        public Payment(
            DateTime paidDate,
            DateTime expireDate,
            decimal total,
            decimal totalPaid,
            Address address,
            string payer,
            Document document,
            Email email)
        {
            PaidDate = paidDate;
            ExpireDate = expireDate;
            Total = total;
            TotalPaid = totalPaid;
            Address = address;
            Payer = payer;
            Document = document;
            Email = email;

            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(Total, 0, "Payment.Total", "O total não pode ser zero")
                .IsGreaterThan(TotalPaid, 0, "Payment.TotalPaid", "O valor pago é menor que o valor do pagamento"));
        }

    }

    public class BoletoPayment : Payment
    {
        public string BarCode { get; set; }
        public string BoletoNumber { get; set; }

        public BoletoPayment( DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, Address address, string payer, Document document, Email email, 
            string barCode, string boletoNumber) 
            : base(paidDate, expireDate, total, totalPaid, address, payer, document, email)
        {
            BarCode = barCode;
            BoletoNumber = boletoNumber;
        }
    }


    public class CreditCardPayment : Payment
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string LastTransactionNumber { get; set; }

        public CreditCardPayment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, Address address, string payer, Document document, Email email,
            string cardHolderName, string cardNumber, string lastTransactionNumber)
            : base(paidDate, expireDate, total, totalPaid, address, payer, document, email)
        {
            CardHolderName = cardHolderName;
            CardNumber = cardNumber;
            LastTransactionNumber = lastTransactionNumber;
        }
    }


    public class PayPalPayment : Payment
    {
        public string TransactionCode { get; set; }

        public PayPalPayment(
            DateTime paidDate, 
            DateTime expireDate, 
            decimal total, 
            decimal totalPaid, 
            Address address, 
            string payer, 
            Document document, 
            Email email,
            string transactionCode)
           : base(paidDate, expireDate, total, totalPaid, address, payer, document, email)
        {
            TransactionCode = transactionCode;
        }

    }


}
