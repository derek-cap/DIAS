using System.Threading.Tasks;

namespace DIAS.Events
{
    /// <summary>
    /// Interface defines the handler to handle dynamic domain event.
    /// </summary>
    public interface IDynamicDomainEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
