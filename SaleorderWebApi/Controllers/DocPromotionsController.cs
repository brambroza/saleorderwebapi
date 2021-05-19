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
    public class DocPromotionsController : ApiController
    {
        // GET: api/DocPromotions
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/DocPromotions/5
        public IHttpActionResult Get(int cmpid, string DocNo)
        {
            DataTable dt = new System.Data.DataTable();
            string _docnew = "";
            string _cmd;
            _cmd = "Select Top 1  FNPriceVerId  as FTDocNo FROM  DK_MASTER.dbo.TPriceListVersion  where   FNPriceVerId =" + DocNo;
            dt = DB.DBConn.GetDataTable(_cmd);
            if (dt.Rows.Count > 0)
            {
                try { _docnew = dt.Rows[0][0].ToString(); } catch { _docnew = ""; }
            }


            if ((_docnew.ToString() == "") || (_docnew.ToLower() == "null"))
            {
                _cmd = "Select  NEXT VALUE FOR  dbo.pomotionsrun as FTDocNo  "; // + cmpid  ;
                dt = DB.DBConn.GetDataTable(_cmd);

            }


            return Ok(dt.Rows[0][0]);
        }


        // POST: api/DocPromotions
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DocPromotions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DocPromotions/5
        public void Delete(int id)
        {
        }
    }
}
