using System.Threading.Tasks;
using CQRS.Core.Common;

namespace CQRS.Core.Command
{
    public class CommandBus : ICommandBus
    {
        private readonly IHandlerRegistrar<ICommandHandler> _commandHandlerRegistrar;

        public CommandBus(IHandlerRegistrar<ICommandHandler> commandHandlerRegistrar)
        {
            _commandHandlerRegistrar = commandHandlerRegistrar;
        }

        public async Task Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _commandHandlerRegistrar.GetHandler<TCommand>();

            if (handler is ICommandHandler<TCommand> commandHandler)
            {
                await commandHandler.Handle(command);
            }
        }
    }
}