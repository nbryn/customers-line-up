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
        private readonly ICLupContext _context;
        private readonly IMapper _mapper;
        public BusinessRepository(
            ICLupContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> CreateBusiness(NewBusinessRequest business)
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

            return new ServiceResponse(HttpCode.Created);

        }

        public async Task<ServiceResponse> UpdateBusiness(int businessId, NewBusinessRequest dto)
        {
            var business = await FindBusinessById(businessId);

            if (business == null)
            {
                return new ServiceResponse(HttpCode.NotFound);
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