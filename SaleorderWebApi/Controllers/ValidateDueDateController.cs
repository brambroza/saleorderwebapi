using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SaleorderWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValidateDueDateController : ApiController
    {
        // GET: api/ValidateDueDate
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ValidateDueDate/5
        public IHttpActionResult Get(int cmpid , string docno )
        {
            CultureInfo _Culture = new CultureInfo("en-US", true); 
            _Culture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";

            System.Threading.Thread.CurrentThread.CurrentCulture = _Culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = _Culture;


            DateTime dateTime = DateTime.Now ;
            DayOfWeek today = dateTime.DayOfWeek;
            if (today == DayOfWeek.Sunday)
            {
                dateTime = dateTime.AddDays(8);

            }
            else if (today == DayOfWeek.Saturday)
            {
                dateTime = dateTime.AddDays(9);
            }

            else
            {
                if (int.Parse(dateTime.ToString("HHmmss")) >= 163000)
                {
                    dateTime = dateTime.AddDays(8);
                }
                else
                {
                    dateTime = dateTime.AddDays(7);
                }

            }
            today = dateTime.DayOfWeek;
            if (today == DayOfWeek.Sunday)
            {
                dateTime = dateTime.AddDays(1);
            }
            else if (today == DayOfWeek.Saturday)
            {
                dateTime = dateTime.AddDays(2);
            }

            return Ok(dateTime.ToString("yyyy-MM-dd"));

          
        }

        // POST: api/ValidateDueDate
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ValidateDueDate/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ValidateDueDate/5
        public void Delete(int id)
        {
        }
    }
}
