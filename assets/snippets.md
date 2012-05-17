#Snippets:



###Custom bootstrapper to handle {modulename}/views/{viewname}:


    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.ViewLocationConventions.Add((viewName, model, context) => 
                string.Concat(context.ModuleName, "/views/", viewName));
        }
    }

###Module route examples

    Get["/events"] = _ => Response.AsJson(repository.GetCollection<CommunityEvent>());
    Get["/meetings"] = _ => Response.AsJson(repository.GetCollection<Meeting>());
    Get["/meetings/current"] = _ =>
                {
                    var now = DateTime.UtcNow;
                    var meetings = repository.GetCollection<Meeting>();
                    var meeting = meetings.SingleOrDefault(x => x.Date.Month == now.Month 
                    	&& x.Date.Year == now.Year);
                    return Response.AsJson(meeting);
                };
    Get["/meetings/next"] = _ =>
                {
                    var now = DateTime.UtcNow;
                    var meetings = repository.GetCollection<Meeting>();
                    var meeting = meetings.SingleOrDefault(x => x.Date.Month == now.Month+1 
                    	&& x.Date.Year == now.Year);
                    if (meeting == null)
                        return new NotFoundResponse();
                    return Response.AsJson(new { date= meeting.Date.ToString("m"), 
                    	topics = meeting.Topics.Select(x=>new {x.Title, x.Speaker})});
                };
