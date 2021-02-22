using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using Logic.Context;
using Logic.Businesses;
using Logic.DTO;

namespace Data
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly ICLupContext _context;
        public BusinessRepository(ICLupContext context)
        {
            _context = context;
        }

        public async Task<Business> CreateBusiness(NewBusinessRequest business)
        {
            BusinessType.TryParse(business.Type, out BusinessType type);

            Business newBusiness = new Business
            {
                Name = business.Name,
                OwnerEmail = business.OwnerEmail,
                Capacity = business.Capacity,
                Opens = business.Opens,
                Closes = business.Closes,
                TimeSlotLength = business.timeSlotLength,
                Zip = business.Zip,
                Address = business.Address,
                Longitude = business.Longitude,
                Latitude = business.Latitude,
                Type = type,
            };

            _context.Businesses.Add(newBusiness);

            await _context.SaveChangesAsync();

            return newBusiness;

        }

        public async Task<HttpCode> UpdateBusiness(int businessId, NewBusinessRequest dto)
        {
            Business business = await FindBusinessById(businessId);

            if (business == null)
            {
                return HttpCode.NotFound;
            }

            BusinessType.TryParse(dto.Type, out BusinessType type);

            business.Capacity = dto.Capacity;
            business.Closes = dto.Closes;
            business.Opens = dto.Opens;
            business.Name = dto.Name;
            business.Zip = dto.Zip;
            business.Type = type;
            business.TimeSlotLength = dto.timeSlotLength;

            await _context.SaveChangesAsync();

            return HttpCode.Updated;
        }

        public async Task<Business> FindBusinessById(int businessId)
        {
            Business business = await _context.Businesses.FirstOrDefaultAsync(x => x.Id == businessId);

            return business;
        }

        public async Task<IList<Business>> FindBusinessesByOwner(string ownerEmail)
        {
            IList<Business> businesses = await _context.Businesses.Where(x => x.OwnerEmail.Equals(ownerEmail))
                                                                  .ToListAsync();

            return businesses;
        }

        public async Task<IList<Business>> GetAll()
        {
            return await _context.Businesses.ToListAsync();
        }

    }
}