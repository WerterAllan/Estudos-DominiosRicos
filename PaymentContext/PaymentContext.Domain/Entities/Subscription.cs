using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        public DateTime CreatedTime { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool Active { get; set; }
        private List<Payment> _payment;
        public IReadOnlyCollection<Payment> Payments { get { return _payment; } }

        public Subscription(DateTime? expireDate)
        {
            CreatedTime = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            ExpireDate = expireDate;
            Active = true;
            this._payment = new List<Payment>();
        }

        public void AddPayment(Payment payment)
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscription.Payments", "A data do pamento deve ser futura"));

            _payment.Add(payment);

        }

        public void Activate()
        {
            Active = true;
            LastUpdateDate = DateTime.Now;
        }

        public void Inactivate()
        {
            Active = false;
            LastUpdateDate = DateTime.Now;
        }
    }
}
