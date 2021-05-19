using SaleorderWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SaleorderWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class OnhandlistController : ApiController
    {
        // GET: api/Onhandlist
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Onhandlist/5
        public IHttpActionResult Get(int CmpId, string prodcode)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.sp_getOnhand   @CmpId=" + CmpId + ", @RawmatCode='" + prodcode + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }


        // POST: api/Onhandlist
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Onhandlist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Onhandlist/5
        public void Delete(int id)
        {
        }
    }
}
