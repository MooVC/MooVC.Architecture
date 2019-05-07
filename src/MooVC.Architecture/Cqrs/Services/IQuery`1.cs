namespace MooVC.Architecture.Cqrs.Services
{
    public interface IQuery<TResult>
        where TResult : Message
    {
        TResult Execute();
    }
}