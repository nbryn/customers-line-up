using AutoMapper;
using CLup.Application.Commands.User.SendMessage;
using CLup.Domain.Message;
using CLup.Domain.User;

namespace CLup.Application.Commands.Shared
{
    public class MessageMapper : Profile
    {
        public MessageMapper()
        {
            CreateMap<SendUserMessageCommand, UserMessage>()
                .ForMember(dest => dest.Type, config => config.MapFrom(src => MessageType.Parse(typeof(MessageType), src.Type)))
                .ForMember(dest => dest.MessageData, config => config.MapFrom(src => new MessageData(src.Title, src.Content)))
                .ForMember(dest => dest.Metadata, config => config.MapFrom(src => new MessageMetadata(false, false)))
                .ForMember(dest => dest.SenderId, config => config.MapFrom(src => src.SenderId))
                .ForMember(dest => dest.ReceiverId, config => config.MapFrom(src => src.ReceiverId));
        }
    }
}