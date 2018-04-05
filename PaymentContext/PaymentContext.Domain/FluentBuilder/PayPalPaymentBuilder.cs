using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Domain.FluentBuilder
{
    public sealed class PayPalPaymentBuilder : FluentBuilderBase<Payment>
    {       
        private string _transactionCode;
        private DateTime _paidDate;
        private DateTime _expireDate;
        private decimal _total;
        private decimal _totalPaid;
        private Address _address;
        private string _payer;
        private Document _document;
        private Email _email;

        public PayPalPaymentBuilder TransactionCode(string transaction)
        {
            _transactionCode = transaction;
            return this;    
        }

        public PayPalPaymentBuilder PaidDate(DateTime paidDate)
        {
            _paidDate = paidDate;
            return this;
        }

        public PayPalPaymentBuilder ExpireDate(DateTime expireDate)
        {
            _expireDate = expireDate;
            return this;
        }

        public PayPalPaymentBuilder Total(decimal total)
        {
            _total = total;
            return this;
        }

        public PayPalPaymentBuilder TotalPaid(decimal totalPaid)
        {
            _totalPaid = totalPaid;
            return this;
        }
        public PayPalPaymentBuilder Address(
                string street,
                string number,
                string neighborhood,
                string city,
                string state,
                string country,
                string zipCode)
        {
            _address = new Address(street, number, neighborhood, city, state, country, zipCode);
            return this;
        }
        public PayPalPaymentBuilder Payer(string payer)
        {
            _payer = payer;
            return this;
        }

        public PayPalPaymentBuilder Document(string number, EDocumentType type)
        {
            _document = new Document(number, type);            
            return this;
        }

        public PayPalPaymentBuilder Email(string address)
        {
            _email = new Email(address);
            return this;
        }

        public override Payment Build()
        {
            return new PayPalPayment(_paidDate, _expireDate, _total, _totalPaid, _address, _payer, _document, _email, _transactionCode);
        }
    }
}
