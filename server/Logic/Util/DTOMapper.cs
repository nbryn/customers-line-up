using System.Threading.Tasks;
using System.Linq;

using Data;
using Logic.BusinessQueues;
using Logic.Businesses;
using Logic.DTO.User;
using Logic.Users;
using Logic.DTO;


namespace Logic.Util
{
    public class DTOMapper : IDTOMapper
    {
        private readonly IBusinessRepository _businessRepository;

        public DTOMapper(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }
        public async Task<BusinessQueueDTO> ConvertQueueToDTO(BusinessQueue queue)
        {
            Business business = await _businessRepository.FindBusinessById(queue.BusinessId);
            return new BusinessQueueDTO
            {
                Id = queue.Id,
                BusinessId = queue.BusinessId,
                Business = business.Name,
                Date = queue.Start.ToString("dd/MM/yyyy"),
                Start = queue.Start.TimeOfDay.ToString().Substring(0, 5),
                End = queue.End.TimeOfDay.ToString().Substring(0, 5),
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