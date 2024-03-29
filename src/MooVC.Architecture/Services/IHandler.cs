﻿namespace MooVC.Architecture.Services;

using System.Threading;
using System.Threading.Tasks;

public interface IHandler<TMessage>
    where TMessage : Message
{
    Task ExecuteAsync(TMessage message, CancellationToken cancellationToken);
}