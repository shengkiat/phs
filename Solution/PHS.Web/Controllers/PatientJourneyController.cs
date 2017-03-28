using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PHS.Business.ViewModel.PatientJourney;
using PHS.Business.Implementation;
using PHS.DB;

namespace PHS.Web.Controllers
{
    public class PatientJourneyController : BaseController
    {
        // GET: PatientJourney
        public ActionResult Index()
        {
            return View();
        }

        // Both GET and POST: /PatientJourney/SearchPatient
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SearchPatient(PatientSearchModel psm)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            if (psm == null)
            {
                return View();
            }

            string message = string.Empty;
            PatientSearchModel result = new PatientSearchModel();

            using (var getPatientJourney = new PatientJourneyManager())
            {
                IList<PatientEventViewModel> patientEvents = getPatientJourney.GetPatientEventsByNric(psm.IcFirstDigit, psm.IcNumber, psm.IcLastDigit, out message);
                if (patientEvents == null)
                {
                    SetViewBagError(message);
                }

                else
                {
                    result.PatientEvents = patientEvents;
                }
            }

            return View(result);
        }

        // Both GET and POST: /PatientJourney/JourneyModality
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult JourneyModality(string nric, string eventId)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToLogin();
            }

            if (string.IsNullOrEmpty(nric) || string.IsNullOrEmpty(eventId))
            {
                return Redirect("~/patientjourney");
            }

            string message = string.Empty;
            PatientEventViewModel result = new PatientEventViewModel();

            using (var getPatientJourney = new PatientJourneyManager())
            {
                PatientEventViewModel patientEvent = getPatientJourney.GetPatientEvent(nric, eventId, out message);
                if (patientEvent == null)
                {
                    SetViewBagError(message);
                }

                else
                {
                    result = patientEvent;
                }
            }

            return View(result);
        }

        public ActionResult JourneyModalityCircles()
        {
            //ModalityCircleViewModel modalityCircle = new ModalityCircleViewModel();
            //List<ModalityCircleViewModel> modalityCircleList = createModalityCircleData();

            string message = string.Empty;
            string nric = "S8518538A";
            string eventId = "100"; 
            PatientEventViewModel result = new PatientEventViewModel();
            using (var getPatientJourney = new PatientJourneyManager())
            {
                PatientEventViewModel patientEvent = getPatientJourney.GetPatientEvent(nric, eventId, out message);
                if (patientEvent == null)
                {
                    SetViewBagError(message);
                }

                else
                {
                    result = patientEvent;
                }
            }

            List<Modality> modalityList = new List<Modality>();
            modalityList = (List<Modality>) result.Event.Modalities; 

            return View(modalityList);
        }


        public PartialViewResult PassJSON(List<Modality> modalityList)
        {
            modalityList.ElementAt(1).Name = "newname"; 
            modalityList.ElementAt(2).IsActive = true;

            return PartialView("_JourneyModalityCirclesPartial", modalityList);
        }

        public PartialViewResult UpdatePtJourney()
        {
            //ModalityCircleViewModel modalityCircle = new ModalityCircleViewModel();
            //List<ModalityCircleViewModel> modalityCircleList = createModalityCircleData();

          
            //    // status = Pending, InProgress, Completed            
            //    modalityCircle.Name = "Registration";
            //    modalityCircle.Position = 0;
            //    modalityCircle.Active = true;
            //    modalityCircle.Visible = true;
            //    modalityCircle.IconPath = "../../Content/images/Modality/achievement.png";
            //    modalityCircle.HasParent = false;
            //    modalityCircle.Status = "Pending";
            //modalityCircleList.Add(modalityCircle);

            //modalityCircleList.ElementAt(2).Active = true;


            string message = string.Empty;
            string nric = "S8518538A";
            string eventId = "100";
            PatientEventViewModel result = new PatientEventViewModel();
            using (var getPatientJourney = new PatientJourneyManager())
            {
                PatientEventViewModel patientEvent = getPatientJourney.GetPatientEvent(nric, eventId, out message);
                if (patientEvent == null)
                {
                    SetViewBagError(message);
                }

                else
                {
                    result = patientEvent;
                }
            }

            List<Modality> modalityList = new List<Modality>();
            modalityList = (List<Modality>)result.Event.Modalities;

            modalityList.ElementAt(2).IsActive = true;

            return PartialView("_JourneyModalityCirclesPartial", modalityList);
        }

        public PartialViewResult UpdatePtJourneyReset()
        {
            //ModalityCircleViewModel modalityCircle = new ModalityCircleViewModel();
            //List<ModalityCircleViewModel> modalityCircleList = createModalityCircleData();

            string message = string.Empty;
            string nric = "S8518538A";
            string eventId = "100";
            PatientEventViewModel result = new PatientEventViewModel();
            using (var getPatientJourney = new PatientJourneyManager())
            {
                PatientEventViewModel patientEvent = getPatientJourney.GetPatientEvent(nric, eventId, out message);
                if (patientEvent == null)
                {
                    SetViewBagError(message);
                }

                else
                {
                    result = patientEvent;
                }
            }

            List<Modality> modalityList = new List<Modality>();
            modalityList = (List<Modality>)result.Event.Modalities;

            return PartialView("_JourneyModalityCirclesPartial", modalityList);
        }

        //public List<ModalityCircleViewModel> createModalityCircleData()
        //{
        //    ModalityCircleViewModel modalityCircle = new ModalityCircleViewModel();
        //    List<ModalityCircleViewModel> modalityCircleList = new List<ModalityCircleViewModel>();

        //    // status = Pending, InProgress, Completed

        //    modalityCircle = new ModalityCircleViewModel();
        //    modalityCircle.Name = "Registration";
        //    modalityCircle.Position = 0;
        //    modalityCircle.Active = true;
        //    modalityCircle.Visible = true;
        //    modalityCircle.IconPath = "../../Content/images/Modality/achievement.png";
        //    modalityCircle.HasParent = false;
        //    modalityCircle.Status = "Pending";
        //    modalityCircleList.Add(modalityCircle);

        //    modalityCircle = new ModalityCircleViewModel();
        //    modalityCircle.Name = "History Taking";
        //    modalityCircle.Position = 1;
        //    modalityCircle.Active = true;
        //    modalityCircle.Visible = true;
        //    modalityCircle.IconPath = "../../Content/images/Modality/abacus.png";
        //    modalityCircle.HasParent = true;
        //    modalityCircle.Status = "Pending";
        //    modalityCircleList.Add(modalityCircle);

        //    modalityCircle = new ModalityCircleViewModel();
        //    modalityCircle.Name = "FIT";
        //    modalityCircle.Position = 2;
        //    modalityCircle.Active = false;
        //    modalityCircle.Visible = true;
        //    modalityCircle.IconPath = "../../Content/images/Modality/agenda.png";
        //    modalityCircle.HasParent = true;
        //    modalityCircle.Status = "Pending";
        //    modalityCircleList.Add(modalityCircle);

        //    modalityCircle = new ModalityCircleViewModel();
        //    modalityCircle.Name = "TeleHealth";
        //    modalityCircle.Position = 3;
        //    modalityCircle.Active = true;
        //    modalityCircle.Visible = false;
        //    modalityCircle.IconPath = "../../Content/images/Modality/balance.png";
        //    modalityCircle.HasParent = false;
        //    modalityCircle.Status = "Pending";
        //    modalityCircleList.Add(modalityCircle);

        //    return modalityCircleList; 
        //}


    }
}