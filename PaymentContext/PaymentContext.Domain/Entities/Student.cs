using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        public Student(Name name, Document document, Email email, Address address)
        {
            Name = name;
            Document = document;
            Email = email;
            Address = address;
            _subscription = new List<Subscription>();

            AddNotifications(name, document, email);

        }
        
        public Name Name { get; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }

        private IList<Subscription> _subscription;
        public IReadOnlyCollection<Subscription> Subscriptions
        {
            get { return _subscription.ToList(); }
        }

        public void AddSubscriptions(Subscription sub)
        {
            var hasSubscriptionActive = _subscription.Any(x => x.Active);
            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscription", "Você já tem uma assinatura ativa"));
            
            _subscription.Add(sub);

        }

        
    }
}
