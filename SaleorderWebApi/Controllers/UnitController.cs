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
    public class UnitController : ApiController
    {
        // GET: api/Unit
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Unit/5
        public IHttpActionResult Get(int CmpId)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.unitlist   @CmpId=" + CmpId;
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);

        }
        // POST: api/Unit
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Unit/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Unit/5
        public void Delete(int id)
        {
        }
    }
}
