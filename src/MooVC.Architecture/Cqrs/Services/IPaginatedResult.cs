namespace MooVC.Architecture.Cqrs.Services
{
    using System.Collections.Generic;

    public interface IPaginatedResult<T>
    {
        IEnumerable<T> Results { get; }
        
        ushort TotalPages { get; }
        
        ulong TotalResults { get; }
    }
}