using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Commands.User.SendMessage
{
    public class SendUserMessageCommand : IRequest<Result>
    {

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Type { get; set; }

        public SendUserMessageCommand()
        {

        }

        public SendUserMessageCommand(
            string senderId,
            string receiverId,
            string title,
            string content,
            string type)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            Title = title;
            Content = content;
            Type = type;
        }
    }
}

