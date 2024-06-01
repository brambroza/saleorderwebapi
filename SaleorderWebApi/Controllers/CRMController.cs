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
    [RoutePrefix("api/CRM")]
    public class CRMController : ApiController
    {

        [HttpGet]
        [Route("GetDashSummary")]
        public IHttpActionResult GetDashSummary(int cmpid, string user)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.[getsalemonth_status] @CmpId=" + cmpid + ", @username ='" + user + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }



        [HttpGet]
        [Route("GetCustomerlocation")]
        public IHttpActionResult GetCustomerlocation(int cmpid, string user)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.customerlist_location @CmpId=" + cmpid + ", @user ='" + user + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }


        [HttpGet]
        [Route("GetCustomerWareHouselocation")]
        public IHttpActionResult GetCustomerWHlocation(int cmpid, string user, string customercode)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.customerlist_warehouse_location @CmpId=" + cmpid + ", @user ='" + user + "' , @CustomerCode='" + customercode + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }


        [HttpGet]
        [Route("GetCheckInHistory")]
        public IHttpActionResult GetCheckInHistory(int cmpid, string user , string customercode)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.customerlist_history_checkin @CmpId=" + cmpid + ", @user ='" + user + "' , @CustomerCode='" + customercode + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }



        [HttpGet]
        [Route("GetSaleTrip")]
        public IHttpActionResult GetSaleTrip(int id, string user)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.saletriplist @CmpId=" + id + ", @user ='" + user + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }


       

        [HttpPost]
        [Route("CheckIn")]
        public IHttpActionResult Checkin(CheckIn so)
        {
            try
            {
                string _cmd;
                _cmd = "exec  dbo.set_SaleCheckIn";
                _cmd += " @CNCustomerId  =" + so.CNCustomerId + "";
                _cmd += ",@CDDateTrans  ='" + so.CDDateTrans + "'";
                _cmd += ",@Latitude =" + so.Latitude + "";
                _cmd += ",@Longitude  =" + so.Longitude + "";
                _cmd += ",@ImagePath  ='" + so.ImagePath + "'";
                _cmd += ",@CSTime  ='" + so.CSTime + "'";
                _cmd += ",@CSUserLogin ='" + so.CSUserLogin + "'";
                DB.DBConn.ExecuteOnly(_cmd);

               

                return Ok("Check-in processed successfully.");

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        [HttpPost]
        [Route("CheckInWarehouse")]
        public IHttpActionResult CheckinWarehouse(CheckInWarehouse so)
        {
            try
            {
                string _cmd;
                _cmd = "exec  dbo.set_SaleCheckInWareHouse";
                _cmd += " @CNCustomerId  =" + so.CNCustomerId + "";
                _cmd += " , @CNDeliveryId  =" + so.CNDeliveryId + "";
                _cmd += ",@CDDateTrans  ='" + so.CDDateTrans + "'";
                _cmd += ",@Latitude =" + so.Latitude + "";
                _cmd += ",@Longitude  =" + so.Longitude + "";
                _cmd += ",@ImagePath  ='" + so.ImagePath + "'";
                _cmd += ",@CSTime  ='" + so.CSTime + "'";
                _cmd += ",@CSUserLogin ='" + so.CSUserLogin + "'";
                DB.DBConn.ExecuteOnly(_cmd);

                return Ok("Check-in processed successfully.");

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }




        [HttpPost]
        [Route("SetSaleTrip")]
        public IHttpActionResult SetSaleTrip(SaleTrip so)
        {
            try
            {
                string _cmd;
                _cmd = "exec  dbo.set_SaleTrip";
                _cmd += " @CNCustomerId  =" + so.CNCustomerId + "";
                _cmd += ",@CDDateTrans  ='" + so.CDDateTrans + "'";
                _cmd += " ,@Seq =" + so.Seq;
                _cmd += ",@CSUserLogin ='" + so.CSUserLogin + "'"; 
                _cmd += ",@ImagePath  ='" + so.ImagePath + "'";
                _cmd += ",@CSTime  ='" + so.CSTime + "'";
                _cmd += ",@StatusCheckIn  ='" + so.StatusCheckIn + "'";
                _cmd += ", @Description ='" + so.Description + "'";
                DB.DBConn.ExecuteOnly(_cmd);

                return Ok("Set Sale Trip Success");

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        
        [HttpDelete]

        [Route("DelSaleTrip")]
        public IHttpActionResult DelSaleTrip(SaleTrip so)
        {
            try
            {
                string _cmd;
                _cmd = "exec  dbo.del_SaleTrip";
                _cmd += " @CNCustomerId  =" + so.CNCustomerId + "";
                _cmd += ",@CDDateTrans  ='" + so.CDDateTrans + "'";
                _cmd += " ,@Seq =" + so.Seq;
                _cmd += ",@CSUserLogin ='" + so.CSUserLogin + "'";
               
                
                DB.DBConn.ExecuteOnly(_cmd);

                return Ok("del Sale Trip Success");

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpPost]
        [Route("OrderAppMobile")]
        public IHttpActionResult OrderAppMobile(saleappMobile so)
        {
            try
            {
                string _cmd;
                _cmd = "exec  dbo.TPreSaleOrderTransApp_Mobile";
                _cmd += " @UserLogin  ='" + so.UserLogin + "'";
                _cmd += ",@CSSaleOrderNo  ='" + so.CSSaleOrderNo + "'"; 
                _cmd += ",@StateApp ='" + so.StateApp + "'";
                
                DB.DBConn.ExecuteOnly(_cmd);

                return Ok("Approve Success");

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }




    }
}
