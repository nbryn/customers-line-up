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
                Zip = business.Zip
            };

            _context.Businesses.Add(newBusiness);

            await _context.SaveChangesAsync();

            return new BusinessDTO(newBusiness);

        }

        public IQueryable<BusinessDTO> Read()
        {
            return from b in _context.Businesses
                   select new BusinessDTO
                   {
                       Id = b.Id,
                       Name = b.Name,
                       OwnerEmail = b.OwnerEmail,
                       Zip = b.Zip
                   };
        }
        private Task<BusinessOwner> GetOwner(string email) =>
             _context.BusinessOwners.FirstOrDefaultAsync(u => u.UserEmail.Equals(email));
    }
}