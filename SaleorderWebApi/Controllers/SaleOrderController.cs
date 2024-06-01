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
    public class SaleOrderController : ApiController
    {
        // GET: api/SaleOrder
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SaleOrder/5
        public IHttpActionResult Get(int CmpId ,string userlogin)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.saleorderlist   @CmpId=" + CmpId + ", @Username='" + userlogin  + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }

        public IHttpActionResult Get(int CmpId, string userlogin , string DocNo  )
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.saleorderheader   @CmpId=" + CmpId + ", @Username='" + userlogin + "',  @CSSaleOrderNo='" + DocNo + "'";
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }

      



        // POST: api/SaleOrder
        public void Post(saleorder so)
        {
            try
            {
                string _cmd;
                _cmd = "exec  dbo.TPreSaleOrderTrans";
                _cmd += " @CSUserIns  ='" + so.CSUserIns + "'";
                _cmd += ",@CSSaleOrderNo  ='" + so.CSSaleOrderNo + "'";
                _cmd += ",@CDSaleOrderDate ='" + so.CDSaleOrderDate +"'";
                _cmd += ",@CSCustomerCode  ='" + so.CSCustomerCode + "'";
                _cmd += ",@CSFactoryCode  ='" + so.CSFactoryCode + "'";
                _cmd += ",@CSCustomerType  ='" + so.CSCustomerType + "'";
                _cmd += ",@CSCustomDocNo  ='" + so.CSCustomDocNo + "'";
                _cmd += ",@CSPayType  ='" + so.CSPayType + "'";
                _cmd += ",@CDDeliveryDate ='" + so.CDDeliveryDate + "'"; ;
                _cmd += ",@CSDeliveryTime =" + so.CSDeliveryTime;
                _cmd += ",@CNSaleOrderAmt =" + so.CNSaleOrderAmt;
                _cmd += ",@CNSaleOrderDiscountPer1 =" + so.CNSaleOrderDiscountPer1;
                _cmd += ",@CNSaleOrderDiscountPer2 =" + so.CNSaleOrderDiscountPer2;
                _cmd += ",@CNSaleOrderDiscountPer3 =" + so.CNSaleOrderDiscountPer3;
                _cmd += ",@CNSaleOrderDiscountAmt1 =" + so.CNSaleOrderDiscountAmt1;
                _cmd += ",@CNSaleOrderDiscountAmt2 =" + so.CNSaleOrderDiscountAmt2;
                _cmd += ",@CNSaleOrderDiscountAmt3 =" + so.CNSaleOrderDiscountAmt3;
                _cmd += ",@CNSaleOrderNetAmt =" + so.CNSaleOrderNetAmt;
                _cmd += ",@CNSaleOrderVatPer =" + so.CNSaleOrderVatPer;
                _cmd += ",@CNSaleOrderVatAmt =" + so.CNSaleOrderVatAmt;
                _cmd += ",@CNSaleOrderGrandTotalAmt =" + so.CNSaleOrderGrandTotalAmt;
                _cmd += ",@CSRemark  ='" + so.CSRemark + "'";
                _cmd += ",@CNSaleOrderType =" + so.CNSaleOrderType;
                _cmd += ",@CSDeliveryAddress  ='" + so.CSDeliveryAddress + "'";
                _cmd += ",@FTStateNotConvert =" + so.FTStateNotConvert;
                _cmd += ",@FNMSysCmpId =" + so.FNMSysCmpId;
                _cmd += ",@GrpPrice=" + so.CNGrpCustomerId;
                _cmd += " ,@SendApp='" + so.FTStateSendApp + "'";
                DB.DBConn.ExecuteOnly(_cmd);

            }
            catch(Exception)
            {

            }
        }
        

        // PUT: api/SaleOrder/5
        public void Put(saleorder so , int state ,  string user   )
        {
            try
            {
                string _cmd;
                _cmd = "exec  dbo.TPreSaleOrderTrans";
                _cmd += " @CSUserIns  ='" + so.CSUserIns + "'";
                _cmd += ",@CSSaleOrderNo  ='" + so.CSSaleOrderNo + "'";
                _cmd += ",@CDSaleOrderDate ='" + so.CDSaleOrderDate + "'";
                _cmd += ",@CSCustomerCode  ='" + so.CSCustomerCode + "'";
                _cmd += ",@CSFactoryCode  ='" + so.CSFactoryCode + "'";
                _cmd += ",@CSCustomerType  ='" + so.CSCustomerType + "'";
                _cmd += ",@CSCustomDocNo  ='" + so.CSCustomDocNo + "'";
                _cmd += ",@CSPayType  ='" + so.CSPayType + "'";
                _cmd += ",@CDDeliveryDate ='" + so.CDDeliveryDate + "'"; ;
                _cmd += ",@CSDeliveryTime =" + so.CSDeliveryTime;
                _cmd += ",@CNSaleOrderAmt =" + so.CNSaleOrderAmt;
                _cmd += ",@CNSaleOrderDiscountPer1 =" + so.CNSaleOrderDiscountPer1;
                _cmd += ",@CNSaleOrderDiscountPer2 =" + so.CNSaleOrderDiscountPer2;
                _cmd += ",@CNSaleOrderDiscountPer3 =" + so.CNSaleOrderDiscountPer3;

                _cmd += ",@CNSaleOrderDiscountAmt1 =" + so.CNSaleOrderDiscountAmt1;
                _cmd += ",@CNSaleOrderDiscountAmt2 =" + so.CNSaleOrderDiscountAmt2;
                _cmd += ",@CNSaleOrderDiscountAmt3 =" + so.CNSaleOrderDiscountAmt3;
                _cmd += ",@CNSaleOrderNetAmt =" + so.CNSaleOrderNetAmt;
                _cmd += ",@CNSaleOrderVatPer =" + so.CNSaleOrderVatPer;
                _cmd += ",@CNSaleOrderVatAmt =" + so.CNSaleOrderVatAmt;
                _cmd += ",@CNSaleOrderGrandTotalAmt =" + so.CNSaleOrderGrandTotalAmt;
                _cmd += ",@CSRemark  ='" + so.CSRemark + "'";
                _cmd += ",@CNSaleOrderType =" + so.CNSaleOrderType;
                _cmd += ",@CSDeliveryAddress  ='" + so.CSDeliveryAddress + "'";
                _cmd += ",@FTStateNotConvert =" + so.FTStateNotConvert;
                _cmd += ",@FNMSysCmpId =" + so.FNMSysCmpId;
                DB.DBConn.ExecuteOnly(_cmd);

            }
            catch (Exception )
            {

            }
        }

        // DELETE: api/SaleOrder/5
        public void Delete(string SaleOrderNo)
        {
            try
            {
                string _cmd;
                _cmd = "Exec  dbo.TPreSaleOrderDelete   @SaleOrderNo='" + SaleOrderNo +"'" ;
                DB.DBConn.ExecuteOnly(_cmd);
            }
            catch
            {

            }
        }
    }
}
