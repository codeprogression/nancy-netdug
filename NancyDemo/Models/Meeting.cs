using System;
using System.Collections.Generic;

namespace NancyDemo.Models
{
    public class Meeting : Entity
    {
        public DateTime Date { get; set; }
        public IList<Topic> Topics { get; set; }

        public class Topic
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Speaker { get; set; }
        }
    }

    public class MeetingRequestModel
    {
        public DateTime Date { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Speaker { get; set; }

        public static implicit operator Meeting(MeetingRequestModel model)
        {
            return new Meeting()
                {
                    Date = model.Date,
                    Topics = new List<Meeting.Topic>
                        {
                            new Meeting.Topic
                                {
                                    Title = model.Title,
                                    Description = model.Description,
                                    Speaker = model.Speaker
                                }
                        }
                };
        }
    }
}