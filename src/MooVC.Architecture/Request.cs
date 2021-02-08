namespace MooVC.Architecture
{
    using static MooVC.Architecture.Resources;
    using static MooVC.Ensure;

    public abstract class Request
    {
        protected Request(Message context)
        {
            ArgumentNotNull(context, nameof(context), RequestContextRequired);

            Context = context;
        }

        public Message Context { get; }
    }
}