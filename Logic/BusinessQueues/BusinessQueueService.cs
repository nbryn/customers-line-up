using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Data;
using Logic.DTO;


namespace Logic.BusinessQueues
{
    public class BusinessQueueService : IBusinessQueueService
    {
        private readonly IBusinessQueueRepository _queueRepository;
        private readonly IBusinessRepository _businessRepository;


        public BusinessQueueService(IBusinessQueueRepository queueRepository, IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
            _queueRepository = queueRepository;
        }

        public async Task<IEnumerable<BusinessQueueDTO>> GenerateQueues(CreateBusinessQueueRequest request)
        {
            ICollection<BusinessQueueDTO> queues = new List<BusinessQueueDTO>();

            BusinessDTO business = await _businessRepository.GetBusinessById(request.BusinessId);

            DateTime start = request.Start.AddHours(business.OpeningTime);

            DateTime closingTime = request.Start.AddHours(business.ClosingTime);

            for (DateTime date = start; date.Date <= request.End.Date; date = date.AddHours(request.TimeInterval))
            {
                if (date.Equals(closingTime))
                {
                    closingTime = closingTime.AddHours(24);

                    date = date.AddHours((23 - date.Hour) + business.OpeningTime);

                    continue;
                }

                CreateBusinessQueueDTO queue = new CreateBusinessQueueDTO
                {
                    BusinessId = request.BusinessId,
                    Start = date,
                    End = date.AddHours(request.TimeInterval)
                };

                BusinessQueueDTO newQueue = await _queueRepository.CreateBusinessQueue(queue);

                queues.Add(newQueue);
            }

            return queues;
        }
    }
}