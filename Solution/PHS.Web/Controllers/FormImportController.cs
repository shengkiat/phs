using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using PHS.Business.Implementation;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using PHS.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace PHS.Web.Controllers
{
    public class FormImportController : BaseController
    {
        // GET: FormImport
        public ActionResult Index()
        {
            List<Template> templates = new List<Template>();

            using (var manager = new FormManager())
            {
                templates = manager.FindAllTemplates();

            }

            return View(templates);
        }

        public string Between( string value, string a, string b)
        {
            int posA = value.IndexOf(a);
            int posB = value.IndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return value.Substring(adjustedPosA, posB - adjustedPosA);
        }

        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            Request.InputStream.Position = 0;
            var input = new StreamReader(Request.InputStream).ReadToEnd();

            var idSearch = Between(input.Substring(10, input.Length - 10), "formid\"", "---");
            Regex.Replace(idSearch, @"\s+", "");
            idSearch = idSearch.Replace(System.Environment.NewLine, string.Empty);

            int formid = 0;
            if (!Int32.TryParse(idSearch, out formid))
            {
                return new HttpStatusCodeResult(400, "Invalid Form");
            }

            foreach (HttpPostedFileBase file in files)
            {
                string filePath = Server.MapPath("~/App_Data/" + Guid.NewGuid().ToString());

               // System.IO.File.WriteAllBytes(filePath, ReadData(file.InputStream));

                byte[] data =  ReadData(file.InputStream);

                using (var manager = new FormManager())
                {
                    string msg  = manager.InsertUploadDataToTemplate(data, formid);

                    System.IO.File.Delete(filePath);

                    if (msg != "")
                    {
                        return new HttpStatusCodeResult(400, msg); // Bad Request

                    }
                }

            }

            return new HttpStatusCodeResult(200, "Results have been successfully uploaded."); // Bad Request
        }

        private byte[] ReadData(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        [HttpPost]
        public ActionResult GenerateFormTemplate(int formid)
        {
            // Set the file name and get the output directory
            String guid = Guid.NewGuid().ToString();
            var fileName = guid + ".xlsx";
            var outputDir = Server.MapPath("~/App_Data/");

            // Create the file using the FileInfo object
            var file = new FileInfo(outputDir + fileName);

            Template template = new Template();
            using (var manager = new FormManager())
            {
                template = manager.FindTemplate(formid);
            }

            var templateView = TemplateViewModel.CreateFromObject(template);

            MemoryStream stream;

            // Create the package and make sure you wrap it in a using statement
            using (var package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(templateView.Title);

                // --------- Data and styling goes here -------------- //
                // Add some formatting to the worksheet
                // worksheet.TabColor = Color.Blue;
                worksheet.DefaultRowHeight = 12;

                // Start adding the header
                // First of all the first row
                int x = 1;
                foreach (var field in template.TemplateFields)
                {
                    if (field.FieldType == "ADDRESS")
                    {
                        worksheet.Cells[1, x].Value = "Blk/Hse No";
                        worksheet.Column(x).AutoFit();
                        x++;

                        worksheet.Cells[1, x].Value = "Unit";
                        worksheet.Column(x).AutoFit();
                        x++;

                        worksheet.Cells[1, x].Value = "Street Address";
                        worksheet.Column(x).AutoFit();
                        x++;

                        worksheet.Cells[1, x].Value = "Postal Code";
                        worksheet.Column(x).AutoFit();
                        x++;

                    }
                    else
                    {
                        worksheet.Cells[1, x].Value = field.Label;
                        worksheet.Column(x).AutoFit();
                        x++;
                    }

  

                }

                // save our new workbook and we are done!
                //package.SaveAs();
                 stream = new MemoryStream(package.GetAsByteArray());
            }

            byte[] fileBytes = stream.ToArray();

          //  byte[] fileBytes = System.IO.File.ReadAllBytes(outputDir + fileName);
            TempData[guid] = fileBytes;

            System.IO.File.Delete(Server.MapPath("~/App_Data/" + guid + ".xlsx"));

            return new JsonResult()
            {
                Data = new { FileGuid = guid, FileName = templateView.Title + ".xlsx" }
            };

        }

        [HttpGet]
        public ActionResult DownloadFormTemplate(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                return File(data, "application/vnd.ms-excel", fileName);
            }
            else
            {
                // Problem - Log the error, generate a blank file,
                //           redirect to another controller action - whatever fits with your application
                return new EmptyResult();
            }
        }


        [HttpPost]
        public ActionResult UploadSelectedFile()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;

                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
    }
}