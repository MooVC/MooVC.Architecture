namespace MooVC.Architecture.Ddd.Services
{
    using System.Threading.Tasks;

    public interface IStartSaga<T>
        where T : DomainEvent
    {
        Task StartAsync(T @event);
    }
}