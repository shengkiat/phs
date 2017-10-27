using Ionic.Zip;
using Novacode;
using PHS.Business.Implementation;
using PHS.Business.ViewModel.FollowUp;
using PHS.Common;
using PHS.Web.Controllers;
using PHS.Web.Filter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PHS.Web.Areas.Admin.Controllers
{
    public class FollowUpMgmtController : BaseController
    {
        // GET: Admin/FollowUpMgmt
        public ActionResult Index()
        {
            using (var eventmanger = new EventManager())
            {
                var result = eventmanger.GetAllEvents().ToList();
                return View(result);
            }
        }
        public ActionResult GetFollowUpConfigurationByEventID(int phsEventId)
        {
            using (var followUpManager = new FollowUpConfigurationManager())
            {
                var result = followUpManager.GetAllFUConfigurationByEventID(phsEventId);
                //var result = followUpManager.GetAllFUConfiguration();
                return PartialView("_FollowUpConfigurations", result);
            }
        }

        public ActionResult GetParticipantsByFollowUpConfiguration(int? followupconfigurationid)
        {
            using (var followUpManager = new FollowUpManager())
            {
                var result = followUpManager.GetParticipantsByFollowUpConfiguration(followupconfigurationid.Value);
                return PartialView("_ParticipantsTable", result);
            }
        }

        [HttpPost]
        public ActionResult DeployFollowUpConfiguration(int followupconfigurationid)
        {
            string message = string.Empty;
            using (var followUpManager = new FollowUpManager())
            {
                var success = followUpManager.DeployFollowUpConfiguration(followupconfigurationid, out message);
                if (!success)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Error = message });
                }
                return Json(new { Success = "Deployment Successful." });
            }
        }

        [HttpPost]
        public ActionResult PrintHealthReportByFollowUpGroup(int followupconfigurationid)
        {
            string message = string.Empty;
            using (var followUpManager = new FollowUpManager())
            {

                var followupParticipantList = followUpManager.PrintHealthReportByFollowUpGroup(followupconfigurationid, out message);
                if (!message.Equals("success"))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Error = message });
                }

                

                String guid = Guid.NewGuid().ToString();

                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                   
                    foreach(var followupParticipant in followupParticipantList)
                    {
                        zip.AddDirectoryByName(followupParticipant.Participant.Nric);

                        string templatePath = Server.MapPath("~/App_Data/Normal_English.docx");

                        // Load template into memory
                        byte[] fileBytes = generateHealthReport(templatePath, followupParticipant);

                        string path = followupParticipant.Participant + "_English.docx";

                        System.IO.File.WriteAllBytes(path, fileBytes); // Requires System.IO

                        zip.AddFile(path, followupParticipant.Participant.Nric);

                        //zip.AddFile(path);
                    }

                    string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        zip.Save(memoryStream);
                        memoryStream.Flush(); //Always catches me out
                        memoryStream.Position = 0; //Not sure if this is required

                        TempData[guid] = memoryStream.ToArray();

                        return new JsonResult()
                        {
                            Data = new { FileGuid = guid, FileName = zipName }
                        };
                    }
                }
            }
        }

        private byte[] generateHealthReport(string templatePath, FollowUpMgmtViewModel followupParticipant)
        {
            var doc = DocX.Load(templatePath);

            doc.ReplaceText("<<Name>>", followupParticipant.Participant.Nric);
            doc.ReplaceText("<<Address>>", followupParticipant.Participant.Address);
            doc.ReplaceText("<<NRIC>>", followupParticipant.Participant.Nric);
            //doc.ReplaceText("<<Height>>", followupParticipant.);
            //doc.ReplaceText("<<Weight>>", result.Event.Title + " " + result.Event.Venue);
            //doc.ReplaceText("<<BMI>>", result.Event.Title + " " + result.Event.Venue);
            //doc.ReplaceText("<<Average Reading>>", result.Event.Title + " " + result.Event.Venue);

            var ms = new MemoryStream();
            doc.SaveAs(ms);
            ms.Position = 0;
            return ms.ToArray();
        }

        [HttpGet]
        public ActionResult DownloadZip(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                return File(data, "application/zip", fileName);
            }
            else
            {
                // Problem - Log the error, generate a blank file,
                //           redirect to another controller action - whatever fits with your application
                return new EmptyResult();
            }
        }

        public ActionResult GenerateZipFileTest()
        {
            return View();
        }

    }
}