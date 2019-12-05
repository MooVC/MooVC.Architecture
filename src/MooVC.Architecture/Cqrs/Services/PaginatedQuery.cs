namespace MooVC.Architecture.Cqrs.Services
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Linq;
    using static MooVC.Ensure;
    using static Resources;

    [Serializable]
    public class PaginatedQuery
        : Message
    {
        public PaginatedQuery(Paging paging)
        {
            ArgumentNotNull(paging, nameof(Paging), PaginatedQueryPagingRequired);

            Paging = paging;
        }

        public PaginatedQuery(Message context, Paging paging)
            : base(context)
        {
            ArgumentNotNull(paging, nameof(Paging), PaginatedQueryPagingRequired);

            Paging = paging;
        }

        protected PaginatedQuery(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Paging = (Paging)info.GetValue(nameof(Paging), typeof(Paging));
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
