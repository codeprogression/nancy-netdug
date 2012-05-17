using System.Collections.Generic;
using NancyDemo.Models;

namespace NancyDemo
{
    public class EventService
    {
        readonly Repository _repository;

        public EventService(Repository repository)
        {
            _repository = repository;
            Events = repository.Events;
        }

        public IList<CommunityEvent> Events { get; set; }
        public void AddEvent(CommunityEvent @event)
        {
            Events.Add(@event);
            _repository.SaveCollection(Events);
            Events = _repository.Events;
        }
    }
}