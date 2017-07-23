using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PHS.DB;
using PHS.Repository.Context;
using PHS.Web.Controllers;
using PHS.Business.Implementation;
using PHS.Common;

namespace PHS.Web.Areas.Admin.Controllers
{
    public class StandardReferenceController : BaseController
    {
        private PHSContext db = new PHSContext();

        // GET: Admin/StandardReference
        public ActionResult Index()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;
            using (var standardReferenceManager = new StandardReferenceManager())
            {
                var standardReferences = standardReferenceManager.GetAllStandardReferences(out message);
                GetErrorAneMessage();
                return View(standardReferences);
            }
        }

        // GET: Admin/StandardReference/Details/5
        public ActionResult Details(int id = 0)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var standardReferenceManager = new StandardReferenceManager())
            {
                StandardReference standardReference = standardReferenceManager.GetStandardReferenceByID(id, out message);
                if (standardReference == null)
                {
                    SetViewBagError(message);
                    return RedirectToAction("Index");
                }
                return View(standardReference);
            }
        }

        // GET: Admin/StandardReference/Create
        public ActionResult Create()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            return View();
        }

        // POST: Admin/StandardReference/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title")] StandardReference standardReference)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;

            using (var standardReferenceManager = new StandardReferenceManager())
            {
                var newStandardReference = standardReferenceManager.AddStandardReference(GetLoginUser(), standardReference, out message);
                if (newStandardReference == null)
                {
                    SetViewBagError(message);
                    return View();
                }

                SetTempDataMessage(Constants.ValueSuccessfuly( "Standard Reference " + standardReference.Title + " has been created"));
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/StandardReference/CreateReferenceRange
        public ActionResult CreateReferenceRange(int standardReferenceID = 0)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var standardReferenceManager = new StandardReferenceManager())
            {
                StandardReference standardReference = standardReferenceManager.GetStandardReferenceByID(standardReferenceID, out message);
                if (standardReference == null)
                {
                    SetViewBagError("Invalid Standard Reference ID. Create Standard Reference first.");
                    return View();
                }
                ReferenceRange referenceRange = new ReferenceRange();
                referenceRange.StandardReferenceID = standardReferenceID;

                return View(referenceRange);
            }
        }

        // POST: Admin/StandardReference/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReferenceRange([Bind(Exclude = "ReferenceRangeID")] ReferenceRange referenceRange)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;

            using (var referenceRangeManager = new ReferenceRangeManager())
            {
                var newReferenceRange = referenceRangeManager.AddReferenceRange(GetLoginUser(), referenceRange, out message);
                if (newReferenceRange == null)
                {
                    SetViewBagError(message);
                    return View();
                }

                SetTempDataMessage(Constants.ValueSuccessfuly("Reference Range " + newReferenceRange.Title + " has been added"));
                return RedirectToAction("Edit",new { id = newReferenceRange.StandardReferenceID });
            }
        }

        // GET: Admin/StandardReference/Edit/5
        public ActionResult Edit(int id = 0)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var standardReferenceManager = new StandardReferenceManager())
            {
                var standardReference = standardReferenceManager.GetStandardReferenceByID(id, out message);
                if (standardReference == null)
                {
                    SetViewBagError(message);
                    return RedirectToAction("Index");
                }
                return View(standardReference);
            };
        }

        // POST: Admin/StandardReferences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StandardReference standardReference)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;

            using (var standardReferenceManager = new StandardReferenceManager())
            {
                if (standardReferenceManager.UpdateStandardReference(GetLoginUser(), standardReference, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Standard Reference " + standardReference.Title + " has been updated"));
                    return RedirectToAction("Index");
                }
                SetViewBagError(message);
                return View(standardReference);
            }
        }

        // GET: Admin/StandardReference/EditReferenceRange
        public ActionResult EditReferenceRange(int referenceRangeID = 0, int standardreferenceID = 0)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var referenceRangeManager = new ReferenceRangeManager())
            {
                var referenceRange = referenceRangeManager.GetReferenceRangeByID(referenceRangeID, out message);
                if (referenceRange == null)
                {
                    SetViewBagError(message);
                    return RedirectToAction("Edit", new { id = standardreferenceID });
                }
                return View(referenceRange);
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditReferenceRange(ReferenceRange referenceRange)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }
            string message = string.Empty;

            using (var referenceRangeManager = new ReferenceRangeManager())
            {
                if (referenceRangeManager.UpdateReferenceRange(GetLoginUser(), referenceRange, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Reference Range " + referenceRange.Title + " has been updated"));
                    return RedirectToAction("Edit", new { id = referenceRange.StandardReferenceID });
                }
                SetViewBagError(message);
                return View(referenceRange);
            }
        }

        // GET: Admin/StandardReference/Delete/5
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;

            using (var standardReferenceManager = new StandardReferenceManager())
            {
                if (standardReferenceManager.DeleteStandardReference(id, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Standard Reference has been deleted"));
                    return RedirectToAction("Index");
                }
                SetViewBagError(message);

                return RedirectToAction("Index");
            }
        }

        // GET: Admin/StandardReference/DeleteReferenceRange/5
        [HttpGet]
        public ActionResult DeleteReferenceRange(int referenceRangeID = 0, int standardreferenceID = 0)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            string message = string.Empty;
            using (var referenceRangeManager = new ReferenceRangeManager())
            {
                if (referenceRangeManager.DeleteReferenceRange(referenceRangeID, out message))
                {
                    SetTempDataMessage(Constants.ValueSuccessfuly("Reference Range has been deleted"));
                    return RedirectToAction("Edit", new { id = standardreferenceID });
                }
                SetViewBagError(message);

                return RedirectToAction("Edit", new { id = standardreferenceID });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
