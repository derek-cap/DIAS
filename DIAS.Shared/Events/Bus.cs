using System;
using System.Collections.Generic;
using System.Text;

namespace DIAS.Events
{
    /// <summary>
    /// Event bus for handling all the domian events.
    /// </summary>
    public class Bus
    {
        private static readonly IList<IHandler<IDomainEvent>> _handlers = new List<IHandler<IDomainEvent>>();

        /// <summary>
        /// Register event handler to event bus.
        /// </summary>
        /// <param name="handler">Handler which handles IDomain event</param>
        public static void Register(IHandler<IDomainEvent> handler)
        {
            if (handler != null)
            {
                _handlers.Add(handler);
            }
        }

        /// <summary>
        /// Raise the handlers to handle <see cref="IDomainEvent"/>.
        /// </summary>
        /// <typeparam name="T">Domain event</typeparam>
        /// <param name="eventData">Domain event data</param>
        public static void Raise<T>(T eventData) where T : IDomainEvent
        {
            foreach (var handler in _handlers)
            {
                if (handler.CanHandle(eventData))
                {
                    handler.Handle(eventData);
                }
            }
        }

    }
}
