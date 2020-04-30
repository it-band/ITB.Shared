namespace ITB.Shared.Domain.Entities
{
    public interface IPublicEntity<TPublicKey> : IEntity
    {
        TPublicKey PublicId { get; set; }
    }
}
