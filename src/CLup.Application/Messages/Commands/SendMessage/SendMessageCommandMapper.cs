using AutoMapper;
using CLup.Domain.Messages;
using CLup.Domain.Messages.Enums;
using CLup.Domain.Messages.ValueObjects;

namespace CLup.Application.Messages.Commands.SendMessage
{
    public class SendMessageCommandMapper : Profile
    {
        public SendMessageCommandMapper()
        {
            CreateMap<SendMessageCommand, Message>()
                .ForMember(dest => dest.Type, opts => opts.MapFrom(src => MessageType.Parse(typeof(MessageType), src.Type)))
                .ForMember(dest => dest.MessageData, opts => opts.MapFrom(src => new MessageData(src.Title, src.Content)))
                .ForMember(dest => dest.Metadata, opts => opts.MapFrom(src => new MessageMetadata(false, false)));
        }
    }
}