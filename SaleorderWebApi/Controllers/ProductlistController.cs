using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaleorderWebApi.Controllers
{
    public class ProductlistController : ApiController
    {
        // GET: api/Productlist
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Productlist/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Productlist
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Productlist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Productlist/5
        public void Delete(int id)
        {
        }
    }
}
