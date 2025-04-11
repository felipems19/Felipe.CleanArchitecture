using System.ComponentModel.DataAnnotations.Schema;

namespace Felipe.CleanArchitecture.Domain.SeedWork;

public abstract class BaseEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();

    private readonly List<BaseEvent> _domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
