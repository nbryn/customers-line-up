using System.Linq;
using System.Threading.Tasks;

using MediatR;

using CLup.Data;
using CLup.Domain.Shared;

namespace CLup.Extensions
{
    public static class MediatorExtensions
    {

        public static async Task DispatchDomainEventsAsync(this IMediator mediator, CLupContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}