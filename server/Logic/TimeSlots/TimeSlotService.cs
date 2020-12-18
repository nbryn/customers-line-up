using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Logic.Businesses;
using Logic.Context;
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


        public TimeSlotService(ITimeSlotRepository timeSlotRepository,
            IBusinessRepository businessRepository, IDTOMapper dtoMapper)
        {
            _businessRepository = businessRepository;
            _timeSlotRepository = timeSlotRepository;
            _dtoMapper = dtoMapper;
        }

        public async Task<Response> RemoveTimeSlot(string userEmail, int timeSlotId)
        {
            TimeSlot timeSlot = await _timeSlotRepository.FindTimeSlotById(timeSlotId);

            if (userEmail != timeSlot.Business.OwnerEmail)
            {
                return Response.Forbidden;
            }

            Response response = await _timeSlotRepository.DeleteTimeSlot(timeSlotId);

            return response;
        }

        public async Task<IEnumerable<TimeSlotDTO>> GenerateTimeSlots(CreateTimeSlotRequest request)
        {
            ICollection<TimeSlotDTO> TimeSlots = new List<TimeSlotDTO>();

            Business business = await _businessRepository.FindBusinessById(request.BusinessId);

            DateTime start = request.Start.AddHours(Double.Parse(business.Opens));

            DateTime closingTime = request.Start.AddHours(Double.Parse(business.Closes));

            for (DateTime date = start; date.Date <= request.End.Date; date = date.AddHours(request.TimeInterval))
            {
                // Only add TimeSlots when shop is open
                if (date.Equals(closingTime))
                {
                    closingTime = closingTime.AddHours(24);

                    date = date.AddHours((23 - date.Hour) + Double.Parse(business.Opens));

                    continue;
                }

                TimeSlot timeSlot = new TimeSlot
                {
                    BusinessId = business.Id,
                    BusinessName = business.Name,
                    Start = date,
                    End = date.AddHours(request.TimeInterval),
                    Capacity = business.Capacity,
                };

                var response = await _timeSlotRepository.CreateTimeSlot(timeSlot);

                timeSlot.Id = response.Item1;

                TimeSlotDTO dto = _dtoMapper.ConvertTimeSlotToDTO(timeSlot);

                TimeSlots.Add(dto);
            }

            return TimeSlots;
        }
        public async Task<ICollection<TimeSlotDTO>> GetAllTimeSlotsForBusiness(int businessId)
        {
            IList<TimeSlot> timeSlots = await _timeSlotRepository.FindTimeSlotsByBusiness(businessId);

            return timeSlots.Select(x => _dtoMapper.ConvertTimeSlotToDTO(x)).ToList();
        }
    }
}