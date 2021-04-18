using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CLup.Businesses.DTO;
using CLup.Businesses.Interfaces;
using CLup.Context;
using CLup.Extensions;
using CLup.Util;
namespace CLup.Businesses
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly CLupContext _context;
        private readonly IMapper _mapper;
        public BusinessRepository(
            CLupContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> CreateBusiness(BusinessRequest business)
        {
            BusinessType.TryParse(business.Type, out BusinessType type);

            var newBusiness = _mapper.Map<Business>(business);

            _context.Businesses.Add(newBusiness);

            await _context.SaveChangesAsync();

            return new ServiceResponse(HttpCode.Created);
        }

        public async Task<ServiceResponse> UpdateBusiness(int businessId, BusinessRequest dto)
        {
            var business = await FindBusinessById(businessId);

            if (business == null)
            {
                return new ServiceResponse(HttpCode.NotFound);
            }

            var updatedBusiness = _mapper.Map<Business>(dto);
            updatedBusiness.Id = business.Id;

            _context.Entry(business).CurrentValues.SetValues(updatedBusiness);

            await _context.SaveChangesAsync();

            return new ServiceResponse(HttpCode.Updated);
        }

        public async Task<Business> FindBusinessById(int businessId)
        {
            Business business = await _context.Businesses.FirstOrDefaultAsync(x => x.Id == businessId);

            return business;
        }

        public async Task<ServiceResponse<IList<BusinessDTO>>> FindBusinessesByOwner(string ownerEmail)
        {
            var businesses = await _context.Businesses.Where(x => x.OwnerEmail.Equals(ownerEmail))
                                                                  .ToListAsync();
            return this.AssembleResponse<Business, BusinessDTO>(businesses, _mapper);
        }

        public async Task<ServiceResponse<IList<BusinessDTO>>> GetAll()
        {
            var businesses = await _context.Businesses.ToListAsync();

            return this.AssembleResponse<Business, BusinessDTO>(businesses, _mapper);
        }
    }
}