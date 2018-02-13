namespace WatchWord.Domain.Entities.Common
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}
