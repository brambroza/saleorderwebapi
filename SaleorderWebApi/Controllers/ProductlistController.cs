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
    public class ProductlistController : ApiController
    {
        // GET: api/Productlist
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Productlist/5
        public IHttpActionResult Get(int CmpId  , string DocNo)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.productlist   @CmpId=" + CmpId + ", @DocNo ='" + DocNo + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }
        // POST: api/Productlist
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Productlist/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Productlist/5
        public void Delete(int id)
        {
        }
    }
}
