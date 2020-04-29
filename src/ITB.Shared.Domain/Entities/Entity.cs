namespace ITB.Shared.Domain.Entities
{
    public abstract class Entity : IEntity
    {
        public override string ToString() => $"[ENTITY: {GetType().Name}] Keys = {string.Join(", ", GetKeys())}";

        public abstract object[] GetKeys();
    }

    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        public virtual TKey Id { get; protected set; }

        protected Entity()
        {

        }

        protected Entity(TKey id)
        {
            Id = id;
        }

        public override object[] GetKeys()
        {
            return new object[] { Id };
        }

        public override string ToString()
        {
            return $"[ENTITY: {GetType().Name}] Id = {Id}";
        }
    }
}
