using Logic.BusinessQueues;

namespace Logic.Users
{
    public class UserQueue
    {
        public string UserEmail { get; set; }

        public User User { get; set; }

        public int BusinessQueueId { get; set; }

        public BusinessQueue BusinessQueue { get; set; }


    }
}