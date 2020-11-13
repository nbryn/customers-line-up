using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

using Logic.Businesses;
using Logic.BusinessQueues;
using Logic.Context;
using Logic.DTO;

namespace Data
{
    public class BusinessQueueRepository : IBusinessQueueRepository
    {
        private readonly ICLupContext _context;

        public BusinessQueueRepository(ICLupContext context)
        {
            _context = context;
        }

        public async Task<BusinessQueueDTO> CreateBusinessQueue(CreateBusinessQueueDTO dto)
        {
            Business business = await GetBusiness(dto.BusinessId);

            BusinessQueue businessQueue = new BusinessQueue
            {
                BusinessId = business.Id,
                Business = business,
                Capacity = business.Capacity,
                Start = dto.Start,
                End = dto.End,

            };
            _context.BusinessQueues.Add(businessQueue);

            await _context.SaveChangesAsync();

            return ConvertToDTO(businessQueue);

        }

        private Task<Business> GetBusiness(int businessId) =>
             _context.Businesses.FirstOrDefaultAsync(b => b.Id == businessId);

        private BusinessQueueDTO ConvertToDTO(BusinessQueue queue)
        {
            return new BusinessQueueDTO
            {
                BusinessId = queue.BusinessId,
                Capacity = queue.Capacity,
                Start = queue.Start,
                End = queue.End
            };
        }
    }
}