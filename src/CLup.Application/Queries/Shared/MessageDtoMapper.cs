using AutoMapper;
namespace CLup.Application.Queries.Shared
{
    public class MessageDtoMapper : Profile
    {
        public MessageDtoMapper()
        {
            CreateMap<Domain.Messages.Message, MessageDto>()
                .ForMember(dest => dest.Title, config => config.MapFrom(src => src.MessageData.Title))
                .ForMember(dest => dest.Content, config => config.MapFrom(src => src.MessageData.Content))
                .ForMember(dest => dest.Date, config => config.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy")));
        }
    }
}