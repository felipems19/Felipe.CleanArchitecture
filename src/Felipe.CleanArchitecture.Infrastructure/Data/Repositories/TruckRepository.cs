using Felipe.CleanArchitecture.Application.EventDispatching;
using Felipe.CleanArchitecture.Domain.Entities;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Felipe.CleanArchitecture.Infrastructure.Data.Repositories;

public class TruckRepository(AppDbContext dbContext, IEventDispatcher eventDispatcher) : ITruckRepository
{
    public async Task<IEnumerable<Truck>> GetAllAsync()
    {
        return await dbContext.Trucks.ToListAsync();
    }

    public async Task<Truck> GetByIdAsync(Guid id)
    {
        return await dbContext.Trucks.FindAsync(id);
    }

    public async Task AddAsync(Truck truck)
    {
        await dbContext.Trucks.AddAsync(truck);
        await dbContext.SaveChangesAsync();

        await eventDispatcher.Dispatch(truck.DomainEvents);
        truck.ClearDomainEvents();
    }

    public async Task UpdateAsync(Truck truck)
    {
        dbContext.Trucks.Update(truck);
        await dbContext.SaveChangesAsync();

        await eventDispatcher.Dispatch(truck.DomainEvents);
        truck.ClearDomainEvents();
    }

    public async Task DeleteAsync(Guid id)
    {
        var truck = await GetByIdAsync(id);
        if (truck != null)
        {
            truck.Delete();
            dbContext.Trucks.Update(truck);
            await dbContext.SaveChangesAsync();

            await eventDispatcher.Dispatch(truck.DomainEvents);
            truck.ClearDomainEvents();
        }
    }
}
