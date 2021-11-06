using System;
using System.Collections.Generic;

using MediatR;

namespace CLup.Domain.Shared
{
    public abstract class Entity
    {  
        public string Id { get;  set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        protected Entity()
        {
            Id = Guid.NewGuid().ToString();
        }
        
        public void ClearDomainEvents() => _domainEvents?.Clear(); 
   
        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem) => _domainEvents?.Remove(eventItem);       
    }
}