namespace MooVC.Architecture.Cqrs.Services
{
    public interface IQuery<TRequest, TResponse>
        where TRequest : Message
        where TResponse : Message
    {
        TResponse Execute(TRequest request);
    }
}