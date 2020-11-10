using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

using Logic.Businesses.Models;
using Logic.BusinessOwners;
using Logic.BusinessOwners.Models;
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


        public async Task<int> CreateBusinessOwner(string ownerEmail)
        {
            BusinessOwner newOwner = new BusinessOwner
            {
                UserEmail = ownerEmail,
            };

            _context.BusinessOwners.Add(newOwner);

            await _context.SaveChangesAsync();

            return newOwner.Id;
        }

        public IQueryable<BusinessOwnerDTO> Read()
        {
            return from x in _context.BusinessOwners
                   select new BusinessOwnerDTO
                   {
                       UserEmail = x.UserEmail,
                       Businesses = from b in x.Businesses
                                    select new BusinessDTO
                                    {
                                        Id = b.Id,
                                        Name = b.Name,
                                        Zip = b.Zip,
                                        OwnerEmail = b.OwnerEmail
                                    }
                   };
        }

    }
}