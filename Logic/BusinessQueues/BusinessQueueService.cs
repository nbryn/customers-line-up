using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Data;
using Logic.DTO;

namespace Logic.BusinessQueues
{
    public class BusinessQueueService : IBusinessQueueService
    {
        private readonly IBusinessQueueRepository _repository;

        public BusinessQueueService(IBusinessQueueRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BusinessQueueDTO>> GenerateQueues(CreateBusinessQueueRequest request)
        {

            ICollection<BusinessQueueDTO> queues = new List<BusinessQueueDTO>();

            for (DateTime date = request.Start.Date; date.Date <= request.End.Date; date = date.AddHours(request.TimeInterval))
            {
                CreateBusinessQueueDTO queue = new CreateBusinessQueueDTO
                {
                    BusinessId = request.BusinessId,
                    Start = date.Date,
                    End = date.AddHours(request.TimeInterval)

                };

                BusinessQueueDTO newQueue = await _repository.CreateBusinessQueue(queue);

                queues.Add(newQueue);

            }

            return queues;
        }
    }
}