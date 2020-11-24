using System;
using System.Linq;

using Logic.BusinessQueues;
using Logic.Businesses;
using Logic.DTO.User;
using Logic.Users;
using Logic.DTO;


namespace Logic.Util
{
    public class DTOMapper : IDTOMapper
    {
        public BusinessQueueDTO ConvertQueueToDTO(BusinessQueue queue)
        {
            return new BusinessQueueDTO
            {
                Start = queue.Start,
                End = queue.End,
            };
        }

        public UserDTO ConvertUserToDTO(User user)
        {
            return new UserDTO
            {
                Name = user.Name,
                Email = user.Email,
                Zip = user.Zip,
            };
        }

        public BusinessDTO ConvertBusinessToDTO(Business business)
        {
            return new BusinessDTO
            {
                Id = business.Id,
                Name = business.Name,
                Zip = business.Zip,
                Opens = business.OpeningTime,
                Closes = business.ClosingTime,
                Type = business.Type
            };
        }
    }
}