namespace MooVC.Architecture.Cqrs.Services
{
    public interface IQueryEngine
    {
        TResponse Query<TResponse>()
            where TResponse : Message;

        TResponse Query<TRequest, TResponse>(TRequest request)
            where TRequest : Message
            where TResponse : Message;
    }
}