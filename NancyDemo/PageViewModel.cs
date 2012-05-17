using System;
using System.Linq;

namespace NancyDemo
{
    public class PageViewModel
    {
        readonly MeetingService _meetingService;
        readonly EventService _eventService;

        public PageViewModel(MeetingService meetingService, EventService eventService)
        {
            _meetingService = meetingService;
            _eventService = eventService;
        }

        public dynamic GetModel(DateTime? dateTime)
        {
            var requestedDate = dateTime ?? DateTime.UtcNow;
            return new
                {
                    CurrentMonth = _meetingService.GetDetailsForMonth(requestedDate),
                    NextMonth = _meetingService.GetDetailsForMonth(requestedDate.AddMonths(1)),
                    YearlySchedule = _meetingService.GetFullSchedule(),
                    Events = _eventService.Events.OrderBy(x=>x.Id)
                };
        }

        
    }
}