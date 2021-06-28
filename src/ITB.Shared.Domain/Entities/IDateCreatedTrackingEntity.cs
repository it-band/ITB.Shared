using System;

namespace ITB.Shared.Domain.Entities
{
    public interface IDateCreatedTrackingEntity
    {
        DateTime DateCreated { get; set; }
    }
}
