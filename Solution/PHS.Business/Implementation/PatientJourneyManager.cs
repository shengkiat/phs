using PHS.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.DB;
using PHS.Repository;
using PHS.Repository.Context;
using PHS.Business.Common;
using PHS.Common;
using PHS.Business.ViewModel.PatientJourney;

namespace PHS.Business.Implementation
{
    public class PatientJourneyManager : BaseManager, IPatientJourneyManager, IManagerFactoryBase<IPatientJourneyManager>
    {
        public IPatientJourneyManager Create()
        {
            return new PatientJourneyManager();
        }

        public IList<PatientEventViewModel> GetPatientEventsByNric(string icFirstDigit, string icNumber, string icLastDigit, out string message)
        {
            IList<PatientEventViewModel> result = null;
            message = string.Empty;

            if (NricChecker.IsNRICValid(icFirstDigit, icNumber, icLastDigit))
            {
                string nric = icFirstDigit + icNumber + icLastDigit;
               
                result = getMockData(nric);
            }


            else
            {
                message = "Invalid Nric!";
            }

            return result;
        }

        public PatientEventViewModel GetPatientEvent(string nric, string eventId, out string message)
        {
            PatientEventViewModel result = null;
            message = string.Empty;

            if (NricChecker.IsNRICValid(nric) && !string.IsNullOrEmpty(eventId))
            {
                result = getMockData(nric, eventId);
                if (result == null)
                {
                    message = "Unable to find using Nric and Event";
                }
            }


            else
            {
                message = "Invalid Nric/Event!";
            }

            return result;
        }


