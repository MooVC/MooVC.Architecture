namespace MooVC.Architecture.Cqrs.Services
{
    public interface IQueryHandler<TResult>
        where TResult : Message
    {
        TResult Execute();
    }
}