using PHS.Business.Implementation;
using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHS.Web.Controllers
{
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

        public ActionResult Edit(int id)
        {
            PHSEvent eventModel;

            using (var eventManager = new EventManager())
            {
                eventModel = eventManager.GetEventByID(id);
            }

            return View(eventModel);
        }

        [HttpPost]
        public ActionResult Edit(PHSEvent eventModel)
        {
            using (var eventManager = new EventManager())
            {
                eventManager.UpdateEvent(eventModel);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            List<Template> Forms;
            using (var formManager = new FormManager())
            {
                Forms = formManager.FindAllForms();

                ViewData["ss"] = Forms;

                String htmlString = "<select id=\"SelectedForm\" name=\"Modalities[0].FormID\">";
               // String htmlString = "";
                foreach (var form in Forms)
                {
                    htmlString += "<option value=\"" + form.ID + "\">" + form.Title + "</option>";
                }

                htmlString += "</select>";

                ViewData["Forms"] = htmlString;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "ID")]PHSEvent eventModel)
        {
            if (ModelState.IsValid)
            {
                using (var eventManager = new EventManager())
                {
                    eventModel.CreatedBy = "";

                    eventManager.NewEvent(eventModel);
                }


            }
            else {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
               
            }

            return RedirectToAction("Index");
        }








    }
}