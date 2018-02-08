using System.Threading.Tasks;

namespace DIAS.Events
{
    /// <summary>
    /// Interface defines the handler to handle domain event.
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    public interface IDomainEventHandler<in TDomainEvent> : IDomainEventHandler
        where TDomainEvent : DomainEvent
    {
        Task Handle(TDomainEvent @event);
    }

    /// <summary>
    /// Interface defines the domain event handler
    /// </summary>
    public interface IDomainEventHandler
    { }
}
