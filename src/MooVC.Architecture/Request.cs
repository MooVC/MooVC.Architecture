namespace MooVC.Architecture
{
    using static MooVC.Ensure;
    using static Resources;

    public abstract class Request
    {
        protected Request(Message context)
        {
            ArgumentNotNull(context, nameof(context), GenericContextRequired);

            Context = context;
        }

        public Message Context { get; }
    }
}