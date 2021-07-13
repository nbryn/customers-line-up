using System;

namespace CLup.Domain
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}