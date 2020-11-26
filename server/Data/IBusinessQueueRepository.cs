using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Logic.DTO;
using Logic.BusinessQueues;

namespace Data
{
    public interface IBusinessQueueRepository
    {
        Task<int> CreateBusinessQueue(BusinessQueue queue);

        Task<IList<BusinessQueue>> FindQueuesByBusiness(int businessId);

        Task<IList<BusinessQueue>> FindAvailableQueuesByBusiness(AvailableQueuesRequest request);

        Task<BusinessQueue> FindQueueByBusinessAndStart(int businessId, DateTime queueStart);

        Task<BusinessQueue> FindQueueById(int queueId);

        Task<IList<BusinessQueue>> FindQueuesByUser(string userMail);

        Task<int> UpdateQueue(BusinessQueue queue);
        
    }
}