namespace WatchWord.Domain.Entity.Common
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}