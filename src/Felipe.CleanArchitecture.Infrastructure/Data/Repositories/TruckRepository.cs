using Felipe.CleanArchitecture.Application.EventDispatching;
using Felipe.CleanArchitecture.Domain.Entities;
using Felipe.CleanArchitecture.Domain.Events;
using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Felipe.CleanArchitecture.Infrastructure.Data.Repositories;

public class TruckRepository(AppDbContext dbContext, IEventDispatcher eventDispatcher) : ITruckRepository
{
    public async Task<List<Truck>> GetAllAsync()
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

    public async Task DeleteAsync(Truck truck)
    {
        truck.Delete();
        dbContext.Trucks.Remove(truck);
        await dbContext.SaveChangesAsync();

        await eventDispatcher.Dispatch(truck.DomainEvents);
        truck.ClearDomainEvents();
    }

    public async Task DeleteAllAsync(List<Truck> trucks)
    {
        foreach (var truck in trucks)
        {
            truck.AddDomainEvent(new TruckDeletedEvent
            {
                TruckId = truck.Id,
                DeletedAt = DateTime.UtcNow
            });
        }

        dbContext.Trucks.RemoveRange(trucks);
        await dbContext.SaveChangesAsync();

        await eventDispatcher.Dispatch(trucks.SelectMany(t => t.DomainEvents));
        foreach (var truck in trucks)
            truck.ClearDomainEvents();
    }
}
