using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaleorderWebApi.Controllers
{
    public class CheckOnhandController : ApiController
    {
        // GET: api/CheckOnhand
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CheckOnhand/5
        public IHttpActionResult Get(string ProductCode , string UnitCode , int Qty)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.checkonhand   @ProductCode='" + ProductCode + "', @UnitCode='" + UnitCode + "' , @Qty=" + Qty ;
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }

        // POST: api/CheckOnhand
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CheckOnhand/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CheckOnhand/5
        public void Delete(int id)
        {
        }
    }
}
