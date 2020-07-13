using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaleorderWebApi.Controllers
{
    public class SaleOrderController : ApiController
    {
        // GET: api/SaleOrder
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SaleOrder/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SaleOrder
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SaleOrder/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SaleOrder/5
        public void Delete(int id)
        {
        }
    }
}
