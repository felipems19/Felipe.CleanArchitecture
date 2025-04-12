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
            var eventType = domainEvent.GetType();
            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
            var handlers = serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                var method = handlerType.GetMethod("Handle");
                if (method != null)
                {
                    var task = method.Invoke(handler, [domainEvent]) as Task;
                    await task!;
                }
            }
        }
    }
}
