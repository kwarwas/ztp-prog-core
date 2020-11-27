namespace CQRS.Core.Command
{
    public class CommandResult
    {
        public bool Success { get; }

        public CommandResult(bool success = true)
        {
            Success = success;
        }
    }
}