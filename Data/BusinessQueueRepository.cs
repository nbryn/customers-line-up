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
        public async Task<BusinessQueueDTO> AddUserToQueue(AddUserToQueueRequest request)
        {
            BusinessQueue queue = await _context.BusinessQueues.FirstOrDefaultAsync(x =>
                                                        x.BusinessId == request.BusinessId &&
                                                        x.Start.Equals(request.QueueStart));


            (queue.Customers ??= new List<User>()).Add(await GetUser(request.UserMail));

            _context.BusinessQueues.Update(queue);

            await _context.SaveChangesAsync();

            return ConvertToDTO(queue);
        }

        public async Task<ICollection<BusinessQueueDTO>> GetQueuesByBusiness(int businessId)
        {
            ICollection<BusinessQueueDTO> queues = await _context.BusinessQueues.Include(x => x.Customers)
                                                                                .Where(x => x.BusinessId == businessId)
                                                                                .Select(x => ConvertToDTO(x))
                                                                                .ToListAsync();
            return queues;
        }

        private Task<Business> GetBusiness(int businessId) =>
             _context.Businesses.FirstOrDefaultAsync(b => b.Id == businessId);

        private Task<User> GetUser(string email) =>
            _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));

        private static BusinessQueueDTO ConvertToDTO(BusinessQueue queue)
        {
            return new BusinessQueueDTO
            {
                BusinessId = queue.BusinessId,
                Capacity = queue.Capacity,
                Start = queue.Start,
                End = queue.End,
                Customers = queue.Customers?.Select(x => ConvertToDTO(x))
                                            .AsEnumerable()
            };
        }

        private static UserDTO ConvertToDTO(User user)
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