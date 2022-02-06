using AutoMapper;
using CLup.Domain.Messages;

namespace CLup.Application.Commands.Shared.Message.Send
{
    public class MessageMapper : Profile
    {
        public MessageMapper()
        {
            CreateMap<SendMessageCommand, Domain.Messages.Message>()
                .ForMember(dest => dest.Type, config => config.MapFrom(src => MessageType.Parse(typeof(MessageType), src.Type)))
                .ForMember(dest => dest.MessageData, config => config.MapFrom(src => new MessageData(src.Title, src.Content)))
                .ForMember(dest => dest.Metadata, config => config.MapFrom(src => new MessageMetadata(false, false)));
        }
    }
}