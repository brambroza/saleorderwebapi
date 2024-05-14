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
    public class CouponController : ApiController
    {
        // GET: api/Coupon
        [HttpGet]
        [Route("api/Coupon")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Coupon/5
        [HttpGet]
        [Route("api/Coupon")]
        public IHttpActionResult Get(int id )
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.getCoupon @CmpId=" + id + " ";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }


        [HttpGet]
        [Route("api/CouponUse")]
        public IHttpActionResult getCouponuse(int CmpId, string couponcode , string CustCode)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.getCouponUse @CmpId=" + CmpId + " , @Code='" + couponcode + "', @CustCode='" + CustCode + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }


        // POST: api/Coupon
        [HttpPost]
        [Route("api/Coupon")]
        public IHttpActionResult Post(coupon coupon )
        {
            MsgReturn msgReturn = new MsgReturn();
            try
            {
                
                if (coupon.FNMSysCouponId == 0 )
                {
                    coupon.FNMSysCouponId = int.Parse( DB.DBConn.GetField("exec   DK_SYSTEM.dbo. SP_GET_SYSID @tablename = 'TPOMCoupon' , @FieldName = 'FNMSysCouponId' , @DbName = 'DK_MASTER'  "));
                }
                string _cmd = "";
                _cmd = "exec  dbo.TPOMCoupon_Trans"; 
                _cmd += " @FTInsUser  ='" + coupon.FTInsUser + "'"; 
                _cmd += ",@FNMSysCouponId =" + coupon.FNMSysCouponId; 
                _cmd += ",@FTCouponCode =" + coupon.FTCouponCode; 
                _cmd += ",@FTDescription  ='" + coupon.FTDescription + "'"; 
                _cmd += ",@FDStartDate ='" + coupon.FDStartDate + "'";
                _cmd += ",@FDEndDate ='" + coupon.FDEndDate + "'";
                _cmd += ",@FTStateActive =" + coupon.FTStateActive; 
                _cmd += ",@FNDisAmt =" + coupon.FNDisAmt;
               if ( DB.DBConn.ExecuteOnly(_cmd))
                {
                    msgReturn.ReturnCode = "200";
                    msgReturn.Msg = "Success !!";
                    return Ok(msgReturn);
                }
                else
                {
                    msgReturn.ReturnCode = "404";
                    msgReturn.Msg = "Error !!";
                    return Ok(msgReturn);

                }


            }
            catch {
                msgReturn.ReturnCode = "404";
                msgReturn.Msg = "Error !!";
                return Ok(msgReturn);

            }
        }

        // PUT: api/Coupon/5
        [HttpPut]
        [Route("api/Coupon")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Coupon/5
        [HttpDelete]
        [Route("api/Coupon")]
        public IHttpActionResult Delete(string  id)
        {
            MsgReturn msgReturn = new MsgReturn();
            try
            {
                string _cmd = "";
                _cmd = "delete from DK_MASTER.dbo.TPOMCoupon where FTCouponCode='" + id + "'";

                if (DB.DBConn.ExecuteOnly(_cmd))
                {
                    msgReturn.ReturnCode = "200";
                    msgReturn.Msg = "Delete Success !!";
                    return Ok(msgReturn);
                }
                else
                {
                    msgReturn.ReturnCode = "404";
                    msgReturn.Msg = "Delete Error !!";
                    return Ok(msgReturn);

                }


            }
            catch
            {
                msgReturn.ReturnCode = "404";
                msgReturn.Msg = "Delete Error !!";
                return Ok(msgReturn);

            }
        }
    }
}
