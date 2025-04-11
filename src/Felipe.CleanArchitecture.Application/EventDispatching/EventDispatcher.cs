using Felipe.CleanArchitecture.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Felipe.CleanArchitecture.Application.EventDispatching;

public interface IEventDispatcher
{
    Task Dispatch(IEnumerable<object> domainEvents);
}

public class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    public async Task Dispatch(IEnumerable<object> domainEvents)
    {
        foreach (var domainEvent in domainEvents)
        {
            var handlerType = typeof(IEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handlers = serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                await ((dynamic)handler).Handle((dynamic)domainEvent);
            }
        }
    }
}
