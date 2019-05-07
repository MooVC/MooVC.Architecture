namespace MooVC.Architecture
{
    public abstract class Request
    {
        protected Request(Message context)
        {
            Ensure.ArgumentNotNull(context, nameof(context), Resources.GenericContextRequired);

            Context = context;
        }

        public Message Context { get; }
    }
}