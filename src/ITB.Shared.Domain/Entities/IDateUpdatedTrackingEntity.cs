using System;

namespace ITB.Shared.Domain.Entities
{
    public interface IDateUpdatedTrackingEntity
    {
        DateTime DateUpdated { get; set; }
    }
}
