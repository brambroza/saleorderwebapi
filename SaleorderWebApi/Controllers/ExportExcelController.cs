using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;


namespace SaleorderWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/ExportExcel")]
    public class ExportExcelController : ApiController
    {

        [HttpGet]
        [Route("reportSaleDaily")]
        public HttpResponseMessage ExportToExcelSaleDaily(String SDate , String EDate , String Username)
        {
            // Sample DataTable
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.[SP_GET_SumSale_Daily_ForMobile_Excel]  @StartDate='" + SDate + "', @EndDate ='" + EDate + "' , @userlogin='" + Username + "'";
           // _cmd = "SELECT TOP (200) CNPositionId, CSPositionCode, CSPositionName 'ชื่อ', CSStateActive  , 98 as 'รวม' FROM     MPosition";
            dt = DB.DBConn.GetDataTable(_cmd);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Create Excel package
            using (ExcelPackage package = new ExcelPackage())
            {
                // Add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                worksheet.Cells.Style.Font.Name = "Tahoma"; // Set font name
                worksheet.Cells.Style.Font.Size = 8; // Set font size
                worksheet.Cells.Style.Font.Color.SetColor(Color.Black);

                // Load the datatable into the sheet, starting from cell A1.
                worksheet.Cells["A1"].LoadFromDataTable(dt, true);
                var row = worksheet.Cells["A1:E1"];
                row.Style.Fill.PatternType = ExcelFillStyle.Solid;
                row.Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                int lastRowNum = dt.Rows.Count + 1; // +1 because the header is also included

                // Set number format for column E
                worksheet.Column(5).Style.Numberformat.Format = "#,##0.00"; // Column E is the 5th column

                // Highlight the last row
                lastRowNum = lastRowNum + 1;
                var lastRow = worksheet.Cells["A" +  lastRowNum + ":E" + lastRowNum];
                lastRow.Style.Fill.PatternType = ExcelFillStyle.Solid;
                lastRow.Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                // Calculate the sum of column E
                string sumFormula = $"SUM(E2:E{lastRowNum - 1})"; // Sum all rows except the header and last row
                worksheet.Cells[lastRowNum, 5].Formula = sumFormula; // E column is the 5th column

                worksheet.Cells.AutoFitColumns();

                ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("Sheet2");

                // Sample data
                DataTable dt2 = new DataTable();
                _cmd = " exec dbo.[SP_GET_SumSale_Daily_ForMobile]   @StartDate='" + SDate + "', @EndDate ='" + EDate + "' , @userlogin='" + Username + "'";
                dt2 = DB.DBConn.GetDataTable(_cmd);

                worksheet1.Cells.Style.Font.Name = "Tahoma"; // Set font name
                worksheet1.Cells.Style.Font.Size = 8; // Set font size
                worksheet1.Cells.Style.Font.Color.SetColor(Color.Black);

                DateTime _sdate = DateTime.ParseExact(SDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                string sformattedDate = _sdate.ToString("dd/MM/yyyy");

                DateTime _edate = DateTime.ParseExact(EDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                string eformattedDate = _edate.ToString("dd/MM/yyyy");

                worksheet1.Cells["A1:H1"].Merge = true;
                worksheet1.Cells["A1:H1"].Value = "สรุปยอดขายสินค้า ประจำวันที่ " + sformattedDate + " - " + eformattedDate + "";

                worksheet1.Cells["A2:B2"].Merge = true;
                worksheet1.Cells["A2:B2"].Value = "รหัสเชล";

                worksheet1.Cells["C2"].Value = "ผู้แทน";
                worksheet1.Cells["D2"].Value = "จังหวัด";
                worksheet1.Cells["E2"].Value = "รหัสลูกค้า";
                worksheet1.Cells["F2"].Value = "ชื่อลูกค้า";
                worksheet1.Cells["G2"].Value = "จำนวน Order";
                worksheet1.Cells["H2"].Value = "จำนวนเงินสุทธิ";

                worksheet1.Cells["A1:H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet1.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet1.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet1.Cells["A2:H2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet1.Cells["A2:H2"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet1.Cells["A2:H2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;




                int rowStart = 3;
                decimal amt = 0;
                decimal totalamt = 0;
                int count = 0;
                var dtSaleCode = dt2.AsEnumerable()
                                      .Select(row5 => row5.Field<string>("CSSaleCode"))
                                      .Distinct().OrderBy(saleCode => saleCode);

                foreach (var saleCode in dtSaleCode)
                {

                    worksheet1.Cells["A"+ rowStart + ":H" + rowStart + ""].Merge = true;
                    worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Value = "รหัสเชล :" + saleCode + "";
                    worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                  
                    rowStart++;
                    // Filter rows by the current sale code
                    var filteredRows = dt2.AsEnumerable()
                                          .Where(row5 => row5.Field<string>("CSSaleCode") == saleCode);
                    amt = 0;
                    count = 0;
                    foreach (var row1 in filteredRows)
                    {
                        // Process each row as needed
                        // Example: write data to the worksheet
                        amt = amt +  row1.Field<decimal>("amount");
                        totalamt = totalamt + row1.Field<decimal>("amount");
                        count = count + row1.Field<int>("FNOrderCount");
                        worksheet1.Cells["B" + rowStart].Value = row1.Field<string>("CSSaleCode");
                        worksheet1.Cells["C" + rowStart].Value = row1.Field<string>("CNSaleName");
                        worksheet1.Cells["D" + rowStart].Value = row1.Field<string>("FTProvinceName");
                        worksheet1.Cells["E" + rowStart].Value = row1.Field<string>("CSCustomerCode");
                        worksheet1.Cells["F" + rowStart].Value = row1.Field<string>("CSCustomerName");
                        worksheet1.Cells["G" + rowStart].Value = row1.Field<int>("FNOrderCount");
                        worksheet1.Cells["H" + rowStart].Value = row1.Field<string>("CNSaleOrderGrandTotalAmt");
                        worksheet1.Cells["H" + rowStart].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet1.Cells["H" + rowStart].Style.Numberformat.Format = "#,##0.00";
                        rowStart++;
                    }

                    worksheet1.Cells["B" + rowStart + ":F" + rowStart + ""].Merge = true;
                    worksheet1.Cells["B" + rowStart + ":H" + rowStart + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet1.Cells["B" + rowStart + ":H" + rowStart + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet1.Cells["G" + rowStart].Value = count;
                    worksheet1.Cells["H" + rowStart].Value = "รวม " + amt.ToString("N2");
                    worksheet1.Cells["H" + rowStart].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    rowStart++;

                }


                worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Merge = true;
                worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Value = " " + totalamt.ToString("N2");
                worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                worksheet1.Cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet1.Cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet1.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet1.Cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;

                Color lightGreyColor = Color.FromArgb(158, 158, 158);
                worksheet1.Cells.Style.Border.Top.Color.SetColor(lightGreyColor);
                worksheet1.Cells.Style.Border.Bottom.Color.SetColor(lightGreyColor);
                worksheet1.Cells.Style.Border.Right.Color.SetColor(lightGreyColor);
                worksheet1.Cells.Style.Border.Left.Color.SetColor(lightGreyColor);
                worksheet1.Cells.AutoFitColumns();




                // Define the path where the file will be saved

                string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                string filenameis = fileName;
                string filePath = Path.Combine(@"D:\reportexportexcel", fileName);
                if (!Directory.Exists(@"D:\reportexportexcel"))
                {
                    Directory.CreateDirectory(@"D:\reportexportexcel");
                }




                // Save the Excel package to the specified path
                FileInfo file = new FileInfo(filePath);
                package.SaveAs(file);

                // Create the response
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(File.ReadAllBytes(filePath))
                };

                // Set the content type and disposition
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = filenameis
                };

                return response;
            }

         
        }


        [HttpGet]
        [Route("reportSaleTrip")]
        public HttpResponseMessage ExportToExcelSaleTrip(String SDate, String EDate, String Username)
        {
            // Sample DataTable
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.[SP_GET_SumSale_Trip_ForMobile_Excel]  @StartDate='" + SDate + "', @EndDate ='" + EDate + "' , @userlogin='" + Username + "'";
            // _cmd = "SELECT TOP (200) CNPositionId, CSPositionCode, CSPositionName 'ชื่อ', CSStateActive  , 98 as 'รวม' FROM     MPosition";
            dt = DB.DBConn.GetDataTable(_cmd);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Create Excel package
            using (ExcelPackage package = new ExcelPackage())
            {
                // Add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                worksheet.Cells.Style.Font.Name = "Tahoma"; // Set font name
                worksheet.Cells.Style.Font.Size = 8; // Set font size
                worksheet.Cells.Style.Font.Color.SetColor(Color.Black);

                // Load the datatable into the sheet, starting from cell A1.
                worksheet.Cells["A1"].LoadFromDataTable(dt, true);
                var row = worksheet.Cells["A1:E1"];
                row.Style.Fill.PatternType = ExcelFillStyle.Solid;
                row.Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                int lastRowNum = dt.Rows.Count + 1; // +1 because the header is also included

                // Set number format for column E
                worksheet.Column(5).Style.Numberformat.Format = "#,##0.00"; // Column E is the 5th column

                // Highlight the last row
                lastRowNum = lastRowNum + 1;
                var lastRow = worksheet.Cells["A" + lastRowNum + ":E" + lastRowNum];
                lastRow.Style.Fill.PatternType = ExcelFillStyle.Solid;
                lastRow.Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                // Calculate the sum of column E
                string sumFormula = $"SUM(E2:E{lastRowNum - 1})"; // Sum all rows except the header and last row
                worksheet.Cells[lastRowNum, 5].Formula = sumFormula; // E column is the 5th column

                worksheet.Cells.AutoFitColumns();

                ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("Sheet2");

                // Sample data
                DataTable dt2 = new DataTable();
                _cmd = " exec dbo.[SP_GET_SumSale_SumTrip_ForMobile_Excel]   @StartDate='" + SDate + "', @EndDate ='" + EDate + "' , @userlogin='" + Username + "'";
                dt2 = DB.DBConn.GetDataTable(_cmd);

                worksheet1.Cells.Style.Font.Name = "Tahoma"; // Set font name
                worksheet1.Cells.Style.Font.Size = 8; // Set font size
                worksheet1.Cells.Style.Font.Color.SetColor(Color.Black);

                DateTime _sdate = DateTime.ParseExact(SDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                string sformattedDate = _sdate.ToString("dd/MM/yyyy");

                DateTime _edate = DateTime.ParseExact(EDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                string eformattedDate = _edate.ToString("dd/MM/yyyy");

                worksheet1.Cells["A1:H1"].Merge = true;
                worksheet1.Cells["A1:H1"].Value = "สรุปยอดขายสินค้า ประจำวันที่ " + sformattedDate + " - " + eformattedDate + "";

                worksheet1.Cells["A2:B2"].Merge = true;
                worksheet1.Cells["A2:B2"].Value = "รหัสเชล";

                worksheet1.Cells["C2"].Value = "ผู้แทน";
                worksheet1.Cells["D2"].Value = "จังหวัด";
                worksheet1.Cells["E2"].Value = "รหัสลูกค้า";
                worksheet1.Cells["F2"].Value = "ชื่อลูกค้า";
                worksheet1.Cells["G2"].Value = "จำนวน Order";
                worksheet1.Cells["H2"].Value = "จำนวนเงินสุทธิ";

                worksheet1.Cells["A1:H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet1.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet1.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet1.Cells["A2:H2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet1.Cells["A2:H2"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet1.Cells["A2:H2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;




                int rowStart = 3;
                decimal amt = 0;
                decimal totalamt = 0;
                int count = 0;
                var dtSaleCode = dt2.AsEnumerable()
                                      .Select(row5 => row5.Field<string>("CSSaleCode"))
                                      .Distinct().OrderBy(saleCode => saleCode);

                foreach (var saleCode in dtSaleCode)
                {

                    worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Merge = true;
                    worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Value = "รหัสเชล :" + saleCode + "";
                    worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                    rowStart++;
                    // Filter rows by the current sale code
                    var filteredRows = dt2.AsEnumerable()
                                          .Where(row5 => row5.Field<string>("CSSaleCode") == saleCode);
                    amt = 0;
                    count = 0;
                    foreach (var row1 in filteredRows)
                    {
                        // Process each row as needed
                        // Example: write data to the worksheet
                        amt = amt + row1.Field<decimal>("amount");
                        totalamt = totalamt + row1.Field<decimal>("amount");
                        count = count + row1.Field<int>("FNOrderCount");
                        worksheet1.Cells["B" + rowStart].Value = row1.Field<string>("CSSaleCode");
                        worksheet1.Cells["C" + rowStart].Value = row1.Field<string>("CNSaleName");
                        worksheet1.Cells["D" + rowStart].Value = row1.Field<string>("FTProvinceName");
                        worksheet1.Cells["E" + rowStart].Value = row1.Field<string>("CSCustomerCode");
                        worksheet1.Cells["F" + rowStart].Value = row1.Field<string>("CSCustomerName");
                        worksheet1.Cells["G" + rowStart].Value = row1.Field<int>("FNOrderCount");
                        worksheet1.Cells["H" + rowStart].Value = row1.Field<string>("CNSaleOrderGrandTotalAmt");
                        worksheet1.Cells["H" + rowStart].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet1.Cells["H" + rowStart].Style.Numberformat.Format = "#,##0.00";
                        rowStart++;
                    }

                    worksheet1.Cells["B" + rowStart + ":F" + rowStart + ""].Merge = true;
                    worksheet1.Cells["B" + rowStart + ":H" + rowStart + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet1.Cells["B" + rowStart + ":H" + rowStart + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet1.Cells["G" + rowStart].Value = count;
                    worksheet1.Cells["H" + rowStart].Value = "รวม " + amt.ToString("N2");
                    worksheet1.Cells["H" + rowStart].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    rowStart++;

                }


                worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Merge = true;
                worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Value = " " + totalamt.ToString("N2");
                worksheet1.Cells["A" + rowStart + ":H" + rowStart + ""].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                worksheet1.Cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet1.Cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet1.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet1.Cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;

                Color lightGreyColor = Color.FromArgb(158, 158, 158);
                worksheet1.Cells.Style.Border.Top.Color.SetColor(lightGreyColor);
                worksheet1.Cells.Style.Border.Bottom.Color.SetColor(lightGreyColor);
                worksheet1.Cells.Style.Border.Right.Color.SetColor(lightGreyColor);
                worksheet1.Cells.Style.Border.Left.Color.SetColor(lightGreyColor);
                worksheet1.Cells.AutoFitColumns();




                // Define the path where the file will be saved

                string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                string filenameis = fileName;
                string filePath = Path.Combine(@"D:\reportexportexcel", fileName);
                if (!Directory.Exists(@"D:\reportexportexcel"))
                {
                    Directory.CreateDirectory(@"D:\reportexportexcel");
                }




                // Save the Excel package to the specified path
                FileInfo file = new FileInfo(filePath);
                package.SaveAs(file);

                // Create the response
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(File.ReadAllBytes(filePath))
                };

                // Set the content type and disposition
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = filenameis
                };

                return response;
            }


        }


         
        [HttpGet]
        [Route("reportSupDaily")]
        public HttpResponseMessage ExportToExcelSupDaily(String SDate, String EDate, String Username)
        {
            // Sample DataTable
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.[SP_GET_Report_Detail_Sup_Daily_ForMobile]  @StartDate='" + SDate + "', @EndDate ='" + EDate + "' , @userlogin='" + Username + "'";
            // _cmd = "SELECT TOP (200) CNPositionId, CSPositionCode, CSPositionName 'ชื่อ', CSStateActive  , 98 as 'รวม' FROM     MPosition";
            dt = DB.DBConn.GetDataTable(_cmd);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Create Excel package
            using (ExcelPackage package = new ExcelPackage())
            {
                // Add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                worksheet.Cells.Style.Font.Name = "Tahoma"; // Set font name
                worksheet.Cells.Style.Font.Size = 8; // Set font size
                worksheet.Cells.Style.Font.Color.SetColor(Color.Black);

                

                worksheet.Cells["A1:D1"].Merge = true;
                worksheet.Cells["A1:D1"].Value = "วันที่";

                worksheet.Cells["E1"].Value = "รหัสเชล";
                worksheet.Cells["F1"].Value = "ผู้แทน";
                worksheet.Cells["G1"].Value = "จำนวนร้านค้า";
                worksheet.Cells["H1"].Value = "จำนวนเงินสุทธิ";
                worksheet.Cells["I1"].Value = "รหัส Sup";
                worksheet.Cells["J1"].Value = "ชื่อ Sup";


                worksheet.Cells["A1:J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A1:J1"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet.Cells["A1:J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var dtSupCode = dt.AsEnumerable()
                                       .GroupBy(row => new
                                       {
                                           CSEmpCodeSup = row.Field<string>("CSEmpCodeSup"),
                                           CSEmpNameSup = row.Field<string>("CSEmpNameSup")
                                       })
                                       .OrderBy(group => group.Key.CSEmpCodeSup);

                int rowSupIndex = 2;
                int supCount = 0;
                int saleCount = 0;

                decimal supAmt = 0;
                decimal saleAmt = 0;

                int totalCount = 0;
                decimal totalAmt = 0;
                
                foreach (var SupRow in dtSupCode)
                {

                    supCount = 0;
                    supAmt = 0;
                    worksheet.Cells["A" + rowSupIndex + ":J" + rowSupIndex + ""].Merge = true;
                    worksheet.Cells["A" + rowSupIndex + ":J" + rowSupIndex + ""].Value = "รหัส Sup :" + SupRow.Key.CSEmpCodeSup + "";
                    worksheet.Cells["A" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells["A" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    rowSupIndex++;

                    worksheet.Cells["B" + rowSupIndex + ":J" + rowSupIndex + ""].Merge = true;
                    worksheet.Cells["B" + rowSupIndex + ":J" + rowSupIndex + ""].Value = "ชื่อ Sup :" + SupRow.Key.CSEmpNameSup + "";
                    worksheet.Cells["B" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells["B" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    rowSupIndex++;

                    var subSaleCode = SupRow.AsEnumerable()
                                      .GroupBy(row => new
                                      {
                                          CSSaleCode = row.Field<string>("CSSaleCode"), 
                                      })
                                      .OrderBy(group => group.Key.CSSaleCode);

                    foreach (var subSaleRow in subSaleCode)
                    {
                        worksheet.Cells["C" + rowSupIndex + ":J" + rowSupIndex + ""].Merge = true;
                        worksheet.Cells["C" + rowSupIndex + ":J" + rowSupIndex + ""].Value = "รหัสเชล :" + subSaleRow.Key.CSSaleCode + "";
                        worksheet.Cells["C" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["C" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        rowSupIndex++;
                        saleCount = 0;
                        saleAmt = 0; 
                        foreach (var subrow in subSaleRow)
                        {
                            saleCount = saleCount + subrow.Field<int>("FNOrderCount");
                            saleAmt = saleAmt  + subrow.Field<decimal>("amount");
                            supAmt = supAmt + subrow.Field<decimal>("amount");
                            supCount = supCount + subrow.Field<int>("FNOrderCount");

                            totalAmt = totalAmt + subrow.Field<decimal>("amount");
                            totalCount = totalCount + subrow.Field<int>("FNOrderCount");


                            worksheet.Cells["D" + rowSupIndex].Value = subrow.Field<string>("CDOrderDate");
                            worksheet.Cells["E" + rowSupIndex].Value = subrow.Field<string>("CSSaleCode");
                            worksheet.Cells["F" + rowSupIndex].Value = subrow.Field<string>("CNSaleName");
                            worksheet.Cells["G" + rowSupIndex].Value = subrow.Field<int>("FNOrderCount");
                            worksheet.Cells["H" + rowSupIndex].Value = subrow.Field<decimal>("amount");
                            worksheet.Cells["H" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            worksheet.Cells["H" + rowSupIndex].Style.Numberformat.Format = "#,##0.00";

                            worksheet.Cells["I" + rowSupIndex].Value = subrow.Field<string>("CSEmpCodeSup");
                            worksheet.Cells["J" + rowSupIndex].Value = subrow.Field<string>("CSEmpNameSup");
                            rowSupIndex++;


                        }
                        worksheet.Cells["D" + rowSupIndex + ":F" + rowSupIndex + ""].Merge = true;
                        worksheet.Cells["D" + rowSupIndex + ":J" + rowSupIndex + ""] .Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["D" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet.Cells["G" + rowSupIndex].Value = saleCount; 
                        worksheet.Cells["H" + rowSupIndex].Value = "รวม " + saleAmt.ToString("N2");
                        worksheet.Cells["H" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        rowSupIndex++;

                    }

                    worksheet.Cells["B" + rowSupIndex + ":F" + rowSupIndex + ""].Merge = true;
                    worksheet.Cells["B" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells["B" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet.Cells["G" + rowSupIndex].Value = supCount;
                    worksheet.Cells["H" + rowSupIndex].Value = "รวม " + supAmt.ToString("N2");
                    worksheet.Cells["H" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    rowSupIndex++;

                }
                worksheet.Cells["A" + rowSupIndex + ":F" + rowSupIndex + ""].Merge = true;
                worksheet.Cells["A" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet.Cells["G" + rowSupIndex].Value = totalCount;
                worksheet.Cells["H" + rowSupIndex].Value = "รวม " + totalAmt.ToString("N2");
                worksheet.Cells["H" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                rowSupIndex++;



                worksheet.Cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;

                Color lightGreyColor1 = Color.FromArgb(158, 158, 158);
                worksheet.Cells.Style.Border.Top.Color.SetColor(lightGreyColor1);
                worksheet.Cells.Style.Border.Bottom.Color.SetColor(lightGreyColor1);
                worksheet.Cells.Style.Border.Right.Color.SetColor(lightGreyColor1);
                worksheet.Cells.Style.Border.Left.Color.SetColor(lightGreyColor1);
                worksheet.Cells.AutoFitColumns();


                 
                ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("Sheet2");

                // Sample data
                DataTable dt2 = new DataTable();
                _cmd = " exec dbo.[SP_GET_Report_Sum_Sup_Daily_ForMobile]   @StartDate='" + SDate + "', @EndDate ='" + EDate + "' , @userlogin='" + Username + "'";
                dt2 = DB.DBConn.GetDataTable(_cmd);

                worksheet1.Cells.Style.Font.Name = "Tahoma"; // Set font name
                worksheet1.Cells.Style.Font.Size = 8; // Set font size
                worksheet1.Cells.Style.Font.Color.SetColor(Color.Black);

                DateTime _sdate = DateTime.ParseExact(SDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                string sformattedDate = _sdate.ToString("dd/MM/yyyy");

                DateTime _edate = DateTime.ParseExact(EDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                string eformattedDate = _edate.ToString("dd/MM/yyyy");



                worksheet1.Cells["A1:D1"].Merge = true;
                worksheet1.Cells["A1:D1"].Value = "รหัสเชล";

             
                worksheet1.Cells["E1"].Value = "ผู้แทน";
                worksheet1.Cells["F1"].Value = "จังหวัด";
                worksheet1.Cells["G1"].Value = "รหัสลูกค้า";
                worksheet1.Cells["H1"].Value = "ชื่อลูกค้า";
                worksheet1.Cells["I1"].Value = "จำนวน Order";
                worksheet1.Cells["J1"].Value = "จำนวนเงินสุทธิ";
                worksheet1.Cells["K1"].Value = "รหัส Sup";
                worksheet1.Cells["L1"].Value = "ชื่อ Sup";


                worksheet1.Cells["A1:L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet1.Cells["A1:L1"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet1.Cells["A1:L1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var dtSupCode2 = dt2.AsEnumerable()
                                       .GroupBy(row => new
                                       {
                                           CSEmpCodeSup = row.Field<string>("CSEmpCodeSup"),
                                           CSEmpNameSup = row.Field<string>("CSEmpNameSup")
                                       })
                                       .OrderBy(group => group.Key.CSEmpCodeSup);

                  rowSupIndex = 2;
                  supCount = 0;
                  saleCount = 0;

                  supAmt = 0;
                  saleAmt = 0;

                  totalCount = 0;
                  totalAmt = 0;

                foreach (var SupRow in dtSupCode2)
                {

                    supCount = 0;
                    supAmt = 0;
                    worksheet1.Cells["A" + rowSupIndex + ":L" + rowSupIndex + ""].Merge = true;
                    worksheet1.Cells["A" + rowSupIndex + ":L" + rowSupIndex + ""].Value = "รหัส Sup :" + SupRow.Key.CSEmpCodeSup + "";
                    worksheet1.Cells["A" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet1.Cells["A" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    rowSupIndex++;

                    worksheet1.Cells["B" + rowSupIndex + ":L" + rowSupIndex + ""].Merge = true;
                    worksheet1.Cells["B" + rowSupIndex + ":L" + rowSupIndex + ""].Value = "ชื่อ Sup :" + SupRow.Key.CSEmpNameSup + "";
                    worksheet1.Cells["B" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet1.Cells["B" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    rowSupIndex++;

                    var subSaleCode = SupRow.AsEnumerable()
                                      .GroupBy(row => new
                                      {
                                          CSSaleCode = row.Field<string>("CSSaleCode"),
                                      })
                                      .OrderBy(group => group.Key.CSSaleCode);

                    foreach (var subSaleRow in subSaleCode)
                    {
                        worksheet1.Cells["C" + rowSupIndex + ":L" + rowSupIndex + ""].Merge = true;
                        worksheet1.Cells["C" + rowSupIndex + ":L" + rowSupIndex + ""].Value = "รหัสเชล :" + subSaleRow.Key.CSSaleCode + "";
                        worksheet1.Cells["C" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet1.Cells["C" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        rowSupIndex++;
                        saleCount = 0;
                        saleAmt = 0;
                        foreach (var subrow in subSaleRow)
                        {
                            saleCount = saleCount + subrow.Field<int>("FNOrderCount");
                            saleAmt = saleAmt + subrow.Field<decimal>("amount");
                            supAmt = supAmt + subrow.Field<decimal>("amount");
                            supCount = supCount + subrow.Field<int>("FNOrderCount");

                            totalAmt = totalAmt + subrow.Field<decimal>("amount");
                            totalCount = totalCount + subrow.Field<int>("FNOrderCount");



                            worksheet1.Cells["D" + rowSupIndex].Value = subrow.Field<string>("CSSaleCode");
                            worksheet1.Cells["E" + rowSupIndex].Value = subrow.Field<string>("CNSaleName");
                            worksheet1.Cells["F" + rowSupIndex].Value = subrow.Field<string>("Province");
                            worksheet1.Cells["G" + rowSupIndex].Value = subrow.Field<string>("CSCustomerCode");
                            worksheet1.Cells["H" + rowSupIndex].Value = subrow.Field<string>("CSCustomerName");

                            worksheet1.Cells["I" + rowSupIndex].Value = subrow.Field<int>("FNOrderCount");
                            worksheet1.Cells["J" + rowSupIndex].Value = subrow.Field<decimal>("amount");
                            worksheet1.Cells["J" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            worksheet1.Cells["J" + rowSupIndex].Style.Numberformat.Format = "#,##0.00";

                            worksheet1.Cells["K" + rowSupIndex].Value = subrow.Field<string>("CSEmpCodeSup");
                            worksheet1.Cells["L" + rowSupIndex].Value = subrow.Field<string>("CSEmpNameSup");
                            rowSupIndex++;


                        }
                        worksheet1.Cells["D" + rowSupIndex + ":H" + rowSupIndex + ""].Merge = true;
                        worksheet1.Cells["D" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet1.Cells["D" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet1.Cells["I" + rowSupIndex].Value = saleCount;
                        worksheet1.Cells["J" + rowSupIndex].Value = "รวม " + saleAmt.ToString("N2");
                        worksheet1.Cells["J" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet1.Cells["K" + rowSupIndex + ":L" + rowSupIndex + ""].Merge = true;
                       
                        rowSupIndex++;

                    }

                    worksheet1.Cells["B" + rowSupIndex + ":H" + rowSupIndex + ""].Merge = true;
                    worksheet1.Cells["B" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet1.Cells["B" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet1.Cells["I" + rowSupIndex].Value = supCount;
                    worksheet1.Cells["J" + rowSupIndex].Value = "รวม " + supAmt.ToString("N2");
                    worksheet1.Cells["J" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    worksheet1.Cells["K" + rowSupIndex + ":L" + rowSupIndex + ""].Merge = true;
                    
                    rowSupIndex++;

                }
                worksheet1.Cells["A" + rowSupIndex + ":H" + rowSupIndex + ""].Merge = true;
                worksheet1.Cells["A" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet1.Cells["A" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet1.Cells["I" + rowSupIndex].Value = totalCount;
                worksheet1.Cells["J" + rowSupIndex].Value = "รวม " + totalAmt.ToString("N2");
                worksheet1.Cells["J" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet1.Cells["K" + rowSupIndex + ":L" + rowSupIndex + ""].Merge = true;
               
                rowSupIndex++;



                worksheet1.Cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet1.Cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet1.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet1.Cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;

                Color lightGreyColor = Color.FromArgb(158, 158, 158);
                worksheet1.Cells.Style.Border.Top.Color.SetColor(lightGreyColor);
                worksheet1.Cells.Style.Border.Bottom.Color.SetColor(lightGreyColor);
                worksheet1.Cells.Style.Border.Right.Color.SetColor(lightGreyColor);
                worksheet1.Cells.Style.Border.Left.Color.SetColor(lightGreyColor);
                worksheet1.Cells.AutoFitColumns();




                // Define the path where the file will be saved

                string fileName = "supDaily_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                string filenameis = fileName;
                string filePath = Path.Combine(@"D:\reportexportexcel", fileName);
                if (!Directory.Exists(@"D:\reportexportexcel"))
                {
                    Directory.CreateDirectory(@"D:\reportexportexcel");
                }




                // Save the Excel package to the specified path
                FileInfo file = new FileInfo(filePath);
                package.SaveAs(file);

                // Create the response
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(File.ReadAllBytes(filePath))
                };

                // Set the content type and disposition
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = filenameis
                };

                return response;
            }


        }


        [HttpGet]
        [Route("reportSupTrip")]
        public HttpResponseMessage ExportToExcelSupTrip(String SDate, String EDate, String Username)
        {

            // Sample DataTable
            DataTable dt = new System.Data.DataTable();
            string _cmd;
            _cmd = "exec dbo.[SP_GET_Report_Detail_Sup_Trip_ForMobile]  @StartDate='" + SDate + "', @EndDate ='" + EDate + "' , @userlogin='" + Username + "'";
            // _cmd = "SELECT TOP (200) CNPositionId, CSPositionCode, CSPositionName 'ชื่อ', CSStateActive  , 98 as 'รวม' FROM     MPosition";
            dt = DB.DBConn.GetDataTable(_cmd);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Create Excel package
            using (ExcelPackage package = new ExcelPackage())
            {
                // Add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                worksheet.Cells.Style.Font.Name = "Tahoma"; // Set font name
                worksheet.Cells.Style.Font.Size = 8; // Set font size
                worksheet.Cells.Style.Font.Color.SetColor(Color.Black);



                worksheet.Cells["A1:D1"].Merge = true;
                worksheet.Cells["A1:D1"].Value = "วันที่";

                worksheet.Cells["E1"].Value = "รหัสเชล";
                worksheet.Cells["F1"].Value = "ผู้แทน";
                worksheet.Cells["G1"].Value = "จำนวนร้านค้า";
                worksheet.Cells["H1"].Value = "จำนวนเงินสุทธิ";
                worksheet.Cells["I1"].Value = "รหัส Sup";
                worksheet.Cells["J1"].Value = "ชื่อ Sup";


                worksheet.Cells["A1:J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A1:J1"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet.Cells["A1:J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var dtSupCode = dt.AsEnumerable()
                                       .GroupBy(row => new
                                       {
                                           CSEmpCodeSup = row.Field<string>("CSEmpCodeSup"),
                                           CSEmpNameSup = row.Field<string>("CSEmpNameSup")
                                       })
                                       .OrderBy(group => group.Key.CSEmpCodeSup);

                int rowSupIndex = 2;
                int supCount = 0;
                int saleCount = 0;

                decimal supAmt = 0;
                decimal saleAmt = 0;

                int totalCount = 0;
                decimal totalAmt = 0;

                foreach (var SupRow in dtSupCode)
                {

                    supCount = 0;
                    supAmt = 0;
                    worksheet.Cells["A" + rowSupIndex + ":J" + rowSupIndex + ""].Merge = true;
                    worksheet.Cells["A" + rowSupIndex + ":J" + rowSupIndex + ""].Value = "รหัส Sup :" + SupRow.Key.CSEmpCodeSup + "";
                    worksheet.Cells["A" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells["A" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    rowSupIndex++;

                    worksheet.Cells["B" + rowSupIndex + ":J" + rowSupIndex + ""].Merge = true;
                    worksheet.Cells["B" + rowSupIndex + ":J" + rowSupIndex + ""].Value = "ชื่อ Sup :" + SupRow.Key.CSEmpNameSup + "";
                    worksheet.Cells["B" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells["B" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    rowSupIndex++;

                    var subSaleCode = SupRow.AsEnumerable()
                                      .GroupBy(row => new
                                      {
                                          CSSaleCode = row.Field<string>("CSSaleCode"),
                                      })
                                      .OrderBy(group => group.Key.CSSaleCode);

                    foreach (var subSaleRow in subSaleCode)
                    {
                        worksheet.Cells["C" + rowSupIndex + ":J" + rowSupIndex + ""].Merge = true;
                        worksheet.Cells["C" + rowSupIndex + ":J" + rowSupIndex + ""].Value = "รหัสเชล :" + subSaleRow.Key.CSSaleCode + "";
                        worksheet.Cells["C" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["C" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        rowSupIndex++;
                        saleCount = 0;
                        saleAmt = 0;
                        foreach (var subrow in subSaleRow)
                        {
                            saleCount = saleCount + subrow.Field<int>("FNOrderCount");
                            saleAmt = saleAmt + subrow.Field<decimal>("amount");
                            supAmt = supAmt + subrow.Field<decimal>("amount");
                            supCount = supCount + subrow.Field<int>("FNOrderCount");

                            totalAmt = totalAmt + subrow.Field<decimal>("amount");
                            totalCount = totalCount + subrow.Field<int>("FNOrderCount");


                            worksheet.Cells["D" + rowSupIndex].Value = subrow.Field<string>("CDOrderDate");
                            worksheet.Cells["E" + rowSupIndex].Value = subrow.Field<string>("CSSaleCode");
                            worksheet.Cells["F" + rowSupIndex].Value = subrow.Field<string>("CNSaleName");
                            worksheet.Cells["G" + rowSupIndex].Value = subrow.Field<int>("FNOrderCount");
                            worksheet.Cells["H" + rowSupIndex].Value = subrow.Field<decimal>("amount");
                            worksheet.Cells["H" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            worksheet.Cells["H" + rowSupIndex].Style.Numberformat.Format = "#,##0.00";

                            worksheet.Cells["I" + rowSupIndex].Value = subrow.Field<string>("CSEmpCodeSup");
                            worksheet.Cells["J" + rowSupIndex].Value = subrow.Field<string>("CSEmpNameSup");
                            rowSupIndex++;


                        }
                        worksheet.Cells["D" + rowSupIndex + ":F" + rowSupIndex + ""].Merge = true;
                        worksheet.Cells["D" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells["D" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet.Cells["G" + rowSupIndex].Value = saleCount;
                        worksheet.Cells["H" + rowSupIndex].Value = "รวม " + saleAmt.ToString("N2");
                        worksheet.Cells["H" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        rowSupIndex++;

                    }

                    worksheet.Cells["B" + rowSupIndex + ":F" + rowSupIndex + ""].Merge = true;
                    worksheet.Cells["B" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells["B" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet.Cells["G" + rowSupIndex].Value = supCount;
                    worksheet.Cells["H" + rowSupIndex].Value = "รวม " + supAmt.ToString("N2");
                    worksheet.Cells["H" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    rowSupIndex++;

                }
                worksheet.Cells["A" + rowSupIndex + ":F" + rowSupIndex + ""].Merge = true;
                worksheet.Cells["A" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A" + rowSupIndex + ":J" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet.Cells["G" + rowSupIndex].Value = totalCount;
                worksheet.Cells["H" + rowSupIndex].Value = "รวม " + totalAmt.ToString("N2");
                worksheet.Cells["H" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                rowSupIndex++;



                worksheet.Cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;

                Color lightGreyColor1 = Color.FromArgb(158, 158, 158);
                worksheet.Cells.Style.Border.Top.Color.SetColor(lightGreyColor1);
                worksheet.Cells.Style.Border.Bottom.Color.SetColor(lightGreyColor1);
                worksheet.Cells.Style.Border.Right.Color.SetColor(lightGreyColor1);
                worksheet.Cells.Style.Border.Left.Color.SetColor(lightGreyColor1);
                worksheet.Cells.AutoFitColumns();



                ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("Sheet2");

                // Sample data
                DataTable dt2 = new DataTable();
                _cmd = " exec dbo.[SP_GET_Report_Sum_Sup_Trip_ForMobile]   @StartDate='" + SDate + "', @EndDate ='" + EDate + "' , @userlogin='" + Username + "'";
                dt2 = DB.DBConn.GetDataTable(_cmd);

                worksheet1.Cells.Style.Font.Name = "Tahoma"; // Set font name
                worksheet1.Cells.Style.Font.Size = 8; // Set font size
                worksheet1.Cells.Style.Font.Color.SetColor(Color.Black);

                DateTime _sdate = DateTime.ParseExact(SDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                string sformattedDate = _sdate.ToString("dd/MM/yyyy");

                DateTime _edate = DateTime.ParseExact(EDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                string eformattedDate = _edate.ToString("dd/MM/yyyy");



                worksheet1.Cells["A1:D1"].Merge = true;
                worksheet1.Cells["A1:D1"].Value = "รหัสเชล";


                worksheet1.Cells["E1"].Value = "ผู้แทน";
                worksheet1.Cells["F1"].Value = "จังหวัด";
                worksheet1.Cells["G1"].Value = "รหัสลูกค้า";
                worksheet1.Cells["H1"].Value = "ชื่อลูกค้า";
                worksheet1.Cells["I1"].Value = "จำนวน Order";
                worksheet1.Cells["J1"].Value = "จำนวนเงินสุทธิ";
                worksheet1.Cells["K1"].Value = "รหัส Sup";
                worksheet1.Cells["L1"].Value = "ชื่อ Sup";


                worksheet1.Cells["A1:L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet1.Cells["A1:L1"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet1.Cells["A1:L1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                var dtSupCode2 = dt2.AsEnumerable()
                                       .GroupBy(row => new
                                       {
                                           CSEmpCodeSup = row.Field<string>("CSEmpCodeSup"),
                                           CSEmpNameSup = row.Field<string>("CSEmpNameSup")
                                       })
                                       .OrderBy(group => group.Key.CSEmpCodeSup);

                rowSupIndex = 2;
                supCount = 0;
                saleCount = 0;

                supAmt = 0;
                saleAmt = 0;

                totalCount = 0;
                totalAmt = 0;

                foreach (var SupRow in dtSupCode2)
                {

                    supCount = 0;
                    supAmt = 0;
                    worksheet1.Cells["A" + rowSupIndex + ":L" + rowSupIndex + ""].Merge = true;
                    worksheet1.Cells["A" + rowSupIndex + ":L" + rowSupIndex + ""].Value = "รหัส Sup :" + SupRow.Key.CSEmpCodeSup + "";
                    worksheet1.Cells["A" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet1.Cells["A" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    rowSupIndex++;

                    worksheet1.Cells["B" + rowSupIndex + ":L" + rowSupIndex + ""].Merge = true;
                    worksheet1.Cells["B" + rowSupIndex + ":L" + rowSupIndex + ""].Value = "ชื่อ Sup :" + SupRow.Key.CSEmpNameSup + "";
                    worksheet1.Cells["B" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet1.Cells["B" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    rowSupIndex++;

                    var subSaleCode = SupRow.AsEnumerable()
                                      .GroupBy(row => new
                                      {
                                          CSSaleCode = row.Field<string>("CSSaleCode"),
                                      })
                                      .OrderBy(group => group.Key.CSSaleCode);

                    foreach (var subSaleRow in subSaleCode)
                    {
                        worksheet1.Cells["C" + rowSupIndex + ":L" + rowSupIndex + ""].Merge = true;
                        worksheet1.Cells["C" + rowSupIndex + ":L" + rowSupIndex + ""].Value = "รหัสเชล :" + subSaleRow.Key.CSSaleCode + "";
                        worksheet1.Cells["C" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet1.Cells["C" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        rowSupIndex++;
                        saleCount = 0;
                        saleAmt = 0;
                        foreach (var subrow in subSaleRow)
                        {
                            saleCount = saleCount + subrow.Field<int>("FNOrderCount");
                            saleAmt = saleAmt + subrow.Field<decimal>("amount");
                            supAmt = supAmt + subrow.Field<decimal>("amount");
                            supCount = supCount + subrow.Field<int>("FNOrderCount");

                            totalAmt = totalAmt + subrow.Field<decimal>("amount");
                            totalCount = totalCount + subrow.Field<int>("FNOrderCount");



                            worksheet1.Cells["D" + rowSupIndex].Value = subrow.Field<string>("CSSaleCode");
                            worksheet1.Cells["E" + rowSupIndex].Value = subrow.Field<string>("CNSaleName");
                            worksheet1.Cells["F" + rowSupIndex].Value = subrow.Field<string>("Province");
                            worksheet1.Cells["G" + rowSupIndex].Value = subrow.Field<string>("CSCustomerCode");
                            worksheet1.Cells["H" + rowSupIndex].Value = subrow.Field<string>("CSCustomerName");

                            worksheet1.Cells["I" + rowSupIndex].Value = subrow.Field<int>("FNOrderCount");
                            worksheet1.Cells["J" + rowSupIndex].Value = subrow.Field<decimal>("amount");
                            worksheet1.Cells["J" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            worksheet1.Cells["J" + rowSupIndex].Style.Numberformat.Format = "#,##0.00";

                            worksheet1.Cells["K" + rowSupIndex].Value = subrow.Field<string>("CSEmpCodeSup");
                            worksheet1.Cells["L" + rowSupIndex].Value = subrow.Field<string>("CSEmpNameSup");
                            rowSupIndex++;


                        }
                        worksheet1.Cells["D" + rowSupIndex + ":H" + rowSupIndex + ""].Merge = true;
                        worksheet1.Cells["D" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet1.Cells["D" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        worksheet1.Cells["I" + rowSupIndex].Value = saleCount;
                        worksheet1.Cells["J" + rowSupIndex].Value = "รวม " + saleAmt.ToString("N2");
                        worksheet1.Cells["J" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        worksheet1.Cells["K" + rowSupIndex + ":L" + rowSupIndex + ""].Merge = true;

                        rowSupIndex++;

                    }

                    worksheet1.Cells["B" + rowSupIndex + ":H" + rowSupIndex + ""].Merge = true;
                    worksheet1.Cells["B" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet1.Cells["B" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    worksheet1.Cells["I" + rowSupIndex].Value = supCount;
                    worksheet1.Cells["J" + rowSupIndex].Value = "รวม " + supAmt.ToString("N2");
                    worksheet1.Cells["J" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    worksheet1.Cells["K" + rowSupIndex + ":L" + rowSupIndex + ""].Merge = true;

                    rowSupIndex++;

                }
                worksheet1.Cells["A" + rowSupIndex + ":H" + rowSupIndex + ""].Merge = true;
                worksheet1.Cells["A" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet1.Cells["A" + rowSupIndex + ":L" + rowSupIndex + ""].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                worksheet1.Cells["I" + rowSupIndex].Value = totalCount;
                worksheet1.Cells["J" + rowSupIndex].Value = "รวม " + totalAmt.ToString("N2");
                worksheet1.Cells["J" + rowSupIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet1.Cells["K" + rowSupIndex + ":L" + rowSupIndex + ""].Merge = true;

                rowSupIndex++;



                worksheet1.Cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet1.Cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet1.Cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet1.Cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;

                Color lightGreyColor = Color.FromArgb(158, 158, 158);
                worksheet1.Cells.Style.Border.Top.Color.SetColor(lightGreyColor);
                worksheet1.Cells.Style.Border.Bottom.Color.SetColor(lightGreyColor);
                worksheet1.Cells.Style.Border.Right.Color.SetColor(lightGreyColor);
                worksheet1.Cells.Style.Border.Left.Color.SetColor(lightGreyColor);
                worksheet1.Cells.AutoFitColumns();




                // Define the path where the file will be saved

                string fileName = "supDaily_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                string filenameis = fileName;
                string filePath = Path.Combine(@"D:\reportexportexcel", fileName);
                if (!Directory.Exists(@"D:\reportexportexcel"))
                {
                    Directory.CreateDirectory(@"D:\reportexportexcel");
                }




                // Save the Excel package to the specified path
                FileInfo file = new FileInfo(filePath);
                package.SaveAs(file);

                // Create the response
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(File.ReadAllBytes(filePath))
                };

                // Set the content type and disposition
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = filenameis
                };

                return response;
            }


        }

    }

    }

