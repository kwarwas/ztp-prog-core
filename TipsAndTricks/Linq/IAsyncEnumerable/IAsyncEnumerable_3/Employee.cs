using System;

namespace IAsyncEnumerable_3
{
    class Employee
    {
        public int Id { get; set; }
        public DateTime BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public DateTime HireDate { get; set; }

        public override string ToString()
        {
            return $"{Id} {BirthDate.ToShortDateString()} {FirstName,10} {LastName,15} {Gender} {HireDate.ToShortDateString()}";
        }
    }
}