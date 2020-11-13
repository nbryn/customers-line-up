using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

using Logic.Context;
using Logic.Users;
using Logic.DTO.User;
using Logic.Businesses;
using Logic.BusinessOwners;
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

        public async Task<BusinessDTO> CreateBusiness(CreateBusinessDTO business, string ownerEmail)
        {
            Business newBusiness = new Business
            {
                Name = business.Name,
                Owner = await GetOwner(ownerEmail),
                OwnerEmail = ownerEmail,
                Capacity = business.Capacity,
                OpeningTime = business.OpeningTime,
                ClosingTime = business.ClosingTime,
                Zip = business.Zip
            };

            _context.Businesses.Add(newBusiness);

            await _context.SaveChangesAsync();

            return ConvertToDTO(newBusiness);

        }

        public async Task<BusinessDTO> GetBusinessById(int businessId)
        {
            Business business = await _context.Businesses.FindAsync(businessId);

            return ConvertToDTO(business);
        }

        public IQueryable<BusinessDTO> Read()
        {
            return from b in _context.Businesses
                   select new BusinessDTO
                   {
                       Id = b.Id,
                       Name = b.Name,
                       OwnerEmail = b.OwnerEmail,
                       Zip = b.Zip,
                       OpeningTime = b.OpeningTime,
                       ClosingTime = b.ClosingTime,
                       Capacity = b.Capacity
                   };
        }
        private Task<BusinessOwner> GetOwner(string email) =>
             _context.BusinessOwners.FirstOrDefaultAsync(u => u.UserEmail.Equals(email));

        private BusinessDTO ConvertToDTO(Business business)
        {
            return new BusinessDTO
            {
                Id = business.Id,
                OwnerEmail = business.OwnerEmail,
                Name = business.Name,
                Zip = business.Zip,
                OpeningTime = business.OpeningTime,
                ClosingTime = business.ClosingTime,
                Capacity = business.Capacity,
            };
        }
    }
}