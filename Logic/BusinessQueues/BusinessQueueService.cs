using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.Businesses;
using Data;
using Logic.DTO;
using Logic.DTO.User;
using Logic.Users;

namespace Logic.BusinessQueues
{
    public class BusinessQueueService : IBusinessQueueService
    {
        private readonly IBusinessQueueRepository _queueRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IUserRepository _userRepository;


        public BusinessQueueService(IBusinessQueueRepository queueRepository,
            IBusinessRepository businessRepository, IUserRepository userRepository)
        {
            _businessRepository = businessRepository;
            _queueRepository = queueRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<BusinessQueueDTO>> GenerateQueues(CreateBusinessQueueRequest request)
        {
            ICollection<BusinessQueueDTO> queues = new List<BusinessQueueDTO>();

            Business business = await _businessRepository.FindBusinessById(request.BusinessId);

            DateTime start = request.Start.AddHours(business.OpeningTime);

            DateTime closingTime = request.Start.AddHours(business.ClosingTime);

            for (DateTime date = start; date.Date <= request.End.Date; date = date.AddHours(request.TimeInterval))
            {
                // Only add queues when shop is open
                if (date.Equals(closingTime))
                {
                    closingTime = closingTime.AddHours(24);

                    date = date.AddHours((23 - date.Hour) + business.OpeningTime);

                    continue;
                }

                BusinessQueue queue = new BusinessQueue
                {
                    BusinessId = business.Id,
                    Business = business,
                    Start = date,
                    End = date.AddHours(request.TimeInterval),
                    Capacity = business.Capacity,
                };

                queue.Id = await _queueRepository.CreateBusinessQueue(queue);

                queues.Add(ConvertQueueToDTO(queue));
            }

            return queues;
        }

        public async Task<BusinessQueueDTO> AddUserToQueue(AddUserToQueueRequest request)
        {
            BusinessQueue queue = await _queueRepository.FindQueueByBusinessAndStart(request.BusinessId, request.QueueStart);

            if (queue.Customers?.Count >= queue.Capacity)
            {
                // TODO: Handle
            }

            User user = await _userRepository.FindUserByEmail(request.UserMail);

            (queue.Customers ??= new List<User>()).Add(user);

            await _queueRepository.UpdateQueue(queue);

            return ConvertQueueToDTO(queue);
        }

        public async Task<IEnumerable<BusinessQueueDTO>> GetAllQueuesForBusiness(int businessId)
        {
            IList<BusinessQueue> queues = await _queueRepository.FindQueuesByBusiness(businessId);

            return queues.Select(x => ConvertQueueToDTO(x));
        }

        private BusinessQueueDTO ConvertQueueToDTO(BusinessQueue queue)
        {
            return new BusinessQueueDTO
            {
                BusinessId = queue.Business.Id,
                Capacity = queue.Capacity,
                Start = queue.Start,
                End = queue.End,
                Customers = queue.Customers?.Select(x => ConvertToDTO(x))
            };
        }

        private UserDTO ConvertToDTO(User user)
        {
            return new UserDTO
            {
                Name = user.Name,
                Email = user.Email,
                Zip = user.Zip,
            };
        }
    }
}