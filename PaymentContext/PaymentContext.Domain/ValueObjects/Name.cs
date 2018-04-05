using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            if (string.IsNullOrEmpty(FirstName))
                AddNotification("Name.FirstName", "Nome inválido");
            if (string.IsNullOrEmpty(LastName))
                AddNotification("Name.LastName", "Sobre nome inválido");

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres")
                .HasMaxLen(FirstName, 60, "Name.FirstName", "Nome muito grande"));

        }

        public string FirstName { get; }
        public string LastName { get; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
