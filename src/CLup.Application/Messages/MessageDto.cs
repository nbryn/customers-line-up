namespace CLup.Application.Messages
{
    public class MessageDto
    {
        public string Id { get; set; }
        
        public string Title { get; set; }
        
        public string Content { get; set; }   
        
        public string Date { get; set; }
        
        public string Sender { get; set; }
        
        public string SenderId { get; set; }

        public string Receiver { get; set; }
        
        public string ReceiverId { get; set; }         
    }
}