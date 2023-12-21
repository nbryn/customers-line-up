using AutoMapper;
using CLup.Domain.Messages;
using CLup.Domain.Messages.ValueObjects;

namespace CLup.Application.Messages.Commands.SendMessage;

public sealed class SendMessageCommandMapper : Profile
{
    public SendMessageCommandMapper()
    {
        CreateMap<SendMessageCommand, Message>()
            .ForMember(dest => dest.MessageData, opts => opts.MapFrom(src => new MessageData(src.Title, src.Content)))
            .ForMember(dest => dest.Metadata, opts => opts.MapFrom(src => new MessageMetadata(false, false)));
    }
}
