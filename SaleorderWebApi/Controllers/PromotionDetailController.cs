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
    public class PromotionDetailController : ApiController
    {
        // GET: api/PromotionDetail
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/PromotionDetail/5
        public IHttpActionResult Get(int CmpId, int promotionid)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.getpromotionsdetail   @CmpId=" + CmpId + ", @PromotionId=" + promotionid + "";
            //foreach(DataRow r in dt.Rows)
            //{
            //    r["StateAccumulate"]
            //}
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }

        // POST: api/PromotionDetail
        public void Post(List<promotiondetail> promotiondetail )
        {



            DB.DBConn.SqlConnectionOpen();
            DB.DBConn.Cmd = DB.DBConn.Cnn.CreateCommand();
            DB.DBConn.Tran = DB.DBConn.Cnn.BeginTransaction();

            try
            {

                string _cmd;
                if (promotiondetail.Count > 0)
                {
                    _cmd = "Delete From DK_MASTER.dbo.TPriceListVersion_Detail where FNPriceVerId='" + promotiondetail[0].FNPriceVerId + "'";
                    DB.DBConn.ExecuteTran(_cmd, DB.DBConn.Cmd, DB.DBConn.Tran);
                }

                for (int i = 0; i < promotiondetail.Count; i++)
                {


                    string statejoin = "0";
                    string stateaccu = "0";
                    if (promotiondetail[i].StateJoin)
                    {
                        statejoin = "1";
                    }

                    if (promotiondetail[i].StateAccumulate)
                    {
                        stateaccu = "1";
                    }

                    _cmd = "exec  dbo.TPromotions_DTrans";
                    _cmd += " @UserLogin  ='" + promotiondetail[i].UserLogin + "'";
                    _cmd += ",@FNPriceVerId =" + promotiondetail[i].FNPriceVerId;
                    _cmd += ",@Seq =" + promotiondetail[i].Seq;
                    _cmd += ",@Description  ='" + promotiondetail[i].Description + "'";
                    _cmd += ",@Qty =" + promotiondetail[i].Qty;
                    _cmd += ",@Price =" + promotiondetail[i].Price;
                    _cmd += ",@CSUnitCode ='" + promotiondetail[i].CSUnitCode + "'";
                    _cmd += ",@DisAmt =" + promotiondetail[i].DisAmt;
                    _cmd += ",@QuatityFree =" + promotiondetail[i].QuatityFree;
                    _cmd += ",@UnitcodeFree ='" + promotiondetail[i].UnitcodeFree + "'";
                    _cmd += ",@StateJoin ='" + statejoin + "'";
                    _cmd += ",@StateAccumulate ='" + stateaccu + "'";
                    _cmd += ",@PointQty =" + promotiondetail[i].PointQty;
                    //DB.DBConn.ExecuteOnly(_cmd);

                    



                    if (DB.DBConn.ExecuteTran(_cmd, DB.DBConn.Cmd, DB.DBConn.Tran) <= 0)
                    {
                        DB.DBConn.Tran.Rollback();
                        DB.DBConn.DisposeSqlTransaction(DB.DBConn.Tran);
                        DB.DBConn.DisposeSqlConnection(DB.DBConn.Cmd);
                    };

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
