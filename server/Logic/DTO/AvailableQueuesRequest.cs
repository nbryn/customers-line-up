using System;

namespace Logic.DTO
{
    public class AvailableQueuesRequest
    {
        public int BusinessId { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}