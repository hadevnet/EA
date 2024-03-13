namespace EA.Core.Common;

public class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime? Updated { get; set; }

    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}