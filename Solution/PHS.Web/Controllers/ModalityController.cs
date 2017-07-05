using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PHS.Web.Controllers
{
    public class ModalityController : Controller
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Modality/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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
    }
}
