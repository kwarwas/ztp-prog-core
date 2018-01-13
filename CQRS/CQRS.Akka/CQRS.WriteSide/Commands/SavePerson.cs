using CQRS.Core.Command;

namespace CQRS.WriteSide.Commands
{
    class SavePerson : ICommand
    {
        public string FirstName { get; }
        public string LastName { get; }

        public SavePerson(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}