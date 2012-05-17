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
}