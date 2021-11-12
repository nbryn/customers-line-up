using System;

namespace CLup.Domain.Shared
{
    public abstract class Entity
    {  
        public string Id { get;  set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        protected Entity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}