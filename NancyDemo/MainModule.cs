using System;
using Nancy;
using Nancy.Json;
using Nancy.Responses;
using Nancy.ModelBinding;
using NancyDemo.Models;

namespace NancyDemo
{
    public class MainModule : NancyModule
    {
        public MainModule(MeetingService meetingService, EventService eventService)
        {
            /* Get["/"] = _ =>
                 {
                    DateTime parsedDate;
                    var date = DateTime.TryParse(Context.Request.Query.Date, out parsedDate)
                                            ? parsedDate
                                            : (DateTime?) null;
                     var model = new PageViewModel(meetingService, eventService).GetModel(date);
                     return View["index", model];
                 };*/
            Get["/"] = _ =>
                {
                    DateTime parsedDate;
                    var date = DateTime.TryParse(Context.Request.Query.Date, out parsedDate)
                                            ? parsedDate
                                            : (DateTime?) null;
                    var model = new PageViewModel(meetingService, eventService).GetModel(date);
                    return View["knockout", new JavaScriptSerializer().Serialize(model)];
                };
            Get["/spec"] = _ => View["spec"];
            Get["/pagemodel"] = _ =>
                {
                    DateTime parsedDate;
                    var date = DateTime.TryParse(Context.Request.Query.Date, out parsedDate)
                                            ? parsedDate
                                            : (DateTime?) null;
                    var model = new PageViewModel(meetingService, eventService).GetModel(date);
                    return Response.AsJson((object)model);
                };

            Get["/meetings"] = _ => Response.AsJson(new
                {
                    Schedule = meetingService.GetFullSchedule(),
                });
            Post["/meetings"] = _ =>
                {
                    var meeting = this.Bind<MeetingRequestModel>();
                    meetingService.AddMeeting(meeting);
                    return new Response(){StatusCode = HttpStatusCode.Created};
                };

            Get["/events"] = _ => Response.AsJson(new
            {
                eventService.Events,
            });
            Post["/events"] = _ =>
                {
                    var @event = this.Bind<CommunityEvent>();
                    eventService.AddEvent(@event);
                    return new Response(){StatusCode = HttpStatusCode.Created};
                };
        }
    }
}