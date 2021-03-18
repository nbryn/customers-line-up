using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using CLup.Businesses.Interfaces;
using CLup.Context;
using CLup.Util;


namespace CLup.Businesses
{
    public class BusinessOwnerRepository : IBusinessOwnerRepository
    {
        private readonly ICLupContext _context;

        public BusinessOwnerRepository(ICLupContext context)
        {
            _context = context;
        }


        public async Task<ServiceResponse> CreateBusinessOwner(string ownerEmail)
        {
            BusinessOwner newOwner = new BusinessOwner
            {
                UserEmail = ownerEmail,
            };

            _context.BusinessOwners.Add(newOwner);

            await _context.SaveChangesAsync();

            return new ServiceResponse(HttpCode.Created);
        }

        public async Task<BusinessOwner> FindOwnerByEmail(string ownerEmail)
        {
            BusinessOwner owner = await _context.BusinessOwners.FirstOrDefaultAsync(x => x.UserEmail.Equals(ownerEmail));

            return owner;
        }

    }
}