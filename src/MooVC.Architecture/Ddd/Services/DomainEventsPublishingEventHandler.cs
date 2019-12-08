namespace MooVC.Architecture.Ddd.Services
{
    public delegate void DomainEventsPublishingEventHandler(IBus sender, DomainEventsPublishingEventArgs e);
}