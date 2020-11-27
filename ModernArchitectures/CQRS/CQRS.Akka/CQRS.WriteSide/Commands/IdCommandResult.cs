using CQRS.Core.Command;

namespace CQRS.WriteSide.Commands
{
    public class IdCommandResult : CommandResult
    {
        public int Id { get; }

        public IdCommandResult(int id, bool success = true) : base(success)
        {
            Id = id;
        }
    }
}