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
   
    public class SaleOrderDetailController : ApiController
    {
        // GET: api/SaleOrderDetail
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SaleOrderDetail/5
        public IHttpActionResult Get(int CmpId , string Saleorderno)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.saleorderdetail @Saleorderno='" + Saleorderno + "' , @CmpId=" + CmpId;
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }


        // POST: api/SaleOrderDetail
        public void Post(saleorderdetail saleorderdetail )
        {
            try
            {
                string _cmd;
                _cmd = "exec  dbo.TPreSaleOrder_Detail_Trans";
                _cmd += "@CSUserIns  ='" + saleorderdetail.CSUserIns + "'";
                _cmd += ",@CSSaleOrderNo  ='" + saleorderdetail.CSSaleOrderNo + "'";
                _cmd += ",@CSProductCode  ='" + saleorderdetail.CSProductCode + "'";
                _cmd += ",@CSUnitCode  ='" + saleorderdetail.CSUnitCode + "'";
                _cmd += ",@CNQty =" + saleorderdetail.CNQty;
                _cmd += ",@CNUnitPrice =" + saleorderdetail.CNUnitPrice;
                _cmd += ",@CNDiscountPer1 =" + saleorderdetail.CNDiscountPer1;
                _cmd += ",@CNDiscountPer2 =" + saleorderdetail.CNDiscountPer2;
                _cmd += ",@CNDiscountPer3 =" + saleorderdetail.CNDiscountPer3;
                _cmd += ",@CNDiscountAmt =" + saleorderdetail.CNDiscountAmt;
                _cmd += ",@CNAmount =" + saleorderdetail.CNAmount;
                _cmd += ",@Seq =" + saleorderdetail.Seq;
                _cmd += ",@FTStateFree =" + saleorderdetail.FTStateFree;
                _cmd += ",@FTStateOther =" + saleorderdetail.FTStateOther;
                DB.DBConn.ExecuteOnly(_cmd);             


            }
            catch(Exception ex)
            {



            }
        }

        // PUT: api/SaleOrderDetail/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SaleOrderDetail/5
        public void Delete(string SaleOrderNo , int Seq , string ProductCode)
        {
            try
            {
                string _cmd;
                _cmd = "Exec  dbo.TPreSaleOrder_D_Delete   @SaleOrderNo='" + SaleOrderNo + "' ,@ProductCode='" + ProductCode+"',@Seq="+Seq;
                DB.DBConn.ExecuteOnly(_cmd);
            }
            catch
            {

            }
        }
    }
}
