namespace MooVC.Architecture.Cqrs.Services
{
    using System;
    using System.Collections.Generic;

    [Obsolete("IPaginatedResult<T> is obsolete and will be removed in version 3.0.  Use PaginatedResult<T>.")]
    public interface IPaginatedResult<T>
    {
        IEnumerable<T> Results { get; }
        
        ushort TotalPages { get; }
        
        ulong TotalResults { get; }
    }
}