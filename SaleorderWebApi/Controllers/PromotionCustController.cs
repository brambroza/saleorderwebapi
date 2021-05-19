using SaleorderWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SaleorderWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    public class PromotionCustController : ApiController
    {
        // GET: api/PromotionCust
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PromotionCust/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PromotionCust
        public void Post(promotionscust promotionscust)
        {
            try
            {
                string _cmd;
                _cmd = "exec  dbo.TPromotions_Cust_Trans";
                _cmd += " @CSUserUpd  ='" + promotionscust.CSUserUpd + "'";
                _cmd += ",@PromotionId =" + promotionscust.PromotionId;
                _cmd += ",@Seq =" + promotionscust.Seq;
                _cmd += ",@CustomerCode ='" + promotionscust.CustomerCode + "'";
                DB.DBConn.ExecuteOnly(_cmd);

            }
            catch(Exception ex)
            {

            }
        }

        // PUT: api/PromotionCust/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PromotionCust/5
        public void Delete(int PromotionId , int Seq ,  string CustomerCode)
        {
            try
            {
                string _cmd;
                _cmd = "exec  dbo.TPromotions_Cust_Delete"; 
                _cmd += " @PromotionId =" + PromotionId ;
                _cmd += ",@Seq =" + Seq ;
                _cmd += ",@CustomerCode ='" + CustomerCode +"'" ;
                DB.DBConn.ExecuteOnly(_cmd);

            }
            catch (Exception ex)
            {

            }

        }
    }
}
