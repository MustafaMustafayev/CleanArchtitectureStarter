namespace Domain.Primitives;
public class Auditable
{
    public Guid? CreatedById { get; set; }
    public Guid? ModifiedById { get; set; }
    public Guid? DeletedById { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}