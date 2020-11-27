using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace CQRS.Core.Command
{
    public interface ICommandBus
    {
        Task<Unit> Send<TCommand>(TCommand request, CancellationToken cancellationToken = default)
            where TCommand : ICommand;
    }
}