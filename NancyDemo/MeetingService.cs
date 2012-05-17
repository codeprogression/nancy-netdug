using System;
using System.Collections.Generic;
using System.Linq;
using NancyDemo.Models;

namespace NancyDemo
{
    public class MeetingService
    {
        readonly Repository _repository;

        public MeetingService(Repository repository)
        {
            _repository = repository;
            Meetings = repository.Meetings.OrderByDescending(x=>x.Date).ToList();
        }

        public IList<Meeting> Meetings { get; set; }

        public void AddMeeting(Meeting meeting)
        {
            var existingMeeting = Meetings.SingleOrDefault(x => x.Date.Month == meeting.Date.Month && x.Date.Year == meeting.Date.Year);
            if (existingMeeting!=null)
            {
                foreach (var topic in meeting.Topics)
                {
                    existingMeeting.Topics.Add(topic);
                }
            }
            else
            {
                Meetings.Add(meeting);
            }
            _repository.SaveCollection(Meetings);
            Meetings = _repository.Meetings;
        }

        public dynamic GetFullSchedule()
        {
            var meetingYears = Meetings.Select(x => x.Date).Select(x => x.Year).Distinct();

            var schedule = new List<dynamic>();

            foreach(var year in meetingYears)
            {
                var thisYear = year;
                var meetings = GetMeetingsForYear(thisYear);
                schedule.Add(new {Year = thisYear, Meetings = meetings});
            }
            return schedule;
        }

        IList<dynamic> GetMeetingsForYear(int thisYear)
        {
            return Meetings.Where(x => x.Date.Year == thisYear).OrderByDescending(x => x.Date).Select(
                x => GetDetailsForMonth(x.Date)).ToList();
        }

        public dynamic GetDetailsForMonth(DateTime date)
        {
            var meeting =
                Meetings.SingleOrDefault(x => x.Date.Month == date.Month && x.Date.Year == date.Year);
            if (meeting==null)
                return null;
            return new
                {
                    meeting.Id,
                    Date = date.ToString("MMMM d"),
                    meeting.Topics
                };
        }
    }
}