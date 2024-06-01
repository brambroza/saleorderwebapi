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
        public void Post(List<TPreSaleOrder_Detail> saleorderdetail    )
        {
            


            DB.DBConn.SqlConnectionOpen();
            DB.DBConn.Cmd = DB.DBConn.Cnn.CreateCommand();
            DB.DBConn.Tran = DB.DBConn.Cnn.BeginTransaction();

            try
            {

                string _cmd;
                if (saleorderdetail.Count > 0)
                {
                    _cmd = "Delete From dbo.TPreSaleOrder_Detail where CSSaleOrderNo='" + saleorderdetail[0].CSSaleOrderNo + "'";
                    DB.DBConn.ExecuteTran(_cmd, DB.DBConn.Cmd, DB.DBConn.Tran);
                }

                for (int i = 0; i < saleorderdetail.Count; i++)
                {
                    if (saleorderdetail[i].CSProductCode != "")
                    {

                        _cmd = "exec  dbo.TPreSaleOrder_Detail_Trans";
                        _cmd += " @CSUserIns  ='" + saleorderdetail[i].CSUserIns + "'";
                        _cmd += ",@CSSaleOrderNo  ='" + saleorderdetail[i].CSSaleOrderNo + "'";
                        _cmd += ",@CSProductCode  ='" + saleorderdetail[i].CSProductCode + "'";
                        _cmd += ",@CSUnitCode  ='" + saleorderdetail[i].CSUnitCode + "'";
                        _cmd += ",@CNQty =" + saleorderdetail[i].CNQty;
                        _cmd += ",@CNUnitPrice =" + saleorderdetail[i].CNUnitPrice;
                        _cmd += ",@CNDiscountPer1 =" + saleorderdetail[i].CNDiscountPer1;
                        _cmd += ",@CNDiscountPer2 =" + saleorderdetail[i].CNDiscountPer2;
                        _cmd += ",@CNDiscountPer3 =" + saleorderdetail[i].CNDiscountPer3; ;
                        _cmd += ",@CNDiscountAmt =" + saleorderdetail[i].CNDiscountAmt;
                        _cmd += ",@CNAmount =" + saleorderdetail[i].CNAmount;
                        _cmd += ",@Seq =" + +saleorderdetail[i].Seq;
                        _cmd += ",@FTStateFree ='" + saleorderdetail[i].FTStateFree + "'";
                        _cmd += ",@FTStateOther =" + 0;
                        //   DB.DBConn.ExecuteOnly(_cmd);



                        if (DB.DBConn.ExecuteTran(_cmd, DB.DBConn.Cmd, DB.DBConn.Tran) <= 0)
                        {
                            DB.DBConn.Tran.Rollback();
                            DB.DBConn.DisposeSqlTransaction(DB.DBConn.Tran);
                            DB.DBConn.DisposeSqlConnection(DB.DBConn.Cmd);
                        };
                    }



                }

                DB.DBConn.Tran.Commit();
                DB.DBConn.DisposeSqlTransaction(DB.DBConn.Tran);
                DB.DBConn.DisposeSqlConnection(DB.DBConn.Cmd);

            }
            catch (Exception)
            {
                DB.DBConn.Tran.Rollback();
                DB.DBConn.DisposeSqlTransaction(DB.DBConn.Tran);
                DB.DBConn.DisposeSqlConnection(DB.DBConn.Cmd);

            }


        }



        //public void Post(List<saleorderdetail> saleorderdetail)
        //{



        //    DB.DBConn.SqlConnectionOpen();
        //    DB.DBConn.Cmd = DB.DBConn.Cnn.CreateCommand();
        //    DB.DBConn.Tran = DB.DBConn.Cnn.BeginTransaction();

        //    try
        //    {

        //        string _cmd;
        //        if (saleorderdetail.Count > 0)
        //        {
        //            _cmd = "Delete From dbo.TPreSaleOrder_Detail where CSSaleOrderNo='" + saleorderdetail[0].docno + "'";
        //            DB.DBConn.ExecuteTran(_cmd, DB.DBConn.Cmd, DB.DBConn.Tran);
        //        }

        //        for (int i = 0; i < saleorderdetail.Count; i++)
        //        {

        //            _cmd = "exec  dbo.TPreSaleOrder_Detail_Trans";
        //            _cmd += " @CSUserIns  ='" + saleorderdetail[i].user + "'";
        //            _cmd += ",@CSSaleOrderNo  ='" + saleorderdetail[i].docno + "'";
        //            _cmd += ",@CSProductCode  ='" + saleorderdetail[i].productcode + "'";
        //            _cmd += ",@FNPcsQty =" + saleorderdetail[i].FNPcsQty;
        //            _cmd += ",@FNPcsPrice =" + saleorderdetail[i].FNPcsPrice;
        //            _cmd += ",@FNDozQty =" + saleorderdetail[i].FNDozQty;
        //            _cmd += ",@FNDozPrice =" + saleorderdetail[i].FNDozPrice;
        //            _cmd += ",@FNBoxQty =" + saleorderdetail[i].FNBoxQty;
        //            _cmd += ",@FNBoxPrice =" + saleorderdetail[i].FNBoxPrice;
        //            _cmd += ",@FNCaseQty =" + saleorderdetail[i].FNCaseQty;
        //            _cmd += ",@FNCasePrice =" + saleorderdetail[i].FNCasePrice;
        //            _cmd += ",@FNFreePcs =" + saleorderdetail[i].FNFreePcs;
        //            _cmd += ",@FNFreeDoz =" + saleorderdetail[i].FNFreeDoz;
        //            _cmd += ",@FNAmount =" + saleorderdetail[i].FNAmount;




        //            if (DB.DBConn.ExecuteTran(_cmd, DB.DBConn.Cmd, DB.DBConn.Tran) <= 0)
        //            {
        //                DB.DBConn.Tran.Rollback();
        //                DB.DBConn.DisposeSqlTransaction(DB.DBConn.Tran);
        //                DB.DBConn.DisposeSqlConnection(DB.DBConn.Cmd);
        //            };

        //        }

        //        DB.DBConn.Tran.Commit();
        //        DB.DBConn.DisposeSqlTransaction(DB.DBConn.Tran);
        //        DB.DBConn.DisposeSqlConnection(DB.DBConn.Cmd);

        //    }
        //    catch (Exception ex)
        //    {
        //        DB.DBConn.Tran.Rollback();
        //        DB.DBConn.DisposeSqlTransaction(DB.DBConn.Tran);
        //        DB.DBConn.DisposeSqlConnection(DB.DBConn.Cmd);

        //    }


        //}


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
