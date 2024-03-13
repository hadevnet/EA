using System.ComponentModel.DataAnnotations.Schema;

namespace EA.Core.Common;

public class BaseEntity
{
    public int Id { get; set; }

    [NotMapped]
    public List<BaseEvent> DomainEvents = new();
}