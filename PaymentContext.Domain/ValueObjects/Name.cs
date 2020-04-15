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

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "Name.FirstName", "FirstName should be at least 3 characters long")
                .HasMinLen(LastName, 3, "Name.LastName", "LastName should be at least 3 characters long")
                .HasMaxLen(FirstName, 20, "Name.FirstName", "FirstName should be up to 20 characters")
                .HasMaxLen(LastName, 40, "Name.LastName", "LastName should be up to 40 characters")
            );
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}
