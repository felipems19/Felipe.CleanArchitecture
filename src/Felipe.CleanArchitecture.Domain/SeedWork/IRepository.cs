namespace Felipe.CleanArchitecture.Domain.SeedWork;

public interface IRepository<T>
{
    public IUnitOfWork UnitOfWork { get; }
}
