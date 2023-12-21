using AutoMapper;
using CLup.Domain.Messages;

namespace CLup.Application.Messages;

public sealed class MessageMapper : Profile
{
    public MessageMapper()
    {
        CreateMap<Message, MessageDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Title, opts => opts.MapFrom(src => src.MessageData.Title))
            .ForMember(dest => dest.Content, opts => opts.MapFrom(src => src.MessageData.Content))
            .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy")));
    }
}
