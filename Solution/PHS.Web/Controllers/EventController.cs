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
            IEnumerable<PHS.DB.@event> events = new List<PHS.DB.@event>();
            using (var eventManager = new EventManager())
            {
                events = eventManager.GetAllEvents();
            }

            return View(events);
        }

        public ActionResult Edit(int id)
        {
            @event eventModel;

            using (var eventManager = new EventManager())
            {
                eventModel = eventManager.GetEventByID(id);
            }

            return View(eventModel);
        }

        [HttpPost]
        public ActionResult Edit(@event eventModel)
        {
            //write code to update student 

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "ID")]@event eventModel)
        {
            if (ModelState.IsValid)
            {
                using (var eventManager = new EventManager())
                {
                    eventModel.CreateBy = "";

                    eventManager.NewEvent(eventModel);
                }


            }
            else {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
               
            }

            return RedirectToAction("Index");
        }



        private readonly List<PHS.DB.@event> clients = new List<PHS.DB.@event>()
    {
        new PHS.DB.@event { Title="asdas" , ID = 1},
new PHS.DB.@event { Title="asdas", ID= 2 }

    };




    }
}