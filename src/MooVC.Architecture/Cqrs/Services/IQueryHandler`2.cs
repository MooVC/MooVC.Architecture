namespace MooVC.Architecture.Cqrs.Services
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : Message
        where TResult : Message
    {
        TResult Execute(TQuery query);
    }
}