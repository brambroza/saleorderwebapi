using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaleorderWebApi.Controllers
{
    public class CustermerShiperController : ApiController
    {
        // GET: api/CustermerShiper
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CustermerShiper/5
        public IHttpActionResult Get(string CustomerCode)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.OMCustomerDelivery_Trans @CSCustomerCode='" + CustomerCode + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }


        // POST: api/CustermerShiper
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CustermerShiper/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CustermerShiper/5
        public void Delete(int id)
        {
        }
    }
}
