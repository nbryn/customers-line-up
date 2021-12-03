using AutoMapper;
using CLup.Domain.Business;
using CLup.Domain.User;

namespace CLup.Application.Queries.Shared
{
    public class MessageMapper : Profile
    {
        public MessageMapper()
        {
            CreateMap<UserMessage, MessageDto>()
                .ForMember(dest => dest.Title, config => config.MapFrom(src => src.MessageData.Title))
                .ForMember(dest => dest.Content, config => config.MapFrom(src => src.MessageData.Content))
                .ForMember(dest => dest.Date, config => config.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy")));

            CreateMap<BusinessMessage, MessageDto>()
                .ForMember(dest => dest.Title, config => config.MapFrom(src => src.MessageData.Title))
                .ForMember(dest => dest.Content, config => config.MapFrom(src => src.MessageData.Content))
                .ForMember(dest => dest.Date, config => config.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy")));

            CreateMap<UserMessage, SentMessageDto>()
                .IncludeBase<UserMessage, MessageDto>()
                .ForMember(dest => dest.Receiver, config => config.MapFrom(src => src.Receiver.Name));

            CreateMap<UserMessage, ReceivedMessageDto>()
                .IncludeBase<UserMessage, MessageDto>()
                .ForMember(dest => dest.Sender, config => config.MapFrom(src => src.Sender.Name));

            CreateMap<BusinessMessage, SentMessageDto>()
                .IncludeBase<BusinessMessage, MessageDto>()
                .ForMember(dest => dest.Receiver, config => config.MapFrom(src => src.Receiver.Name));

            CreateMap<BusinessMessage, ReceivedMessageDto>()
                .IncludeBase<BusinessMessage, MessageDto>()
                .ForMember(dest => dest.Sender, config => config.MapFrom(src => src.Sender.Name));

        }
    }
}