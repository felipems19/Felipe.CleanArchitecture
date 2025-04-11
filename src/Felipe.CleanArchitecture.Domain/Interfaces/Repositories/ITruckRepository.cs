using Felipe.CleanArchitecture.Domain.Entities;

namespace Felipe.CleanArchitecture.Domain.Interfaces.Repositories;

public interface ITruckRepository
{
    Task<Truck> GetByIdAsync(Guid id);
    Task AddAsync(Truck truck);
    Task UpdateAsync(Truck truck);
    Task DeleteAsync(Guid id);
}
