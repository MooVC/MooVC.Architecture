namespace MooVC.Architecture.Ddd.Services
{
    public delegate void DomainEventsUnhandledEventHandler(IBus sender, DomainEventsUnhandledEventArgs e);
}