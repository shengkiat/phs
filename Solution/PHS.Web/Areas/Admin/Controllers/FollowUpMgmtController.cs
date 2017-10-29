using Ionic.Zip;
using Novacode;
using OfficeOpenXml;
using PHS.Business.Implementation;
using PHS.Business.ViewModel.FollowUp;
using PHS.Common;
using PHS.DB;
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

        public ActionResult GetParticipantsByFollowUpConfiguration(int followupconfigurationid)
        {
            string message = string.Empty;
            using (var followUpManager = new FollowUpManager())
            {
                var result = followUpManager.GetParticipantsByFollowUpConfiguration(followupconfigurationid, out message);
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
        public ActionResult DeleteFollowUpConfiguration(int followupconfigurationid)
        {
            string message = string.Empty;
            using (var followUpManager = new FollowUpConfigurationManager())
            {
                var success = followUpManager.DeleteFollowUpConfiguration(followupconfigurationid, out message);
                if (!success)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Error = message });
                }
                //return Json(new { Success = "Deployment Successful." });
                return View();
            }
        }

        [HttpPost]
        public ActionResult PrintHealthReportByFollowUpGroup(int followgroupid, string healthReportType)
        {
            string message = string.Empty;

            if (string.IsNullOrEmpty(healthReportType))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Error = "None" });
            }

            string englishTemplatePath = null;
            if (Constants.Followup_Print_HealthReport_Normal.Equals(healthReportType))
            {
                englishTemplatePath = Server.MapPath("~/App_Data/Normal_English.docx");
            }

            else if (Constants.Followup_Print_HealthReport_Abnormal.Equals(healthReportType))
            {
                englishTemplatePath = Server.MapPath("~/App_Data/Abnormal_English.docx");
            }

            else if (Constants.Followup_Print_HealthReport_AbnormalNonEligible.Equals(healthReportType))
            {
                englishTemplatePath = Server.MapPath("~/App_Data/Abnormal Non-eligible_English.docx");
            }

            if (string.IsNullOrEmpty(healthReportType))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Error = "Invalid health report type" });
            }

            using (var followUpManager = new FollowUpManager())
            {

                var followupParticipantList = followUpManager.PrintHealthReportByFollowUpGroup(followgroupid, out message);
                if (!message.Equals("success"))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Error = message });
                }

                //followupParticipantList = Testing();

                string directoryName = Path.GetRandomFileName();
                string directoryFolder = Path.Combine(Path.GetTempPath(), directoryName);
                Directory.CreateDirectory(directoryFolder);

                String guid = Guid.NewGuid().ToString();

                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                   
                    foreach(var followupParticipant in followupParticipantList)
                    {
                        zip.AddDirectoryByName(followupParticipant.Participant.Nric);

                        byte[] fileBytes = generateHealthReport(englishTemplatePath, followupParticipant);

                        string englishResultPath = directoryFolder + "\\" +  followupParticipant.Participant.Nric + "English.docx";

                        System.IO.File.WriteAllBytes(englishResultPath, fileBytes); // Requires System.IO

                        zip.AddFile(englishResultPath, followupParticipant.Participant.Nric);

                        if (!string.IsNullOrEmpty(followupParticipant.Participant.Language))
                        {
                            if (followupParticipant.Participant.Language.Contains("Mandarin"))
                            {

                            }

                            else if (followupParticipant.Participant.Language.Contains("Tamil"))
                            {

                            }

                            else if (followupParticipant.Participant.Language.Contains("Malay"))
                            {

                            }
                        }

                    }

                    string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        zip.Save(memoryStream);
                        memoryStream.Flush(); //Always catches me out
                        memoryStream.Position = 0; //Not sure if this is required

                        TempData[guid] = memoryStream.ToArray();

                        Directory.Delete(directoryFolder, true);

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

            doc.ReplaceText("<<Name>>", followupParticipant.Participant.FullName);
            doc.ReplaceText("<<Address>>", followupParticipant.Participant.Address);
            doc.ReplaceText("<<NRIC>>", followupParticipant.Participant.Nric);
            doc.ReplaceText("<<Height>>", followupParticipant.Height);
            doc.ReplaceText("<<Weight>>", followupParticipant.Weight);
            doc.ReplaceText("<<BMI>>", followupParticipant.BMIValue);
            doc.ReplaceText("<<BMIRange>>", followupParticipant.BMIStandardReferenceResult);
            doc.ReplaceText("<<BloodTestResult>>", followupParticipant.BloodTestResult);
            doc.ReplaceText("<<Average Reading>>", followupParticipant.BPValue);
            doc.ReplaceText("<<OverallResult>>", followupParticipant.OverAllResult);

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

        private List<FollowUpMgmtViewModel> Testing()
        {
            var printmodellist = new List<FollowUpMgmtViewModel>();
            Random random = new Random();
            string[] names = { "Mandarin", "Tamil", "Malay" };

            for (int i = 0; i< 1000; i++)
            {
                int randomNumber = random.Next(0, 9000000);

                var printmodel = new FollowUpMgmtViewModel();

                Participant participant = new Participant();
                participant.Nric = randomNumber.ToString();
                participant.FullName = "My name is tester-" + randomNumber;
                participant.Address = "Testing Address";

                int index = random.Next(names.Count());
                participant.Language = names[index];

                printmodel.Participant = participant;

                printmodel.Weight = "" + random.Next(0, 60);
                printmodel.Height = "" + random.Next(150, 180);
                printmodel.BMIValue = "" + random.Next(20, 25);
                printmodel.BMIStandardReferenceResult = "normal";
                printmodel.BPValue = "120/80";
                printmodel.BPStandardReferenceResult = "normal";
                printmodel.BloodTestResult = "Satisfactory";
                printmodel.OverAllResult = "satisfactory";

                printmodellist.Add(printmodel);
            }
            

            return printmodellist;
        }

        [HttpPost]
        public ActionResult ImportCaller(int followgroupid, HttpPostedFileBase file)
        {
            string message = string.Empty;
            //Get Callers from excel
            using (var followUpManager = new FollowUpManager())
            {
                List<string> volunteers = new List<string>();
                List<string> commmembers = new List<string>();

                var followupParticipantList = followUpManager.ImportCaller(followgroupid, volunteers, commmembers, out message);
                if (!message.Equals("success"))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Error = message });
                }
            }
            return Json(new { Success = "Import Caller Successful." });
        }

    }
}