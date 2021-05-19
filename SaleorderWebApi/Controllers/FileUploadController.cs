using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
namespace SaleorderWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FileUploadController : ApiController
    {
        // GET: api/FileUpload
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/FileUpload/5
        public IHttpActionResult Get(int CmpId)
        {
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.getfilepromotions   @CMPID=" + CmpId;
            dt = DB.DBConn.GetDataTable(_cmd);
            return Ok(dt);
        }

        // POST: api/FileUpload


        public async Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = System.Web.Hosting.HostingEnvironment.MapPath("~/Reports");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                int x = 0;
                foreach (MultipartFileData file in provider.FileData)
                {
                    x += +1;
                    var newname = DateTime.Now.ToString("yyyyMMddmmsss");
                    string pdfpath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Reports"), newname + x + ".pdf");
                    File.Move(file.LocalFileName, pdfpath);

                    string cmd = "";
                    cmd = "exec  dbo.sp_savefile @filename='" + pdfpath + "' , @name='" + file.Headers.ContentDisposition.Name.ToString() + "', @id=" + newname + x;
                    DB.DBConn.ExecuteOnly(cmd);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        // PUT: api/FileUpload/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FileUpload/5
        public void Delete(string id)
        {
            try
            {
                string _cmd;
                _cmd = "Exec  dbo.MFilePromotion  where  @name='" + id + "'";
                DB.DBConn.ExecuteOnly(_cmd);
            }
            catch
            {

            }
        }
    }
}
