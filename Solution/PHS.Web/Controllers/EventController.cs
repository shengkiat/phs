using PHS.Business.Implementation;
using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using PHS.Web.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHS.Web.Controllers
{
    [CustomAuthorize(Roles = Constants.User_Role_CommitteeMember_Code)]
    public class EventController : BaseController
    {

        public ActionResult Index()
        {
            IEnumerable<PHS.DB.PHSEvent> events = new List<PHS.DB.PHSEvent>();
            using (var eventManager = new EventManager())
            {
                events = eventManager.GetAllEvents();
            }

            return View(events);
        }

        public ActionResult Create()
        {
            //List<Template> templates;
            //using (var formManager = new FormManager())
            //{
            //    templates = formManager.FindAllTemplates();

            //    ViewData["ss"] = templates;

            //    String htmlString = "<select id=\"SelectedForm\" name=\"Modalities[0].FormID\">";
            //    // String htmlString = "";
            //    foreach (var template in templates)
            //    {
            //        var templateView = TemplateViewModel.CreateFromObject(template);
            //        htmlString += "<option value=\"" + template.TemplateID + "\">" + templateView.Title + "</option>";
            //    }

            //    htmlString += "</select>";

            //    ViewData["Forms"] = htmlString;
            //}

            //Create default Modalities
            PHSEvent phsEvent = buildDefaultEvent();

            return View(phsEvent);
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "ID")]PHSEvent eventModel)
        {
            string message = string.Empty;

            using (var eventManager = new EventManager())
            {
                //Person loginUser = GetLoginUser();
                //if(loginUser != null)
                //{
                //    eventModel.CreatedBy = loginUser.PersonID;
                //}
                //else
                //{
                    eventModel.CreatedBy = "";
                //}
                eventModel.Modalities = BuildDefaultModalites();

                bool isCreated = eventManager.NewEvent(eventModel, out message);

                if(isCreated == false)
                {
                    SetViewBagError(message);
                    return View(eventModel);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var getEvent = new EventManager())
            {
                PHSEvent eventModel = getEvent.GetEventByID(id, out message);
                if (eventModel == null)
                {
                    SetViewBagError(message);
                }

                SetBackURL("Index");
                return View(eventModel);
            };
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "PHSEventID,Title,Venue,StartDT,EndDT")] PHSEvent eventModel)
        {
            using (var eventManager = new EventManager())
            {
                eventManager.UpdateEvent(eventModel);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var getEvent = new EventManager())
            {
                bool isDeleted = getEvent.DeleteEvent(id, out message);

                if (isDeleted == false)
                {
                    SetTempDataError(message);
                }else
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Event has been deleted!"));
                }

                return RedirectToAction("Index");
            };
        }

        private List<Modality> BuildDefaultModalites()
        {
            List<Modality> modalities = new List<Modality>();
            Modality mRegister = new Modality();
            mRegister.Name = "Registration";
            mRegister.Position = 0;
            mRegister.IconPath = "../../../Content/images/Modality/01registration.png";
            mRegister.IsActive = false;
            mRegister.IsVisible = true;
            mRegister.IsMandatory = true;
            mRegister.HasParent = false;
            mRegister.Status = "Pending";
            modalities.Add(mRegister);

            Modality mHistoryTaking = new Modality();
            mHistoryTaking.Name = "History Taking";
            mHistoryTaking.Position = 1;
            mHistoryTaking.IconPath = "../../../Content/images/Modality/02historytaking.png";
            mHistoryTaking.IsActive = false;
            mHistoryTaking.IsVisible = true;
            mHistoryTaking.IsMandatory = true;
            mHistoryTaking.HasParent = true;
            mHistoryTaking.Status = "Pending";
            modalities.Add(mHistoryTaking);


            Modality publicForm = new Modality();
            publicForm.Name = "Public Forms";
            publicForm.Position = 99;
            publicForm.IconPath = "../../../Content/images/Modality/02historytaking.png";
            publicForm.IsActive = true;
            publicForm.IsVisible = false;
            publicForm.IsMandatory = true;
            publicForm.HasParent = false;
            publicForm.Status = "Public";
            modalities.Add(publicForm);

            return modalities; 
        }

        private PHSEvent buildDefaultEvent()
        {
            List<Modality> modalities = BuildDefaultModalites();
            PHSEvent phsEvent = new PHSEvent();
            phsEvent.Modalities = modalities;
            return phsEvent;
        }

    }
}