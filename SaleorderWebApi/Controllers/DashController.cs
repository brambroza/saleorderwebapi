﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaleorderWebApi.Controllers
{
    public class DashController : ApiController
    {
     

        // GET: api/Dash/5
        public IHttpActionResult Get(int CmpId , string username , int dtype)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
           switch (dtype)
            {
                case 0:
                    _cmd = "exec dbo.getsaledaily @CmpId=" + CmpId + " , @Username='" + username + "'";
                    break;
                case 1:
                    _cmd = "exec dbo.getsalemonth @CmpId=" + CmpId + " , @Username='" + username + "'";
                    break;
                case 2:
                    _cmd = "exec dbo.getTop10SaleProduct @CmpId=" + CmpId + " , @Username='" + username + "'";
                    break;
                case 3:
                    _cmd = "exec dbo.getsaleyear @CmpId=" + CmpId + " , @Username='" + username + "'";
                    break;
                default:
                    _cmd = "exec dbo.getsaledaily @CmpId=" + CmpId + " , @Username='" + username + "'";
                    break;                

            }
             dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }

         
    }
}
