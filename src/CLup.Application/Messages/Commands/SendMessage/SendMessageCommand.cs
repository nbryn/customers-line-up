using CLup.Application.Shared;
using MediatR;

namespace CLup.Application.Messages.Commands.SendMessage
{
    using Shared.Result;

    public class SendMessageCommand : IRequest<Result>
    {
        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Type { get; set; }

        public SendMessageCommand()
        {

        }

        public SendMessageCommand(
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

