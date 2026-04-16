namespace AlgoDat
{
    public class Person : IComparable<Person>
    {
        private string? _firstName;

        private string? _lastName;

        private DateTime _birthDate;

        public Person(string firstName, string lastName, DateTime birthDate)
        {
            _firstName = firstName;
            _lastName = lastName;
            _birthDate = birthDate;
        }

        public Person(DateTime birthDate)
        {
            _birthDate = birthDate;
        }

        public int CompareTo(Person? other)
        {
            if (other == null)
            {
                return 1;
            }

            return _birthDate.CompareTo(other._birthDate);
        }

        public override string ToString()
        {
            return $"{_lastName}, {_firstName}: {_birthDate}";
        }
    }
}