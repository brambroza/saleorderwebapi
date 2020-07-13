using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaleorderWebApi.Controllers
{
    public class SaleOrderDetailController : ApiController
    {
        // GET: api/SaleOrderDetail
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SaleOrderDetail/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SaleOrderDetail
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/SaleOrderDetail/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SaleOrderDetail/5
        public void Delete(int id)
        {
        }
    }
}
