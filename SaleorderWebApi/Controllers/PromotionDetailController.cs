using SaleorderWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaleorderWebApi.Controllers
{
    public class PromotionDetailController : ApiController
    {
        // GET: api/PromotionDetail
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PromotionDetail/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PromotionDetail
        public void Post(promotiondetail promotiondetail )
        {
            try
            {
                string _cmd;
                _cmd = "exec  dbo.TPromotions_DTrans";
                _cmd += "@CSUserUpd  ='" + promotiondetail.CSUserUpd + "'";
                _cmd += ",@PromotionId =" + promotiondetail.PromotionId;
                _cmd += ",@Seq =" + promotiondetail.Seq;
                _cmd += ",@QuatityBuymin =" + promotiondetail.QuatityBuymin;
                _cmd += ",@Unitcode =" + promotiondetail.Unitcode;
                _cmd += ",@StateJoin =" + promotiondetail.StateJoin;
                _cmd += ",@PriceSale =" + promotiondetail.PriceSale;
                _cmd += ",@CSProductCode =" + promotiondetail.CSProductCode;
                _cmd += ",@QuatityFree =" + promotiondetail.QuatityFree;
                _cmd += ",@UnitcodeFree =" + promotiondetail.UnitcodeFree;
                _cmd += ",@DisAmout =" + promotiondetail.DisAmout;
                _cmd += ",@StateActive =" + promotiondetail.StateActive;
                DB.DBConn.ExecuteOnly(_cmd);


            }
            catch(Exception ex)
            {

            }
        }

        // PUT: api/PromotionDetail/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/PromotionDetail/5
        public void Delete(int promotionid ,  int seq )
        {
            string _cmd;
            _cmd = "exec  dbo.TPromotions_DTrans";
            _cmd += " @PromotionId =" + promotionid;
            _cmd += ",@Seq =" + seq;
            DB.DBConn.ExecuteOnly(_cmd);

        }
    }
}
