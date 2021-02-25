using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Logic.Businesses;
using Data;
using Logic.DTO;
using Logic.Util;

namespace Logic.TimeSlots
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly IBusinessRepository _businessRepository;
        private readonly IDTOMapper _dtoMapper;

        public TimeSlotService(
            ITimeSlotRepository timeSlotRepository,
            IBusinessRepository businessRepository, 
            IDTOMapper dtoMapper
            )
        {
            _businessRepository = businessRepository;
            _timeSlotRepository = timeSlotRepository;
            _dtoMapper = dtoMapper;
        }

        public async Task<HttpCode> RemoveTimeSlot(string userEmail, int timeSlotId)
        {
            TimeSlot timeSlot = await _timeSlotRepository.FindTimeSlotById(timeSlotId);

            if (userEmail != timeSlot.Business.OwnerEmail)
            {
                return HttpCode.Forbidden;
            }

            HttpCode response = await _timeSlotRepository.DeleteTimeSlot(timeSlotId);

            return response;
        }

        public async Task<QueryResult> GenerateTimeSlots(GenerateTimeSlotsRequest request)
        {
            Business business = await _businessRepository.FindBusinessById(request.BusinessId);

            if (business == null)
            {
                return new QueryResult(HttpCode.NotFound, "Business not found");
            }

            IList<TimeSlot> existingTimeSlots = await _timeSlotRepository.FindTimeSlotsByBusinessAndDate(request.BusinessId, request.Start);

            if (existingTimeSlots.Count() > 0)
            {
               return new QueryResult(HttpCode.Conflict, "Time slots already generated for this date");
            }

            DateTime opens = request.Start.AddHours(Double.Parse(business.Opens));

            DateTime closes = request.Start.AddHours(Double.Parse(business.Closes));

            for (DateTime date = opens; date.TimeOfDay <= closes.TimeOfDay; date = date.AddMinutes(business.TimeSlotLength))
            {
                // Only add TimeSlots when shop is open
        /*         if (date.Equals(closingTime))
                {
                    closingTime = closingTime.AddHours(24);

                    date = date.AddHours((23 - date.Hour) + Double.Parse(business.Opens));

                    continue;
                } */

                TimeSlot timeSlot = new TimeSlot
                {
                    BusinessId = business.Id,
                    BusinessName = business.Name,
                    Start = date,
                    End = date.AddMinutes(business.TimeSlotLength),
                    Capacity = business.Capacity,
                };

                var response = await _timeSlotRepository.CreateTimeSlot(timeSlot);
            }

            return new QueryResult(HttpCode.Created);
        }
    }
}