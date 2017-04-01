namespace WatchWord.Domain.Entity.Common
{
    public abstract class Entity<T> : IEntity<T>
    {
        /// <summary>Gets or sets Id.</summary>
        public virtual T Id { get; set; }
    }
}