        private List<PatientEventViewModel> getMockData(string nric)
        {
            Dictionary<string, List<PatientEventViewModel>> mockData = new Dictionary<string, List<PatientEventViewModel>>();

            //ModalityCircleViewModel modalityCircle = new ModalityCircleViewModel();
            //List<ModalityCircleViewModel> modalityCircleList = new List<ModalityCircleViewModel>();

            //// status = Pending, InProgress, Completed

            //modalityCircle = new ModalityCircleViewModel();
            //modalityCircle.Name = "Registration";
            //modalityCircle.Position = 0;
            //modalityCircle.Active = true;
            //modalityCircle.Visible = true;
            //modalityCircle.IconPath = "../../Content/images/Modality/achievement.png";
            //modalityCircle.HasParent = false;
            //modalityCircle.Status = "Pending";
            //modalityCircleList.Add(modalityCircle);

            //modalityCircle = new ModalityCircleViewModel();
            //modalityCircle.Name = "History Taking";
            //modalityCircle.Position = 1;
            //modalityCircle.Active = true;
            //modalityCircle.Visible = true;
            //modalityCircle.IconPath = "../../Content/images/Modality/abacus.png";
            //modalityCircle.HasParent = true;
            //modalityCircle.Status = "Pending";
            //modalityCircleList.Add(modalityCircle);

            //modalityCircle = new ModalityCircleViewModel();
            //modalityCircle.Name = "FIT";
            //modalityCircle.Position = 2;
            //modalityCircle.Active = false;
            //modalityCircle.Visible = true;
            //modalityCircle.IconPath = "../../Content/images/Modality/agenda.png";
            //modalityCircle.HasParent = true;
            //modalityCircle.Status = "Pending";
            //modalityCircleList.Add(modalityCircle);

            //modalityCircle = new ModalityCircleViewModel();
            //modalityCircle.Name = "TeleHealth";
            //modalityCircle.Position = 3;
            //modalityCircle.Active = true;
            //modalityCircle.Visible = false;
            //modalityCircle.IconPath = "../../Content/images/Modality/balance.png";
            //modalityCircle.HasParent = false;
            //modalityCircle.Status = "Pending";
            //modalityCircleList.Add(modalityCircle);

            List<PatientEventViewModel> firstRecords = new List<PatientEventViewModel>();
            PatientEventViewModel patientOne = new PatientEventViewModel();

            Modality modalityOne = new Modality();
            modalityOne.ID = 50;
            modalityOne.Name = "Registration";
            modalityOne.Position = 0;
            modalityOne.IsActive = true;
            modalityOne.IsVisible = true;
            modalityOne.IconPath = "../../Content/images/Modality/achievement.png";
            modalityOne.HasParent = false;
            modalityOne.Status = "Pending";
            modalityOne.ModalityForms = new List<ModalityForm>();

            ModalityForm modalityFormOne = new ModalityForm();
            modalityFormOne.ModalityID = 50;
            modalityFormOne.FormID = 1;
            modalityOne.ModalityForms.Add(modalityFormOne);

            ModalityForm modalityFormTwo = new ModalityForm();
            modalityFormTwo.ModalityID = 100;
            modalityFormTwo.FormID = 2;
            modalityOne.ModalityForms.Add(modalityFormTwo);

            Modality modalityTwo = new Modality();
            modalityTwo.ID = 2;
            modalityTwo.Name = "History Taking";
            modalityTwo.Position = 1;
            modalityTwo.IsActive = true;
            modalityTwo.IsVisible = true;
            modalityTwo.IconPath = "../../Content/images/Modality/abacus.png";
            modalityTwo.HasParent = true;
            modalityTwo.Status = "Pending";
            modalityTwo.ModalityForms = new List<ModalityForm>();

            Modality modalityThree = new Modality();
            modalityThree.ID = 3;
            modalityThree.Name = "FIT";
            modalityThree.Position = 2;
            modalityThree.IsActive = false;
            modalityThree.IsVisible = true;
            modalityThree.IconPath = "../../Content/images/Modality/agenda.png";
            modalityThree.HasParent = true;
            modalityThree.Status = "Pending";

            Modality modalityFour = new Modality();
            modalityFour.ID = 4;
            modalityFour.Name = "TeleHealth";
            modalityFour.Position = 3;
            modalityFour.IsActive = true;
            modalityFour.IsVisible = false;
            modalityFour.IconPath = "../../Content/images/Modality/balance.png";
            modalityFour.HasParent = false;
            modalityFour.Status = "Pending";




            @event eventOne = new @event();
            eventOne.ID = 100;
            eventOne.Title = "2016 - Event";
            eventOne.Modalities = new List<Modality>();
            eventOne.Modalities.Add(modalityOne);
            eventOne.Modalities.Add(modalityTwo);
            eventOne.Modalities.Add(modalityThree);
            eventOne.Modalities.Add(modalityFour);

            @event eventTwo = new @event();
            eventTwo.ID = 200;
            eventTwo.Title = "2015 - Event";
            eventTwo.Modalities = new List<Modality>();
            eventTwo.Modalities.Add(modalityOne);
            eventTwo.Modalities.Add(modalityTwo);
            eventTwo.Modalities.Add(modalityThree);
            eventTwo.Modalities.Add(modalityFour);

            patientOne.FullName = "ABCDE";
            patientOne.Nric = "S8518538A";
            patientOne.Event = eventOne;
           // patientOne.ModalityCircles = modalityCircleList;
            firstRecords.Add(patientOne);

            PatientEventViewModel patientTwo = new PatientEventViewModel();
            patientTwo.FullName = "ABCDE";
            patientTwo.Nric = "S8518538A";
            patientTwo.Event = eventTwo;
            //patientTwo.ModalityCircles = modalityCircleList;
            firstRecords.Add(patientTwo);

            mockData.Add("S8518538A", firstRecords);

           

            return mockData[nric];
        }

        private PatientEventViewModel getMockData(string nric, string eventId)
        {
            List<PatientEventViewModel> patientEvents = getMockData(nric);

            foreach(PatientEventViewModel patientEvent in patientEvents)
            {
                if (patientEvent.EventId.Equals(eventId))
                {
                    return patientEvent;
                }
            }

            return null;
        }
    }
    
}
