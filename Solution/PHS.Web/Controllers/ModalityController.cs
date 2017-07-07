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
using System.IO;

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
        public ActionResult Details(int modalityid, int eventid)
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

            var validImageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (modalityEventView.ImageUpload == null || modalityEventView.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "This field is required");
            }
            else if (!validImageTypes.Contains(modalityEventView.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }

            if (ModelState.IsValid)
            {
                string message = string.Empty;

                if (modalityEventView.ImageUpload != null && modalityEventView.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/Content/images/Modality"; 
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), modalityEventView.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, modalityEventView.ImageUpload.FileName);
                    modalityEventView.ImageUpload.SaveAs(imagePath);
                    modalityEventView.IconPath = imageUrl;
                }

                try
                {
                    using (var modalityManager = new ModalityManager())
                    {
                        modalityManager.NewModality(modalityEventView, out message);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLog(ex);
                    message = Constants.OperationFailedDuringAddingValue("Modality");

                    SetViewBagError(message);
                    return View(modalityEventView);
                }

                return RedirectToAction("Edit", "Event", new { id = modalityEventView.EventID });
            }

            return View(modalityEventView);

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
        public ActionResult Edit(int modalityid, int eventid)
        {
            string message = string.Empty;
            using (var mManager = new ModalityManager())
            {
                Modality modality = mManager.GetModalityByID(modalityid, out message);
                if (modality == null)
                {
                    SetViewBagError(message);
                }

                ModalityEventViewModel view = MapModalityToView(modality);
                view.EventID = eventid;

                return View(view);
            };
        }

        // POST: Modality/Edit/5
        [HttpPost]
        public ActionResult Edit(ModalityEventViewModel viewModel)
        {
            using (var mManager = new ModalityManager())
            {
                //mManager.UpdateModality(); TODO
            }

            return RedirectToAction("Edit", "Event", new { id = viewModel.EventID });
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

        private ModalityEventViewModel MapModalityToView(Modality modality)
        {
            ModalityEventViewModel view = new ModalityEventViewModel();
            Util.CopyNonNullProperty(modality, view);

            return view;
        }
    }
        
}
