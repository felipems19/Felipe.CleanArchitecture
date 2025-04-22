using Felipe.CleanArchitecture.Application.EventDispatching;
using Felipe.CleanArchitecture.Domain.SeedWork;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Felipe.CleanArchitecture.Infrastructure.Data.Interceptors;


public class DispatchDomainEventsInterceptor(IEventDispatcher dispatcher) : SaveChangesInterceptor
{
    private List<object> _domainEvents = [];

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context == null) return ValueTask.FromResult(result);

        var domainEntities = context.ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.Entity.DomainEvents.Count != 0)
            .Select(e => e.Entity)
            .ToList();

        _domainEvents = domainEntities
            .SelectMany(e => e.DomainEvents)
            .Cast<object>()
            .ToList();

        domainEntities.ToList().ForEach(e => e.ClearDomainEvents());

        return ValueTask.FromResult(result);
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        if (_domainEvents.Count > 0)
        {
            await dispatcher.Dispatch(_domainEvents);
            _domainEvents.Clear();
        }

        return result;
    }
}
