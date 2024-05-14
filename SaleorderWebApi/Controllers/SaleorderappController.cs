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
    public class SaleorderappController : ApiController
    {
        // GET: api/Saleorderapp
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Saleorderapp/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Saleorderapp
        public void Post(saleorderappM so)
        {
            try { 
                    string _cmd;
                    _cmd = "exec  dbo.TPreSaleOrderTransApp";
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
                    _cmd += ",@StateApp ='" + so.FTStateApp + "'";
                    DB.DBConn.ExecuteOnly(_cmd);

               
                
            }
            catch (Exception ex)
            {

            }
        }

        // PUT: api/Saleorderapp/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Saleorderapp/5
        public void Delete(int id)
        {
        }
    }
}
