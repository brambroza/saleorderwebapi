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

    public class ListDataController : ApiController
    {
        // GET: api/ListData
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ListData/5
        public IHttpActionResult Get(string id)
        {
            string _QuatationNo = id;
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.[getlistdata] @ListName=" + _QuatationNo + "";
            dt = DB.DBConn.GetDataTable(_cmd);
            //string qdetail = string.Empty;
            //qdetail = JsonConvert.SerializeObject(dt);
            return Ok(dt);
        }
        // POST: api/ListData
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ListData/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ListData/5
        public void Delete(int id)
        {
        }
    }
}
