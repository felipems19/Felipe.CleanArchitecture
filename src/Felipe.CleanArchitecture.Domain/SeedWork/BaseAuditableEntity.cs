namespace Felipe.CleanArchitecture.Domain.SeedWork;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
}
