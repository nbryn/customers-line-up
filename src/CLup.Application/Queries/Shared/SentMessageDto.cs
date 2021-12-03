namespace CLup.Application.Queries.Shared
{
    public class SentMessageDto : MessageDto
    {

        public string Receiver { get; set; }
        
        public string ReceiverId { get; set; } 
    }
}