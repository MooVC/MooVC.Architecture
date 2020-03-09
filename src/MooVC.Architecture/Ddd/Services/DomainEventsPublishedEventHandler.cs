namespace MooVC.Architecture.Ddd.Services
{
    public delegate void DomainEventsPublishedEventHandler(IBus sender, DomainEventsPublishedEventArgs e);
}