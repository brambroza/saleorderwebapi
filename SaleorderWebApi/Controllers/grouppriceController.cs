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
    public class grouppriceController : ApiController
    {
        // GET: api/groupprice
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/groupprice/5
        public IHttpActionResult Get(int CmpId)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.grouppricelist   @CmpId=" + CmpId  ;
            //foreach(DataRow r in dt.Rows)
            //{
            //    r["StateAccumulate"]
            //}
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }


        // POST: api/groupprice
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/groupprice/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/groupprice/5
        public void Delete(int id)
        {
        }
    }
}
