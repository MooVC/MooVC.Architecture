namespace MooVC.Architecture.Ddd
{
    using System;

    public interface IReference
    {
        Guid Id { get; }

        Type Type { get; }
    }
}