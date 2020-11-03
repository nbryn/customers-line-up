using System.Threading.Tasks;

using Logic.Context;
using Logic.Users;
using Logic.Users.Models;
using Logic.Businesses;
using Logic.Businesses.Models;

namespace Data
{
    public class BusinessRepository : IBusinessRepository
    {

        private readonly ICLupContext _context;

        public BusinessRepository(ICLupContext context)
        {
            _context = context;
        }

        public async Task<int> Register(CreateBusinessDTO business, UserDTO owner)
        {
            Business newBusiness = new Business
            {
                Name = business.Name,
                Owner = new User(owner),
                OwnerEmail = owner.Email,
                Zip = business.Zip,
            };
            _context.Businesses.Add(newBusiness);

            await _context.SaveChangesAsync();

            return newBusiness.Id;

        }

    }
}