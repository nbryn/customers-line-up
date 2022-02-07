using AutoMapper;
using CLup.Domain.Messages;

namespace CLup.Application.Shared.Messages.Send
{
    public class MessageCommandMapper : Profile
    {
        public MessageCommandMapper()
        {
            CreateMap<SendMessageCommand, Message>()
                .ForMember(dest => dest.Type, config => config.MapFrom(src => MessageType.Parse(typeof(MessageType), src.Type)))
                .ForMember(dest => dest.MessageData, config => config.MapFrom(src => new MessageData(src.Title, src.Content)))
                .ForMember(dest => dest.Metadata, config => config.MapFrom(src => new MessageMetadata(false, false)));
        }
    }
}