using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

using Logic.Businesses;
using Logic.BusinessQueues;
using Logic.Context;
using Logic.DTO;
using Logic.DTO.User;
using Logic.Users;

namespace Data
{
    public class BusinessQueueRepository : IBusinessQueueRepository
    {
        private readonly ICLupContext _context;
        public BusinessQueueRepository(ICLupContext context)
        {
            _context = context;
        }
        public async Task<int> CreateBusinessQueue(BusinessQueue queue)
        {
            _context.BusinessQueues.Add(queue);

            await _context.SaveChangesAsync();

            return queue.Id;
        }

        public async Task<BusinessQueue> FindQueueByBusinessAndStart(int businessId, DateTime queueStart)
        {
            BusinessQueue queue = await _context.BusinessQueues.Include(x => x.Customers)
                                                               .FirstOrDefaultAsync(x =>
                                                                    x.BusinessId == businessId &&
                                                                    x.Start.Equals(queueStart));
            return queue;
        }

        public async Task<IList<BusinessQueue>> FindQueuesByBusiness(int businessId)
        {
            IList<BusinessQueue> queues = await _context.BusinessQueues.Include(x => x.Customers)
                                                                       .Where(x => x.BusinessId == businessId)
                                                                       .ToListAsync();
            return queues;
        }

        public async Task<IList<BusinessQueue>> FindAvailableQueuesByBusiness(AvailableQueuesRequest request)
        {
            IList<BusinessQueue> queues = await _context.BusinessQueues.Include(x => x.Customers)
                                                                        .Where(x => x.BusinessId == request.BusinessId &&
                                                                               (x.Start > request.Start && x.End < request.End))
                                                                        .ToListAsync();

            return queues;
        }

        public async Task<int> UpdateQueue(BusinessQueue queue)
        {
            _context.BusinessQueues.Update(queue);

            return await _context.SaveChangesAsync();
        }
    }
}