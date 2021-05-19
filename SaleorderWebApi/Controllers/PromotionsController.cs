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
    public class PromotionsController : ApiController
    {
        // GET: api/Promotions
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Promotions/5
        public IHttpActionResult Get(string prodcode , string unitcode ,  int qty  , string customercode )
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.Use_Promotions @ProductCode='" + prodcode + "', @UnitCode ='" + unitcode + "' , @Qty=" + qty + ", @CustomerCode ='" + customercode + "' ";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }
        // POST: api/Promotions
        public void Post(promotions promotions)
        {
            try
            {
                string stateactive = "0";
                if (promotions.FTStateActive.ToString().ToLower() == "true")
                {
                    stateactive = "1";
                }
                string _cmd;
                _cmd = "exec  dbo.TPriceListVersion_Trans";
                _cmd += " @FTInsUser  ='" + promotions.FTInsUser + "'";
                _cmd += ",@FNPriceVerId =" +  promotions.FNPriceVerId;
                _cmd += ",@FTPriceVerName  ='" + promotions.FTPriceVerName + "'";
                _cmd += ",@FDStartDate  ='" + promotions.FDStartDate + "'";
                _cmd += ",@FDEndDate  ='" + promotions.FDEndDate + "'";
                _cmd += ",@FNMSysRawMatId =" + promotions.FNMSysRawMatId;
                _cmd += ",@CNGrpCustomerId =" + promotions.CNGrpCustomerId;
                _cmd += ",@CNCustomerId =" + promotions.CNCustomerId;
                _cmd += ",@FTStateActive ='" + stateactive + "'";
                DB.DBConn.ExecuteOnly(_cmd);

            }
            catch(Exception ex)
            {

            }
        }

        // PUT: api/Promotions/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Promotions/5
        public void Delete(int PromotionId)
        {
            string _cmd;
             _cmd = "exec  dbo.TPromotionsDelete";
            _cmd += " @PromotionId =" + PromotionId; 
            DB.DBConn.ExecuteOnly(_cmd);

        }
    }
}
