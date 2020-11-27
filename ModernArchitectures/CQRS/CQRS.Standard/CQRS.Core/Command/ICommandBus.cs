using System.Threading.Tasks;

namespace CQRS.Core.Command
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand: ICommand;
    }
}