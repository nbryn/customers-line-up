using System;

namespace CLup.Domain
{
    public class BaseEntity
    {
        public string Id { get; internal set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}