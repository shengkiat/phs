using PHS.Business.Extensions;
using PHS.Business.Implementation;
using PHS.Common;
using PHS.DB;
using PHS.Web.Filter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PHS.Web.Controllers
{
    [CustomAuthorize(Roles = Constants.User_Role_CommitteeMember_Code + Constants.User_Role_FollowUpVolunteer_Code + Constants.User_Role_FollowUpCommitteeMember_Code)]
    public class FollowUpController : BaseController
    {
        // GET: FollowUp
        public ActionResult Index()
        {
            using (var eventmanger = new EventManager())
            {
                var result = eventmanger.GetAllEvents().ToList();
                return View(result);
            }
        }
        public ActionResult GetParticipantsByLoginUser(int? eventid)
        {
            string message = string.Empty;
            using (var followUpManager = new FollowUpManager())
            {
                var result = followUpManager.GetParticipantsByLoginUser(eventid.Value, GetLoginUser(), out message);
                return PartialView("_ParticipantCallerTable", result);
            }
        }

        [HttpPost]
        public ActionResult SaveParticipantCallerMapping([Bind(Exclude = "ParticipantID,FollowUpGroupID,PhaseIFollowUpVolunteer,PhaseIIFollowUpVolunteer,PhaseICommitteeMember,PhaseIICommitteeMember")]ParticipantCallerMapping participantCallerMapping)
        {
            string message = string.Empty;

            using (var followUpManager = new FollowUpManager())
            {
                bool isSaved = followUpManager.SaveParticipantCallerMapping(participantCallerMapping, out message);

                if (!isSaved)
                {
                    return Json(new { success = false, error = message, isautosave = false });
                }

                else
                {
                    return Json(new { success = true, message = "Your changes were saved.", isautosave = false });
                }
            }
        }

        public ActionResult ExportParticipantCallerMapping(int followgroupid)
        {
            string message = string.Empty;
            using (var followUpManager = new FollowUpManager(GetLoginUser()))
            {
                var participantCallerMappingExportViewModel = followUpManager.CreateParticipantCallerMappingDataTable(followgroupid, out message);

                var gridView = new GridView();
                gridView.DataSource = participantCallerMappingExportViewModel.ValuesDataTable;
                gridView.DataBind();

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                gridView.RenderControl(htw);

                byte[] fileBytes = Encoding.ASCII.GetBytes(sw.ToString());

                String guid = Guid.NewGuid().ToString();

                TempData[guid] = fileBytes;

                return new JsonResult()
                {
                    Data = new { FileGuid = guid, FileName = "{0}.xls".FormatWith(participantCallerMappingExportViewModel.Title) }
                };
            }
        }

        [HttpGet]
        public ActionResult DownloadExport(string fileGuid, string fileName)
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