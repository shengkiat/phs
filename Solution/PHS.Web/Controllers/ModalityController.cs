using PHS.DB;
using PHS.Business.ViewModel.Event;
using PHS.Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PHS.Business.Implementation;
using PHS.Common;

namespace PHS.Web.Controllers
{
    public class ModalityController : BaseController
    {
        // GET: Modality
        public ActionResult Index()
        {
            return View();
        }

        // GET: Modality/Details/5
        public ActionResult Details(int modalityid)
        {
            return View();
        }

        // GET: Modality/Create
        public ActionResult Create(int eventid)
        {
            ModalityEventViewModel modality = initModalityEventView(eventid);

            return View(modality);
        }

        // POST: Modality/Create
        [HttpPost]
        public ActionResult Create(ModalityEventViewModel modalityEventView)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;

            try
            {

                using (var modalityManager = new ModalityManager())
                {
                    modalityManager.NewModality(modalityEventView, out message);
                }

                return RedirectToAction("Edit", "Event", new { id = modalityEventView.EventID });
            }
            catch (Exception ex)
            {
                ExceptionLog(ex);
                message = Constants.OperationFailedDuringAddingValue("Modality");

                SetViewBagError(message);
                return View(modalityEventView);
            }
        }

        // POST: Modality/CreateForms
        [HttpPost]
        public ActionResult CreateForms(FormCollection collection)
        {
            try
            {
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Modality/Edit/5
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Edit(int modalityid)
        {
            return View();
        }

        // POST: Modality/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Modality/Delete/5
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Delete(int ModalityID)
        {
            return View();
        }

        // POST: Modality/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private ModalityEventViewModel initModalityEventView(int eventid)
        {
            ModalityEventViewModel modality = new ModalityEventViewModel();
            modality.Name = "";
            modality.Position = 0;//Assign the actual position when user Save Modality.
            modality.IconPath = "";
            modality.IsActive = false;
            modality.IsVisible = true;
            modality.IsMandatory = false;
            modality.HasParent = false;
            modality.Status = "Pending";
            modality.EventID = eventid;

            return modality;
        }
    }
}
