using System;
using System.Collections.Generic;
using System.Text;

namespace DIAS.Events
{
    /// <summary>
    /// Interface defines the domain event
    /// </summary>
    public interface IDomainEvent
    {
    }

    /// <summary>
    /// Interface defines the handler to handle domain event.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHandler<T>
    {
        void Handle(T eventData);
        bool CanHandle(T eventType);
    }
}
