using AutoMapper;
using CLup.Domain.Business;
using CLup.Domain.User;

namespace CLup.Application.Queries.Shared
{
    public class MessageDtoMapper : Profile
    {
        public MessageDtoMapper()
        {
            CreateMap<UserMessage, MessageDto>()
                .ForMember(dest => dest.Title, config => config.MapFrom(src => src.MessageData.Title))
                .ForMember(dest => dest.Content, config => config.MapFrom(src => src.MessageData.Content))
                .ForMember(dest => dest.Date, config => config.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Sender, config => config.MapFrom(src => src.Sender.Name))
                .ForMember(dest => dest.SenderId, config => config.MapFrom(src => src.Sender.Id))
                .ForMember(dest => dest.Receiver, config => config.MapFrom(src => src.Receiver.Name))
                .ForMember(dest => dest.ReceiverId, config => config.MapFrom(src => src.Receiver.Id));

            CreateMap<BusinessMessage, MessageDto>()
                .ForMember(dest => dest.Title, config => config.MapFrom(src => src.MessageData.Title))
                .ForMember(dest => dest.Content, config => config.MapFrom(src => src.MessageData.Content))
                .ForMember(dest => dest.Date, config => config.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.Sender, config => config.MapFrom(src => src.Sender.Name))
                .ForMember(dest => dest.SenderId, config => config.MapFrom(src => src.Sender.Id))
                .ForMember(dest => dest.Receiver, config => config.MapFrom(src => src.Receiver.Name))
                .ForMember(dest => dest.Receiver, config => config.MapFrom(src => src.Receiver.Id));
        }
    }
}