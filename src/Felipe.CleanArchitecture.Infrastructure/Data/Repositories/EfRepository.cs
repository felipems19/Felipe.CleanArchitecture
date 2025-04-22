using Felipe.CleanArchitecture.Domain.SeedWork;

namespace Felipe.CleanArchitecture.Infrastructure.Data.Repositories;

public class EfRepository<T>(AppDbContext dbContext) :
  BaseRepository<T>(dbContext), IRepository<T> where T : BaseAuditableEntity
{
}
