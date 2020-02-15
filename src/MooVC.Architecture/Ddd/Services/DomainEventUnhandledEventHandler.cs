namespace MooVC.Architecture.Ddd.Services
{
    public delegate void DomainEventUnhandledEventHandler(IBus sender, DomainEventUnhandledEventArgs e);
}