using Felipe.CleanArchitecture.Domain.Entities;

namespace Felipe.CleanArchitecture.Domain.Interfaces.Repositories;

public interface ITruckRepository
{
    Task<IEnumerable<Truck>> GetAllAsync();
    Task<Truck> GetByIdAsync(Guid id);
    Task AddAsync(Truck truck);
    Task UpdateAsync(Truck truck);
    Task DeleteAsync(Guid id);
}
