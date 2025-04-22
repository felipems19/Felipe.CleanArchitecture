using Felipe.CleanArchitecture.Domain.Interfaces;

namespace Felipe.CleanArchitecture.Domain.SeedWork;

public interface IRepository<T> : IBaseRepository<T> where T : BaseAuditableEntity
{
    public IUnitOfWork UnitOfWork { get; }
}
