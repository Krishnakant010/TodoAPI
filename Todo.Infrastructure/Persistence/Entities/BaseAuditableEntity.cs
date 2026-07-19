namespace Todo.Infrastructure.Persistence.Entities;

public abstract class BaseAuditableEntity :BaseEntity
{
    
    public DateTimeOffset CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; } 
    public string CreatedBy { get; set; }="system";
    public string? UpdatedBy { get; set; }
}