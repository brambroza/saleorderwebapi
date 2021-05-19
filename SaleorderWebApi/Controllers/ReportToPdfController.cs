using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using SaleorderWebApi.Etpdf;


namespace SaleorderWebApi.Controllers
{
    public class ReportToPdfController : ApiController
    {
        // GET: api/ReportToPdf
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ReportToPdf/5
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage ExportReport(string id)
        {
            // Users user = new Users();

            CrystalReportPdfResult c = new CrystalReportPdfResult(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Reports"), "saleorder.rpt"));
          
           
          
            string EmailTosend = "" ;// = WebUtility.UrlDecode(user.Email);
           // List<Users> model = new List<Users>();
           // var data = cX.tbl_Registration;
            var rd = new   ReportDocument();

           
            rd.Load(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Reports"), "saleorder.rpt"));
            ConnectionInfo connectInfo = new ConnectionInfo()
            {
                ServerName = "NOHF",
                DatabaseName = "DB_Payroll",
                UserID = "sa",
                Password = "1234"
            };
            rd.SetDatabaseLogon("sa", "1234");
            foreach (Table tbl in rd.Database.Tables)
            {
                tbl.LogOnInfo.ConnectionInfo = connectInfo;
                tbl.ApplyLogOnInfo(tbl.LogOnInfo);
            }

            rd.ExportToDisk(ExportFormatType.PortableDocFormat, (Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Reports"), "invoice.pdf")));
            // rd.SetDataSource(model);


            //using (var stream = rd.ExportToStream(ExportFormatType.PortableDocFormat))
            //{
            //    SmtpClient smtp = new SmtpClient
            //    {
            //        Port = 587,
            //        UseDefaultCredentials = true,
            //        Host = "smtp.gmail.com",
            //        EnableSsl = true
            //    };

            //    smtp.UseDefaultCredentials = false;
            //    smtp.Credentials = new NetworkCredential("debendra256@gmail.com", "9853183297");
            //    var message = new System.Net.Mail.MailMessage("debendra256@gmail.com", EmailTosend, "User Registration Details", "Hi Please check your Mail  and find the attachement.");
            //    message.Attachments.Add(new Attachment(stream, "UsersRegistration.pdf"));

            //    smtp.Send(message);
            //}

             var Message = string.Format("Report Created and sended to your Mail.");
            HttpResponseMessage response1 = Request.CreateResponse(HttpStatusCode.OK, Message);
            return response1;
        }


        // POST: api/ReportToPdf
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ReportToPdf/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ReportToPdf/5
        public void Delete(int id)
        {
        }

       
        public void genreport()
        {
            string _path = HttpContext.Current.Server.MapPath("\\Reports\\marketing\\invoice.rpt");
            

           
            ReportDocument crystalReport = new ReportDocument();
           
            try {
                crystalReport.Load(Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Reports/marketing"), "invoice.rpt"));
            } catch(CrystalDecisions.Shared.CrystalReportsException ex)
            {

            }

            try
            {
                crystalReport.Load(_path); //Load crystal Report
            }
            catch (Exception ex)
            {

                string msg = "The report file " + _path +
                " can not be loaded, ensure that the report exists or the path is correct." +
                "\nException:\n" + ex.Message +
                "\nSource:" + ex.Source +
                "\nStacktrace:" + ex.StackTrace;
                throw new Exception(msg);
            }


        }
    }
}
