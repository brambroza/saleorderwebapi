using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaleorderWebApi.Controllers
{
    public class SaleorderappController : ApiController
    {
        // GET: api/Saleorderapp
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Saleorderapp/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Saleorderapp
        public void Post(string UserName , string SaleorderNo , string state)
        {
            try
            {
                string _cmd;
                _cmd = "Exec  dbo.saleorderapp  @UserName ='" + UserName + "',@SaleOrderNo='"+SaleorderNo+"', @State='" +state +"'";
                DB.DBConn.ExecuteOnly(_cmd);
            }
            catch (Exception ex)
            {

            }
        }

        // PUT: api/Saleorderapp/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Saleorderapp/5
        public void Delete(int id)
        {
        }
    }
}
