using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading;
using Google.Apis.Util.Store;
using SaleorderWebApi.calendargoogle;

namespace SaleorderWebApi.Controllers
{
    public class GoogleCalendarController : ApiController
    {
        // GET: api/GoogleCalendar
        public IEnumerable<string> Get()
        {

            // If modifying these scopes, delete your previously saved credentials
            // at ~/.credentials/calendar-dotnet-quickstart.json
              string[] Scopes = { CalendarService.Scope.CalendarReadonly };
              string ApplicationName = "maservicenis";

           
                UserCredential credential;

                using (var stream =
                    new FileStream("client_secret_197509951570-2kc24m41d6sdgadhs5cbhadaksk0vv60.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }

                // Create Google Calendar API service.
                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                // Define parameters of request.
                EventsResource.ListRequest request = service.Events.List("primary");
                request.TimeMin = DateTime.Now;
                request.ShowDeleted = false;
                request.SingleEvents = true;
                request.MaxResults = 10;
                request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                // List events.
                Events events = request.Execute();
                Console.WriteLine("Upcoming events:");
                if (events.Items != null && events.Items.Count > 0)
                {
                    foreach (var eventItem in events.Items)
                    {
                        string when = eventItem.Start.DateTime.ToString();
                        if (String.IsNullOrEmpty(when))
                        {
                            when = eventItem.Start.Date;
                        }
                        Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                    }
                }
                else
                {
                    Console.WriteLine("No upcoming events found.");
                }
                Console.Read();

            




            return new string[] { "value1", "value2" };
        }

        // GET: api/GoogleCalendar/5
        public string Get(int id)
        {

            createEven();
            return "value";
        }

        // POST: api/GoogleCalendar
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GoogleCalendar/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GoogleCalendar/5
        public void Delete(int id)
        {
        }

        public void createEven()
        {
             
                string jsonFile = "maservicenis-99b261563ad6.json";
                string calendarId = @"dtlo6tnj1ggqg758hfk5tn903s@group.calendar.google.com";

                string[] Scopes = { CalendarService.Scope.Calendar };

                ServiceAccountCredential credential;

                using (var stream =
                    new FileStream(jsonFile, FileMode.Open, FileAccess.Read))
                {
                    var confg = Google.Apis.Json.NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(stream);
                    credential = new ServiceAccountCredential(
                       new ServiceAccountCredential.Initializer(confg.ClientEmail)
                       {
                           Scopes = Scopes
                       }.FromPrivateKey(confg.PrivateKey));
                }

                var service = new CalendarService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "maservicenis",
                });

                var calendar = service.Calendars.Get(calendarId).Execute();
                //Console.WriteLine("Calendar Name :");
                //Console.WriteLine(calendar.Summary);

                //Console.WriteLine("click for more .. ");
                //Console.Read();


                // Define parameters of request.
                EventsResource.ListRequest listRequest = service.Events.List(calendarId);
                listRequest.TimeMin = DateTime.Now;
                listRequest.ShowDeleted = false;
                listRequest.SingleEvents = true;
                listRequest.MaxResults = 10;
                listRequest.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

                // List events.
                Events events = listRequest.Execute();
                Console.WriteLine("Upcoming events:");
                if (events.Items != null && events.Items.Count > 0)
                {
                    foreach (var eventItem in events.Items)
                    {
                        string when = eventItem.Start.DateTime.ToString();
                        if (String.IsNullOrEmpty(when))
                        {
                            when = eventItem.Start.Date;
                        }
                        Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                    }
                }
                else
                {
                    Console.WriteLine("No upcoming events found.");
                }
                Console.WriteLine("click for more .. ");
                //Console.Read();

                var myevent = DB.Find(x => x.Id == "eventid" + 9);

                var InsertRequest = service.Events.Insert(myevent, calendarId);

                try
                {
                 

                InsertRequest.Execute();
                }
                  

            catch (Exception ext)
                {
                    try
                    {
                        service.Events.Update(myevent, calendarId, myevent.Id).Execute();
                        Console.WriteLine("Insert/Update new Event ");
                        Console.Read();

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("can't Insert/Update new Event ");

                    }
                }
            }


              List<Event> DB =
                 new List<Event>() {
                new Event(){
                    Id = "eventid" + 9,
                    Summary = "maservice",
                    Location = "288 bkk",
                    Description = "A chance to hear more about Google's developer products.",
                    Start = new EventDateTime()
                    {
                        DateTime = new DateTime(2021, 01, 13, 15, 30, 0),
                        TimeZone = "Asia/Bangkok",
                    },
                    End = new EventDateTime()
                    {
                        DateTime = new DateTime(2021, 01, 14, 15, 30, 0),
                        TimeZone = "Asia/Bangkok",
                    },
                     Recurrence = new List<string> { "RRULE:FREQ=DAILY;COUNT=2" },
                    Attendees = new List<EventAttendee>
                    {
                        new EventAttendee() { Email = "brambroza@gmail.com"} ,
                        new EventAttendee() { Email = "wcosbpw88@gmail.com"}
                    }
                }
                 };



       

    }
}
