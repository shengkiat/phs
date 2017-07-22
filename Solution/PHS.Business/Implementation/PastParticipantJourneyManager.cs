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
using PHS.Business.ViewModel.PastParticipantJourney;
using PHS.Business.ViewModel.ParticipantJourney;

namespace PHS.Business.Implementation
{
    public class PastParticipantJourneyManager : BaseManager, IPastParticipantJourneyManager, IManagerFactoryBase<IPastParticipantJourneyManager>
    {
        public IPastParticipantJourneyManager Create()
        {
            return new PastParticipantJourneyManager();
        }

        public IList<ParticipantJourneyViewModel> GetAllParticipantJourneyByNric(string nric, out string message)
        {
            IList<ParticipantJourneyViewModel> result = new List<ParticipantJourneyViewModel>();
            message = string.Empty;

            if (string.IsNullOrEmpty(nric))
            {
                message = "Nric cannot be null";
            }

            else if (!NricChecker.IsNRICValid(nric))
            {
                message = "Invalid Nric";
            }

            else
            {
                try
                {
                    using (var unitOfWork = CreateUnitOfWork())
                    {
                        var participant = unitOfWork.Participants.FindParticipants(u => u.Nric.Equals(nric, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                        if (participant != null)
                        {
                            DateTime currentTime = DateTime.Now;
                            foreach (PHSEvent phsEvent in participant.PHSEvents)
                            {
                                if (currentTime.Ticks > phsEvent.EndDT.Ticks)
                                {
                                    result.Add(new ParticipantJourneyViewModel(participant, phsEvent.PHSEventID));
                                }
                                
                            }

                            if (result.Count == 0)
                            {
                                message = "No result found";
                            }
                            
                            return result;
                        }
                        else
                        {
                            message = "Participant not found!";
                            return result;
                        }
                    }
                }

                catch (Exception ex)
                {
                    ExceptionLog(ex);
                    message = Constants.OperationFailedDuringRetrievingValue("GetAllParticipantJourneyByNric");
                    return null;
                }
            }

            return result;
        }

        [System.Obsolete("To be deprecated since use by formImport")]
        public PatientEventViewModel GetPatientEvent(string nric, string eventId, out string message)
        {
            PatientEventViewModel result = null;
            message = string.Empty;

            if (NricChecker.IsNRICValid(nric) && !string.IsNullOrEmpty(eventId))
            {
                //result = getMockData(nric, eventId);
                try
                {
                    using (var unitOfWork = new UnitOfWork(new PHSContext()))
                    {
                        int intEventId = int.Parse(eventId);
                        var eventpatient = unitOfWork.Participants.FindParticipant(nric, intEventId);
                        //Nric = getMockData(nric);

                        if (eventpatient != null)
                        {
                            message = string.Empty;
                            result = new PatientEventViewModel(eventpatient);

                            return result;
                        }
                        else
                        {
                            message = "Event Patient not found!";
                            return result;
                        }
                    }
                }

                catch (Exception ex)
                {
                    ExceptionLog(ex);
                    message = Constants.OperationFailedDuringRetrievingValue("GetPatientEvent");
                    return null;
                }
            }


            else
            {
                message = "Invalid Nric/Event!";
            }

            return result;
        }

        [System.Obsolete("To be deprecated since use by formImport")]
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
            modalityOne.ModalityID = 50;
            modalityOne.Name = "Registration";
            modalityOne.Position = 0;
            modalityOne.IsActive = true;
            modalityOne.IsVisible = true;
            modalityOne.IconPath = "../../Content/images/Modality/01registration.png";
            modalityOne.HasParent = false;
            modalityOne.Status = "Pending";
            //modalityOne.ModalityForms = new List<ModalityForm>();

            //Registration form
            //ModalityForm modalityFormOne = new ModalityForm();
           // modalityFormOne.ModalityID = 50;
           // modalityFormOne.FormID = 1;
          //  modalityOne.ModalityForms.Add(modalityFormOne);

            Modality modalityTwo = new Modality();
            modalityTwo.ModalityID = 2;
            modalityTwo.Name = "History Taking";
            modalityTwo.Position = 1;
            modalityTwo.IsActive = true;
            modalityTwo.IsVisible = true;
            modalityTwo.IconPath = "../../Content/images/Modality/02historytaking.png";
            modalityTwo.HasParent = true;
            modalityTwo.Status = "Pending";
         //   modalityTwo.ModalityForms = new List<ModalityForm>();

            //BMI form
       //     ModalityForm modalityFormTwo = new ModalityForm();
        //    modalityFormTwo.ModalityID = 2;
         //   modalityFormTwo.FormID = 3;
         //   modalityTwo.ModalityForms.Add(modalityFormTwo);

            Modality modalityThree = new Modality();
            modalityThree.ModalityID = 3;
            modalityThree.Name = "Mega Sorting Station";
            modalityThree.Position = 2;
            modalityThree.IsActive = true;
            modalityThree.IsVisible = true;
            modalityThree.IconPath = "../../Content/images/Modality/03megasorting.png";
            modalityThree.HasParent = true;
            modalityThree.Status = "Pending";

            Modality modalityFour = new Modality();
            modalityFour.ModalityID = 4;
            modalityFour.Name = "Phlebotomy";
            modalityFour.Position = 3;
            modalityFour.IsActive = false;
            modalityFour.IsVisible = false;
            modalityFour.IconPath = "../../Content/images/Modality/04phlebo.png";
            modalityFour.HasParent = true;
            modalityFour.Status = "Pending";

            Modality modalityFive = new Modality();
            modalityFive.ModalityID = 5;
            modalityFive.Name = "FIT";
            modalityFive.Position = 4;
            modalityFive.IsActive = false;
            modalityFive.IsVisible = false;
            modalityFive.IconPath = "../../Content/images/Modality/05fit.png";
            modalityFive.HasParent = true;
            modalityFive.Status = "Pending";

            Modality modalitySix = new Modality();
            modalitySix.ModalityID = 6;
            modalitySix.Name = "Woman Cancer";
            modalitySix.Position = 5;
            modalitySix.IsActive = false;
            modalitySix.IsVisible = false;
            modalitySix.IconPath = "../../Content/images/Modality/06woman.png";
            modalitySix.HasParent = true;
            modalitySix.Status = "Pending";

            Modality modalitySeven = new Modality();
            modalitySeven.ModalityID = 7;
            modalitySeven.Name = "Geriatric Screening";
            modalitySeven.Position = 6;
            modalitySeven.IsActive = false;
            modalitySeven.IsVisible = false;
            modalitySeven.IconPath = "../../Content/images/Modality/07geri.png";
            modalitySeven.HasParent = true;
            modalitySeven.Status = "Pending";

            Modality modalityEight = new Modality();
            modalityEight.ModalityID = 8;
            modalityEight.Name = "Oral";
            modalityEight.Position = 7;
            modalityEight.IsActive = false;
            modalityEight.IsVisible = false;
            modalityEight.IconPath = "../../Content/images/Modality/08oral.png";
            modalityEight.HasParent = true;
            modalityEight.Status = "Pending";

            Modality modalityNine = new Modality();
            modalityNine.ModalityID = 9;
            modalityNine.Name = "Doctor's Consult";
            modalityNine.Position = 8;
            modalityNine.IsActive = false;
            modalityNine.IsVisible = false;
            modalityNine.IconPath = "../../Content/images/Modality/09doctor.png";
            modalityNine.HasParent = false;
            modalityNine.Status = "Pending";

            Modality modalityTen = new Modality();
            modalityTen.ModalityID = 10;
            modalityTen.Name = "Exhibition";
            modalityTen.Position = 9;
            modalityTen.IsActive = true;
            modalityTen.IsVisible = false;
            modalityTen.IconPath = "../../Content/images/Modality/10exhibition.png";
            modalityTen.HasParent = false;
            modalityTen.Status = "Pending";

            Modality modalityEleven = new Modality();
            modalityEleven.ModalityID = 11;
            modalityEleven.Name = "Summary";
            modalityEleven.Position = 10;
            modalityEleven.IsActive = true;
            modalityEleven.IsVisible = false;
            modalityEleven.IconPath = "../../Content/images/Modality/11summary.png";
            modalityEleven.HasParent = false;
            modalityEleven.Status = "Pending";



            PHSEvent eventOne = new PHSEvent();
            eventOne.PHSEventID = 100;
            eventOne.Title = "2016 - Event";
            eventOne.Modalities = new List<Modality>();
            eventOne.Modalities.Add(modalityOne);
            eventOne.Modalities.Add(modalityTwo);
            eventOne.Modalities.Add(modalityThree);
            eventOne.Modalities.Add(modalityFour);
            eventOne.Modalities.Add(modalityFive);
            eventOne.Modalities.Add(modalitySix);
            eventOne.Modalities.Add(modalitySeven);
            eventOne.Modalities.Add(modalityEight);
            eventOne.Modalities.Add(modalityNine);
            eventOne.Modalities.Add(modalityTen);
            eventOne.Modalities.Add(modalityEleven);

            PHSEvent eventTwo = new PHSEvent();
            eventTwo.PHSEventID = 200;
            eventTwo.Title = "2015 - Event";
            eventTwo.Modalities = new List<Modality>();
            eventTwo.Modalities.Add(modalityOne);
            eventTwo.Modalities.Add(modalityTwo);
            eventTwo.Modalities.Add(modalityThree);
            eventTwo.Modalities.Add(modalityFour);
            eventTwo.Modalities.Add(modalityFive);
            eventTwo.Modalities.Add(modalitySix);
            eventTwo.Modalities.Add(modalitySeven);
            eventTwo.Modalities.Add(modalityEight);
            eventTwo.Modalities.Add(modalityNine);
            eventTwo.Modalities.Add(modalityTen);
            eventTwo.Modalities.Add(modalityEleven);

            patientOne.FullName = "ABCDE";
            patientOne.Nric = "S8518538A";
            //patientOne.Event = eventOne;
           // patientOne.ModalityCircles = modalityCircleList;
            firstRecords.Add(patientOne);

            PatientEventViewModel patientTwo = new PatientEventViewModel();
            patientTwo.FullName = "ABCDE";
            patientTwo.Nric = "S8518538A";
           // patientTwo.Event = eventTwo;
            //patientTwo.ModalityCircles = modalityCircleList;
            firstRecords.Add(patientTwo);

            mockData.Add("S8518538A", firstRecords);

           

            return mockData[nric];
        }

        [System.Obsolete("To be deprecated since use by formImport")]
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
