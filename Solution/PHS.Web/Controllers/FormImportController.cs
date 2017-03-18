using OfficeOpenXml;
using OfficeOpenXml.Style;
using PHS.Business.Implementation;
using PHS.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
            List<form> forms = new List<form>();

            using (var manager = new FormManager())
            {
                forms = manager.FindAllForms();

            }

            return View(forms);
        }

        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (HttpPostedFileBase file in files)
            {
                string filePath = Server.MapPath("~/App_Data/" + file.FileName);

                System.IO.File.WriteAllBytes(filePath, ReadData(file.InputStream));
            }

            return Json("All files have been successfully stored.");
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

            form form = new form();
            using (var manager = new FormManager())
            {
                form = manager.FindForm(formid);
            }


            // Create the package and make sure you wrap it in a using statement
            using (var package = new ExcelPackage(file))
            {
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(form.Title);

                // --------- Data and styling goes here -------------- //
                // Add some formatting to the worksheet
               // worksheet.TabColor = Color.Blue;
                worksheet.DefaultRowHeight = 12;

                // Start adding the header
                // First of all the first row
                int x = 1;
                foreach (var field in form.form_fields)
                {
                    worksheet.Cells[1, x].Value = field.Label;
                    worksheet.Column(x).AutoFit();
                    x++;

                }

                // save our new workbook and we are done!
                package.Save();
            }

            byte[] fileBytes = System.IO.File.ReadAllBytes(outputDir + fileName);
            TempData[guid] = fileBytes;

            System.IO.File.Delete(Server.MapPath("~/App_Data/" + guid + ".xlsx"));

            return new JsonResult()
            {
                Data = new { FileGuid = guid, FileName = form.Title + ".xlsx" }
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

    }
}