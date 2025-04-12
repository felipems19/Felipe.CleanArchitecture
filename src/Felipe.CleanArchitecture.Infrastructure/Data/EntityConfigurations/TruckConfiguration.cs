using Felipe.CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Felipe.CleanArchitecture.Infrastructure.Data.EntityConfigurations;

public class TruckConfiguration : IEntityTypeConfiguration<Truck>
{
    public void Configure(EntityTypeBuilder<Truck> builder)
    {
        builder.ToTable("Trucks", c => c.IsTemporal());

        builder.HasKey(c => c.Id);

        builder.Property(t => t.LicensePlate)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.Model)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.RegisteredAt)
            .IsRequired();
    }
}
