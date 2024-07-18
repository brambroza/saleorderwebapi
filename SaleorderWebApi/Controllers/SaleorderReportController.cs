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
    [RoutePrefix("api/SaleReport")]
    public class SaleorderReportController : ApiController
    {


        [HttpGet]
        [Route("getSaleSummaryByCustomer")]
        public IHttpActionResult getSaleSummary(int cmpid, string user , string customerCode)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.get_SaleSummary_ByCustomer @CmpId=" + cmpid + ", @user ='" + user + "' , @cust='" + customerCode + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }

        [HttpGet]
        [Route("getSaleProductByCustomer")]
        public IHttpActionResult getSaleProduct(int cmpid, string user, string customerCode , string yearmount)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.get_SaleProduct_ByCustomer @CmpId=" + cmpid + ", @user ='" + user + "' , @cust='" + customerCode + "' , @yearmonth='" + yearmount + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }


        [HttpGet]
        [Route("getSaleProductTopProduct")]
        public IHttpActionResult getSaleTop5Product(int cmpid, string user )
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.[get_sale_permonth_top5Product] @CmpId=" + cmpid + ", @username ='" + user + "' ";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }

        [HttpGet]
        [Route("getSaleTotalPerMonth")]
        public IHttpActionResult getSaleTotalPerMonth(int cmpid, string user)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.[get_sale_permonth] @CmpId=" + cmpid + ", @username ='" + user + "' ";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }

        [HttpGet]
        [Route("getSaleTop10Customer")]
        public IHttpActionResult getSaleTop10Customer(int cmpid, string user)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.[get_sale_permonth_top10Customer] @CmpId=" + cmpid + ", @username ='" + user + "' ";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }
    ///// sale report daily 
    ///
    [HttpGet]
        [Route("getSaleDaily")]
        public IHttpActionResult getSaleDaily(String SDate , String EDate ,  string user , string salecode)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.[SP_GET_SumSale_Daily_ForMobile]  @StartDate='" + SDate + "', @EndDate ='" + EDate + "' , @userlogin='" + user + "' , @salecode='" + salecode + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }

        [HttpGet]
        [Route("getSaleTrip")]
        public IHttpActionResult getSaleTrip(String SDate, String EDate, string user, string salecode)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.[SP_GET_SumSale_Trip_ForMobile]  @StartDate='" + SDate + "', @EndDate ='" + EDate + "' , @userlogin='" + user + "' , @salecode='" + salecode + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }

        [HttpGet]
        [Route("getSaleDailySup")]
        public IHttpActionResult getSaleDailySup(String SDate, String EDate, string user, string salecode)
        {

           
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.[SP_GET_SumSale_Daily_sup_ForMobile]  @StartDate='" + SDate + "', @EndDate ='" + EDate + "' , @userlogin='" + user + "' , @salecode='" + salecode + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }


        [HttpGet]
        [Route("getSaleTripSup")]
        public IHttpActionResult getSaleTripSup(String SDate, String EDate, string user, string salecode)
        {


            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.[SP_GET_SumSale_Daily_Trip_ForMobile]  @StartDate='" + SDate + "', @EndDate ='" + EDate + "' , @userlogin='" + user + "' , @salecode='" + salecode + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }

    }
}
