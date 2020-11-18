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
                BusinessId = queue.Business.Id,
                Capacity = queue.Capacity,
                Start = queue.Start,
                End = queue.End,
                Customers = queue.Customers?.Select(x => ConvertUserToDTO(x))
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
                Name = business.Name,
                Zip = business.Zip,
                OpeningTime = business.OpeningTime,
                ClosingTime = business.ClosingTime,
                Type = business.Type
            };
        }
    }
}