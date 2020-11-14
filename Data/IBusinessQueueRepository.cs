using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Logic.BusinessQueues;

namespace Data
{
    public interface IBusinessQueueRepository
    {
        Task<int> CreateBusinessQueue(BusinessQueue queue);

        Task<IList<BusinessQueue>> FindQueuesByBusiness(int businessId);

        Task<BusinessQueue> FindQueueByBusinessAndStart(int businessId, DateTime queueStart);

        Task<int> UpdateQueue(BusinessQueue queue);

        
    }
}