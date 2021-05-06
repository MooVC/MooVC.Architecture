namespace MooVC.Architecture.Cqrs.Services
{
    using System.Threading.Tasks;

    public interface IQueryHandler<TResult>
        where TResult : Message
    {
        Task<TResult> ExecuteAsync();
    }
}