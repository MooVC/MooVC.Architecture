namespace MooVC.Architecture.Cqrs.Services
{
    public interface IQueryEngine
    {
        TResult Query<TResult>()
            where TResult : Message;

        TResult Query<TQuery, TResult>(TQuery query)
            where TQuery : Message
            where TResult : Message;
    }
}