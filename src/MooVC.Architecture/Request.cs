namespace MooVC.Architecture
{
    using static MooVC.Ensure;

    public abstract class Request
    {
        protected Request(Message context)
        {
            ArgumentNotNull(context, nameof(context), Resources.GenericContextRequired);

            Context = context;
        }

        public Message Context { get; }
    }
}