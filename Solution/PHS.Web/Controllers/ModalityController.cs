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

        // GET: Modality/Details?modalityid=13&eventid=3
        public ActionResult Details(int modalityid, int eventid)
        {

            return View();
        }

        // GET: Modality/Create?eventid=3
        public ActionResult Create(int eventid)
        {
            ModalityEventViewModel modality = initModalityEventView(eventid);

            return View(modality);
        }

        // POST: Modality/Create
        [HttpPost]
        public ActionResult Create(ModalityEventViewModel viewModel)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            validateModality(viewModel);

            if (ModelState.IsValid)
            {
                string message = string.Empty;

                uploadImage(viewModel);

                try
                {
                    using (var modalityManager = new ModalityManager())
                    {
                        modalityManager.NewModality(viewModel, out message);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLog(ex);
                    message = Constants.OperationFailedDuringAddingValue("Modality");

                    SetViewBagError(message);
                    return View(viewModel);
                }

                return RedirectToAction("Edit", "Event", new { id = viewModel.EventID });
            }

            return View(viewModel);

        }

        // GET: Modality/Edit?modalityid=13&eventid=3
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

        // POST: Modality/Edit
        [HttpPost]
        public ActionResult Edit(ModalityEventViewModel viewModel)
        {
            if(viewModel.ImageUpload != null) { 
                validateModality(viewModel);
            }

            if (ModelState.IsValid)
            {
                string errormsg = string.Empty;

                uploadImage(viewModel);

                try
                {
                    using (var modalityManager = new ModalityManager())
                    {
                        modalityManager.UpdateModality(viewModel, out errormsg);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionLog(ex);
                    errormsg = Constants.OperationFailedDuringAddingValue("Modality");

                    SetViewBagError(errormsg);
                    return View(viewModel);
                }

                return RedirectToAction("Edit", "Event", new { id = viewModel.EventID });
            }

            return View(viewModel);
        }

        // GET: Modality/Delete?modalityid=13&eventid=3
        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Delete(int modalityid, int eventid)
        {

            string message = string.Empty;
            using (var eManager = new EventManager())
            {
                bool isDeleted = eManager.DeleteEventModality(modalityid, eventid, out message);
                if (isDeleted == false)
                {
                    SetTempDataError(message);
                    return RedirectToAction("Edit", "Event", new { id = eventid });
                }
                    SetTempDataMessage(Constants.ValueSuccessfuly("Modality has been deleted"));
            }

            return RedirectToAction("Edit", "Event", new { id = eventid });
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
            view.Forms = modality.Forms; 

            return view;
        }

        private void validateModality(ModalityEventViewModel viewModel)
        {
            var validImageTypes = new string[]
                        {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
                        };

            if (viewModel.ImageUpload == null || viewModel.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "This field is required");
            }
            else if (!validImageTypes.Contains(viewModel.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }
        }

        private void uploadImage(ModalityEventViewModel viewModel)
        {
            if (viewModel.ImageUpload != null && viewModel.ImageUpload.ContentLength > 0)
            {
                var uploadDir = "~/Content/images/Modality";
                var imagePath = Path.Combine(Server.MapPath(uploadDir), viewModel.ImageUpload.FileName);
                var imageUrl = Path.Combine(uploadDir, viewModel.ImageUpload.FileName);
                viewModel.ImageUpload.SaveAs(imagePath);
                viewModel.IconPath = imageUrl;
            }
        }
    }
        
}
