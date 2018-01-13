using System.Threading.Tasks;
using CQRS.Core.Common;

namespace CQRS.Core.Command
{
    public class CommandBus : ICommandBus
    {
        private readonly IHandlerRegistrator<ICommandHandler> _commandHandlerRegistrator;

        public CommandBus(IHandlerRegistrator<ICommandHandler> commandHandlerRegistrator)
        {
            _commandHandlerRegistrator = commandHandlerRegistrator;
        }

        public async Task Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = _commandHandlerRegistrator.GetHandler<TCommand>();

            if (handler is ICommandHandler<TCommand> commandHandler)
            {
                await commandHandler.Handle(command);
            }
        }
    }
}