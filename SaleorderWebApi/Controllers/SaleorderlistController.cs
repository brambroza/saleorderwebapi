using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaleorderWebApi.Controllers
{
    public class SaleorderlistController : ApiController
    {
        // GET: api/Saleorderlist
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Saleorderlist/5
        public IHttpActionResult Get(string username  , string SDate , string EDate)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.saleorderlist @Username='" + username + "'  , @SDate ='" + SDate + "',@EDate ='" + EDate + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }
        // POST: api/Saleorderlist
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Saleorderlist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Saleorderlist/5
        public void Delete(int id)
        {
        }
    }
}
