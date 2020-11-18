using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using Logic.DTO;
using Logic.BusinessOwners;
using Logic.Context;

namespace Data
{
    public class BusinessOwnerRepository : IBusinessOwnerRepository
    {
        private readonly ICLupContext _context;

        public BusinessOwnerRepository(ICLupContext context)
        {
            _context = context;
        }


        public async Task<BusinessOwner> CreateBusinessOwner(string ownerEmail)
        {
            BusinessOwner newOwner = new BusinessOwner
            {
                UserEmail = ownerEmail,
            };

            _context.BusinessOwners.Add(newOwner);

            await _context.SaveChangesAsync();

            return newOwner;
        }

        public async Task<BusinessOwner> FindOwnerByEmail(string ownerEmail)
        {
            BusinessOwner owner = await _context.BusinessOwners.FirstOrDefaultAsync(x => x.UserEmail.Equals(ownerEmail));

            return owner;
        }

    }
}