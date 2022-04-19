using AutoMapper;
using CLup.Domain.Messages;

namespace CLup.Application.Messages.Send
{
    public class MessageCommandMapper : Profile
    {
        public MessageCommandMapper()
        {
            CreateMap<SendMessageCommand, Message>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => MessageType.Parse(typeof(MessageType), src.Type)))
                .ForMember(dest => dest.MessageData, opts => opts.MapFrom(src => new MessageData(src.Title, src.Content)))
                .ForMember(dest => dest.Metadata, opts => opts.MapFrom(src => new MessageMetadata(false, false)));
        }
    }
}