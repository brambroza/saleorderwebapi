using SaleorderWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaleorderWebApi.Controllers
{
    public class PromotionsController : ApiController
    {
        // GET: api/Promotions
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Promotions/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Promotions
        public void Post(promotions promotions)
        {
            try
            {
                string _cmd;
                _cmd = "exec  dbo.TPromotionsTrans";
                _cmd += "@CSUserUpd  ='" + promotions.CSUserUpd + "'";
                _cmd += ",@PromotionId =" + promotions.PromotionId;
                _cmd += ",@PromotionName  ='" + promotions.PromotionName + "'";
                _cmd += ",@CSProductCode  ='" + promotions.CSProductCode + "'";
                _cmd += ",@StartDate =" + promotions.StartDate;
                _cmd += ",@EndDate =" + promotions.EndDate;
                _cmd += ",@StatusActive =" + promotions.StatusActive;
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
