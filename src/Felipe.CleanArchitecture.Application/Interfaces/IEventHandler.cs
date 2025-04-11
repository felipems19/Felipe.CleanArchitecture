namespace Felipe.CleanArchitecture.Application.Interfaces;

public interface IEventHandler<TEvent>
{
    Task Handle(TEvent domainEvent);
}
