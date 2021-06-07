using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;

using CLup.Businesses;
using CLup.Businesses.Interfaces;
using CLup.TimeSlots.DTO;
using CLup.TimeSlots.Interfaces;
using CLup.Util;

namespace CLup.TimeSlots
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IMapper _mapper;

        public TimeSlotService(
            ITimeSlotRepository timeSlotRepository,
            IBusinessRepository businessRepository,
            IMapper mapper)
        {
            _businessRepository = businessRepository;
            _timeSlotRepository = timeSlotRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> RemoveTimeSlot(string userEmail, string timeSlotId)
        {
            TimeSlot timeSlot = await _timeSlotRepository.FindTimeSlotById(timeSlotId);

            if (userEmail != timeSlot.Business.OwnerEmail)
            {
                return new ServiceResponse(HttpCode.Forbidden);
            }

            var response = await _timeSlotRepository.DeleteTimeSlot(timeSlotId);

            return response;
        }

        public async Task<ServiceResponse> GenerateTimeSlots(GenerateTimeSlotsRequest request)
        {
            Business business = await _businessRepository.FindBusinessById(request.BusinessId);

            if (business == null)
            {
                return new ServiceResponse(HttpCode.NotFound, "Business not found");
            }

            var existingTimeSlots = await _timeSlotRepository.FindTimeSlotsByBusinessAndDate(request.BusinessId, request.Start);

            if (existingTimeSlots._response.Count() > 0)
            {
                return new ServiceResponse(HttpCode.Conflict, "Time slots already generated for this date");
            }

            DateTime opens = request.Start.AddHours(Double.Parse(business.Opens.Substring(0, business.Opens.IndexOf("."))));
            DateTime closes = request.Start.AddHours(Double.Parse(business.Closes.Substring(0, business.Closes.IndexOf("."))));

            for (DateTime date = opens; date.TimeOfDay <= closes.TimeOfDay; date = date.AddMinutes(business.TimeSlotLength))
            {
                // Only add TimeSlots when shop is open
                /*         if (date.Equals(closingTime))
                        {
                            closingTime = closingTime.AddHours(24);

                            date = date.AddHours((23 - date.Hour) + Double.Parse(business.Opens));

                            continue;
                        } */

                var timeSlot = _mapper.Map<TimeSlot>(business);

                timeSlot.Start = date;
                timeSlot.End = date.AddMinutes(business.TimeSlotLength);

                var response = await _timeSlotRepository.CreateTimeSlot(timeSlot);
            }

            return new ServiceResponse(HttpCode.Created);
        }
    }
}