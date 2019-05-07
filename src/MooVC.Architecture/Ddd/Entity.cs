namespace MooVC.Architecture.Ddd
{
    public abstract class Entity<T>
        where T : struct
    {
        protected Entity(T id)
        {
            Id = id;
        }

        public T Id { get; }

        public override bool Equals(object other)
        {
            return other is Entity<T> entity
                && Id.Equals(entity.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}