namespace CLup.Application.Queries.Shared
{
    public class ReceivedMessageDto : MessageDto
    { 
        public string Sender { get; set; }
        
        public string SenderId { get; set; }
         
    }
}