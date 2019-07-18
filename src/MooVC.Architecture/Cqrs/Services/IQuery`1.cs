namespace MooVC.Architecture.Cqrs.Services
{
    public interface IQuery<TResponse>
        where TResponse : Message
    {
        TResponse Execute();
    }
}