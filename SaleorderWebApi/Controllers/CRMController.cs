using SaleorderWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaleorderWebApi.Controllers
{
    public class CRMController : ApiController
    {
       [HttpGet]
        public IHttpActionResult GetCusomterlocation(int id, string user)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.customerlist_location @CmpId=" + id + ", @user ='" + user + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }

        [HttpGet]
        public IHttpActionResult GetSaleTrip(int id, string user)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.saletriplist @CmpId=" + id + ", @user ='" + user + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }

        [HttpPost]
        public void Checkin(CheckIn so)
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



            }
            catch (Exception ex)
            {

            }
        }


        [HttpPost]
        public void SetSaleTrip(SaleTrip so)
        {
            try
            {
                string _cmd;
                _cmd = "exec  dbo.set_SaleCheckIn";
                _cmd += " @CNCustomerId  =" + so.CNCustomerId + "";
                _cmd += ",@CDDateTrans  ='" + so.CDDateTrans + "'";
                _cmd += " , @Seq =" + so.Seq;
                _cmd += ",@CSUserLogin ='" + so.CSUserLogin + "'"; 
                _cmd += ",@ImagePath  ='" + so.ImagePath + "'";
                _cmd += ",@CSTime  ='" + so.CSTime + "'";
                _cmd += ",@StatusCheckIn  ='" + so.StatusCheckIn + "'";
                DB.DBConn.ExecuteOnly(_cmd);



            }
            catch (Exception ex)
            {

            }
        }




    }
}
