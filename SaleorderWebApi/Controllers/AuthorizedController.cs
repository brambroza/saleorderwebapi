using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaleorderWebApi.Controllers
{
    public class AuthorizedController : ApiController
    {
        // GET: api/Authorized
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Authorized/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Authorized
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Authorized/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Authorized/5
        public void Delete(int id)
        {
        }
    }
}
