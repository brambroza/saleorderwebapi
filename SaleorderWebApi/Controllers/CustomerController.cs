using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net; 
using System.Web.Http; 
using System.Web.Http.Cors;

namespace SaleorderWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomerController : ApiController
    {
        // GET: api/Customer
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Customer/5
        public IHttpActionResult Get(int id ,string user)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.customerlist @CmpId=" + id + ", @user ='" + user + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }


        // POST: api/Customer
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Customer/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Customer/5
        public void Delete(int id)
        {
        }
    }
}
