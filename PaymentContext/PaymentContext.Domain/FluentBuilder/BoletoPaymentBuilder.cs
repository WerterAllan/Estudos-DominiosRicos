using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Domain.FluentBuilder
{
    public sealed class BoletoPaymentBuilder : FluentBuilderBase<Payment>
    {        
        private DateTime _paidDate;
        private DateTime _expireDate;
        private decimal _total;
        private decimal _totalPaid;
        private Address _address;
        private string _payer;
        private Document _document;
        private Email _email;
        private string _barCode;
        private string _boletoNumber;

        public BoletoPaymentBuilder BarCode(string barCode)
        {
            _barCode = barCode;
            return this;
        }

        public BoletoPaymentBuilder BoletoNumber(string boletoNumber)
        {
            _boletoNumber = boletoNumber;
            return this;
        }

        public BoletoPaymentBuilder PaidDate(DateTime paidDate)
        {
            _paidDate = paidDate;
            return this;
        }

        public BoletoPaymentBuilder ExpireDate(DateTime expireDate)
        {
            _expireDate = expireDate;
            return this;
        }

        public BoletoPaymentBuilder Total(decimal total)
        {
            _total = total;
            return this;
        }

        public BoletoPaymentBuilder TotalPaid(decimal totalPaid)
        {
            _totalPaid = totalPaid;
            return this;
        }
        public BoletoPaymentBuilder Address(
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
        public BoletoPaymentBuilder Payer(string payer)
        {
            _payer = payer;
            return this;
        }

        public BoletoPaymentBuilder Document(string number, EDocumentType type)
        {
            _document = new Document(number, type);
            return this;
        }

        public BoletoPaymentBuilder Email(string address)
        {
            _email = new Email(address);
            return this;
        }

        public override Payment Build()
        {
            return new BoletoPayment(_paidDate, _expireDate, _total, _totalPaid, _address, _payer, _document, _email, _barCode, _boletoNumber);
        }
    }
}
