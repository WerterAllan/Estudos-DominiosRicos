using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.FluentBuilder
{
    public class StudentBuilder : FluentBuilderBase<Student>
    {
        private string _firstName;
        private string _lastName;
        private string _number;
        private EDocumentType _type;
        private Address _address;
        private string _email;

        public StudentBuilder Name(string firstName, string lastName)
        {
            _firstName = firstName;
            _lastName = lastName;
            return this;
        }

        public StudentBuilder Document(string number, EDocumentType type)
        {
            _number = number;
            _type = type;
            return this;
        }

        public StudentBuilder Email(string email)
        {
            _email = email;
            return this;
        }

        public StudentBuilder Address(
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

        public override Student Build()
        {
            var name = new Name(_firstName, _lastName);
            var document = new Document(_number, _type);
            var email = new Email(_email);
            return new Student(name, document, email, _address);
        }
    }
}
