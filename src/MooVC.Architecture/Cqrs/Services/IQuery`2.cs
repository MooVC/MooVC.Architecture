namespace MooVC.Architecture.Cqrs.Services
{
    public interface IQuery<TRequest, TResult>
        where TRequest : Message
        where TResult : Message
    {
        TResult Execute(TRequest request);
    }
}