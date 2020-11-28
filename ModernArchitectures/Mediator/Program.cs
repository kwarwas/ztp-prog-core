using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Mediator
{
    public class CreateUserAccount : IRequest<Guid>
    {
    }

    public class UserAccountCreated : INotification
    {
        public Guid Id { get; }

        public UserAccountCreated(Guid id)
        {
            Id = id;
        }
    }

    public class CreateUserAccountHandler : IRequestHandler<CreateUserAccount, Guid>
    {
        private readonly IMediator _mediator;

        public CreateUserAccountHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateUserAccount request, CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync("Create User Account Handler");

            var id = Guid.NewGuid();

            await _mediator.Publish(new UserAccountCreated(id), cancellationToken);

            return id;
        }
    }

    public class UserAccountCreatedHandler1 : INotificationHandler<UserAccountCreated>
    {
        public async Task Handle(UserAccountCreated notification, CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync($"{notification.Id} in {GetType().Name}");
        }
    }

    public class UserAccountCreatedHandler2 : INotificationHandler<UserAccountCreated>
    {
        public async Task Handle(UserAccountCreated notification, CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync($"{notification.Id} in {GetType().Name}");
        }
    }

    class Program
    {
        static async Task Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddMediatR(Assembly.GetExecutingAssembly())
                .BuildServiceProvider();

            var mediator = serviceProvider.GetService<IMediator>();

            if (mediator is null)
                return;

            var response = await mediator.Send(new CreateUserAccount());

            Console.WriteLine(response);
        }
    }
}