namespace MooVC.Architecture.Cqrs.Services
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Linq;
    using MooVC.Serialization;
    using static MooVC.Ensure;
    using static Resources;

    [Serializable]
    public abstract class PaginatedQuery
        : Message
    {
        protected PaginatedQuery(Paging paging)
        {
            ArgumentNotNull(paging, nameof(Paging), PaginatedQueryPagingRequired);

            Paging = paging;
        }

        protected PaginatedQuery(Message context, Paging paging)
            : base(context)
        {
            ArgumentNotNull(paging, nameof(Paging), PaginatedQueryPagingRequired);

            Paging = paging;
        }

        protected PaginatedQuery(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Paging = info.GetValue<Paging>(nameof(Paging));
        }

        public Paging Paging { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Paging), Paging);
        }
    }
}
