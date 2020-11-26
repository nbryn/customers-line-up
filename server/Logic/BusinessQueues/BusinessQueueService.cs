using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Logic.Businesses;
using Data;
using Logic.DTO;
using Logic.DTO.User;
using Logic.Users;
using Logic.Util;

namespace Logic.BusinessQueues
{
    public class BusinessQueueService : IBusinessQueueService
    {
        private readonly IBusinessQueueRepository _queueRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDTOMapper _dtoMapper;


        public BusinessQueueService(IBusinessQueueRepository queueRepository,
            IBusinessRepository businessRepository, IUserRepository userRepository,
            IDTOMapper dtoMapper)
        {
            _businessRepository = businessRepository;
            _queueRepository = queueRepository;
            _userRepository = userRepository;
            _dtoMapper = dtoMapper;
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
                    Start = date,
                    End = date.AddHours(request.TimeInterval),
                    Capacity = business.Capacity,
                };

                queue.Id = await _queueRepository.CreateBusinessQueue(queue);

                BusinessQueueDTO dto = await _dtoMapper.ConvertQueueToDTO(queue);

                queues.Add(dto);
            }

            return queues;
        }

        public async Task<int> AddUserToQueue(string userEmail, int queueId)
        {
            BusinessQueue queue = await _queueRepository.FindQueueById(queueId);

            if (queue.Customers?.Count >= queue.Capacity)
            {
                // TODO: Handle
            }

            User user = await _userRepository.FindUserByEmail(userEmail);

            UserQueue s = new UserQueue { UserEmail = userEmail, BusinessQueueId = queueId };

            (queue.Customers ??= new List<UserQueue>()).Add(s);

            await _queueRepository.UpdateQueue(queue);

            return 1;
        }

        public async Task<int> RemoverUserFromQueue(string userEmail, int queueId)
        {
            BusinessQueue queue = await _queueRepository.FindQueueById(queueId);

            UserQueue user = await _userRepository.FindUserQueueByEmail(userEmail);

            queue.Customers.Remove(user);

            await _queueRepository.UpdateQueue(queue);

            return 1;
        }

        public async Task<ICollection<BusinessQueueDTO>> GetAllQueuesForBusiness(int businessId)
        {
            IList<BusinessQueue> queues = await _queueRepository.FindQueuesByBusiness(businessId);

            var dtos = queues.Select(async x => await _dtoMapper.ConvertQueueToDTO(x));

            return await Task.WhenAll(dtos.ToList());
        }
    }
}