using System.Threading.Tasks;

namespace CQRS.Core.Command
{
    public interface ICommandHandler
    {
    }
    
    public interface ICommandHandler<in TCommand>: ICommandHandler where TCommand: ICommand
    {
        Task Handle(TCommand command);
    }
}