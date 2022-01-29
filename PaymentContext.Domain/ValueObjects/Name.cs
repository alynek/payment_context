using Flunt.Validations;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract<Name>()
                .Requires()
                .IsGreaterThan(FirstName, 3, "Name.FirstName", "Name must be at least 3 characters long")
                .IsGreaterThan(LastName, 3, "Name.LastName", "LastName must be at least 3 characters long")
                .IsLowerThan(FirstName, 40, "Name.FirstName", "Name must contain 40 characters")
                .IsLowerThan(LastName, 40, "Name.LastName", "LastName must contain 40 characters")
            );
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}