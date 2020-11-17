using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        public async Task<Business> CreateBusiness(CreateBusinessDTO business, string ownerEmail)
        {
            Business newBusiness = new Business
            {
                Name = business.Name,
                Owner = business.Owner,
                OwnerEmail = ownerEmail,
                Capacity = business.Capacity,
                OpeningTime = business.OpeningTime,
                ClosingTime = business.ClosingTime,
                Zip = business.Zip
            };

            _context.Businesses.Add(newBusiness);

            await _context.SaveChangesAsync();

            return newBusiness;

        }

        public async Task<Business> FindBusinessById(int businessId)
        {
            Business business = await _context.Businesses.FindAsync(businessId);

            return business;
        }

       public async Task<IList<Business>> GetAll()
        {
            return await _context.Businesses.ToListAsync();
        }

    }
}