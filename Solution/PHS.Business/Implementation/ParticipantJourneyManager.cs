using PHS.Business.Common;
using PHS.Business.Implementation.FillIn;
using PHS.Business.Interface;
using PHS.Business.ViewModel;
using PHS.Business.ViewModel.ParticipantJourney;
using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using static PHS.Common.Constants;

namespace PHS.Business.Implementation
{
    public class ParticipantJourneyManager : BaseParticipantJourneyManager, IParticipantJourneyManager, IManagerFactoryBase<IParticipantJourneyManager>
    {
        public IParticipantJourneyManager Create()
        {
            return new ParticipantJourneyManager();
        }

        public ParticipantJourneySearchViewModel RetrieveActiveScreeningEvent()
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                ParticipantJourneySearchViewModel result = new ParticipantJourneySearchViewModel();
                //result.PHSEvents = unitOfWork.Events.GetAll();
                result.PHSEvents = unitOfWork.Events.GetAllActiveEvents();

                return result;
            }
        }

        public ParticipantJourneyViewModel RetrieveParticipantJourney(ParticipantJourneySearchViewModel psm, out string message, out MessageType messageType)
        {
            message = string.Empty;
            messageType = MessageType.ERROR;

            ParticipantJourneyViewModel result = null;

            if (psm == null)
            {
                message = "Parameter cannot be null";
            }

            else if (string.IsNullOrEmpty(psm.Nric) || psm.PHSEventId == 0)
            {
                message = "Nric or PHSEventId cannot be null";
            }

            else if (!NricChecker.IsNRICValid(psm.Nric))
            {
                message = "Invalid Nric";
            }

            else
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    PHSEvent phsEvent = unitOfWork.Events.GetAllActiveEvents().Where(e => e.PHSEventID == psm.PHSEventId).FirstOrDefault();

                    if (phsEvent == null)
                    {
                        message = "Screening Event is not active";
                    }

                    else
                    {
                        Participant participant = unitOfWork.Participants.FindParticipant(psm.Nric, psm.PHSEventId);

                        if (participant != null)
                        {
                            result = new ParticipantJourneyViewModel(participant, psm.PHSEventId);
                        }

                        else
                        {

                            messageType = MessageType.PROMPT;

                            PreRegistration preRegistration = unitOfWork.PreRegistrations.FindPreRegistration(p => p.Nric.Equals(psm.Nric));
                            if (preRegistration == null)
                            {
                                message = "No registration record found. Do you want to register this Nric?";
                            }

                            else
                            {
                                message = "Do you want to register this Nric?";
                            }
                        }
                    }
                }
            }

            return result;
        }

        public ParticipantJourneyFormViewModel RetrieveParticipantJourneyForm(ParticipantJourneySearchViewModel psm, out string message)
        {
            message = string.Empty;

            ParticipantJourneyFormViewModel result = null;

            if (psm == null)
            {
                message = "Parameter cannot be null";
            }

            else if (string.IsNullOrEmpty(psm.Nric) || psm.PHSEventId == 0)
            {
                message = "Nric or PHSEventId cannot be null";
            }

            //todo: removed for data migration. to put back before live
            //else if (!NricChecker.IsNRICValid(psm.Nric))
            //{
            //    message = "Invalid Nric";
            //}

            else
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    Participant participant = unitOfWork.Participants.FindParticipant(psm.Nric, psm.PHSEventId);

                    if (participant != null)
                    {
                        result = new ParticipantJourneyFormViewModel(participant, psm.PHSEventId);
                    }

                    else
                    {
                        message = "No result found";
                    }

                }
            }

            return result;
        }

        public string RegisterParticipant(ParticipantJourneySearchViewModel psm)
        {
            string result = null;

            if (psm == null)
            {
                return "Parameter cannot be null";
            }

            else if (string.IsNullOrEmpty(psm.Nric) || psm.PHSEventId == 0)
            {
                return "Nric or PHSEventId cannot be null";
            }

            else if (!NricChecker.IsNRICValid(psm.Nric))
            {
                return "Invalid Nric";
            }

            else
            {
                using (var unitOfWork = CreateUnitOfWork())
                {

                    PHSEvent phsEvent = unitOfWork.Events.GetEventWithModalityForm(psm.PHSEventId);

                    if (phsEvent == null)
                    {
                        return "Screening Event is not active";
                    }

                    else
                    {
                        Participant participant = unitOfWork.Participants.FindParticipant(psm.Nric);

                        if (participant != null)
                        {
                            if (participant.PHSEvents.All(e => e.PHSEventID == psm.PHSEventId))
                            {
                                return "Invalid register participant";
                            }
                        }

                        PreRegistration preRegistration = unitOfWork.PreRegistrations.FindPreRegistration(p => p.Nric.Equals(psm.Nric));

                        using (TransactionScope scope = new TransactionScope())
                        {
                            if (participant == null)
                            {
                                participant = new Participant()
                                {
                                    Nric = psm.Nric
                                };

                                copyPreRegistrationToParticipant(participant, preRegistration);
                                unitOfWork.Participants.AddParticipantWithPHSEvent(participant, phsEvent);
                            }

                            else
                            {
                                copyPreRegistrationToParticipant(participant, preRegistration);
                                unitOfWork.Participants.AddPHSEventToParticipant(participant, phsEvent);
                            }

                            foreach(var modality in phsEvent.Modalities)
                            {
                                if (modality.IsMandatory)
                                {
                                    foreach (var form in modality.Forms)
                                    {
                                       
                                        ParticipantJourneyModality participantJourneyModality = new ParticipantJourneyModality()
                                        {
                                            ParticipantID = participant.ParticipantID,
                                            PHSEventID = phsEvent.PHSEventID,
                                            FormID = form.FormID,
                                            ModalityID = modality.ModalityID
                                        };

                                        participant.ParticipantJourneyModalities.Add(participantJourneyModality);
                                    }
                                }
                            }

                            unitOfWork.Complete();
                            scope.Complete();

                            result = "success";
                        }
                    }
                }
            }

            return result;
        }

        public ParticipantJourneyModality RetrieveParticipantJourneyModality(ParticipantJourneySearchViewModel psm, int formID, int modalityID, out string message)
        {
            message = string.Empty;

            ParticipantJourneyModality result = null;

            if (psm == null)
            {
                message = "Parameter cannot be null";
            }

            else if (string.IsNullOrEmpty(psm.Nric) || psm.PHSEventId == 0)
            {
                message = "Nric or PHSEventId cannot be null";
            }

            //todo: comment out for data migration. to put it back before going live
            //else if (!NricChecker.IsNRICValid(psm.Nric))
            //{
            //    message = "Invalid Nric";
            //}

            else
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    result = unitOfWork.ParticipantJourneyModalities.GetParticipantJourneyModality(psm.Nric, psm.PHSEventId, formID, modalityID);
                }
            }

            return result;
        }

        public TemplateViewModel FindTemplateToDisplay(ParticipantJourneySearchViewModel psm, int formID, int selectedModalityId, bool embed, out string message)
        {
            TemplateViewModel model = null;
            using (var unitOfWork = CreateUnitOfWork())
            {
                ParticipantJourneyModality participantJourneyModality = RetrieveParticipantJourneyModality(psm, formID, selectedModalityId, out message);

                if (participantJourneyModality != null)
                {
                    var template = participantJourneyModality.TemplateID.HasValue ? unitOfWork.FormRepository.GetTemplate(participantJourneyModality.TemplateID.Value) : FindLatestTemplate(formID, unitOfWork);

                    if (template != null)
                    {
                        model = TemplateViewModel.CreateFromObject(template, Constants.TemplateFieldMode.INPUT);
                        model.Embed = embed;

                        bool valueRequiredForRegistration = false;

                        if (Internal_Form_Type_Registration.Equals(model.InternalFormType))
                        {
                            if (participantJourneyModality.EntryId == Guid.Empty)
                            {
                                valueRequiredForRegistration = true;
                            }
                        }

                        foreach (var field in model.Fields)
                        {
                            field.ParticipantNric = psm.Nric;
                            field.IsValueRequiredForRegistration = valueRequiredForRegistration;
                            field.EntryId = participantJourneyModality.EntryId.ToString();
                        }
                    }
                }

                else
                {
                    throw new Exception("No participantJourneyModality found");
                }

                return model;
                
            }
        }

        public Participant FindParticipant(string nric)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                return unitOfWork.Participants.FindParticipant(nric);
            }
        }

        public Template FindTemplate(int templateID)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                return FindTemplate(templateID, unitOfWork);
            }
        }

        private Template FindTemplate(int templateID, IUnitOfWork unitOfWork)
        {
            return unitOfWork.FormRepository.GetTemplate(templateID);
        }

        private Template FindLatestTemplate(int formId, IUnitOfWork unitOfWork)
        {
            Form form = unitOfWork.FormRepository.GetForm(formId);
            if (form != null)
            {
                return form.Templates.Where(t => t.IsActive == true).OrderByDescending(f => f.Version).First();
            }
            return null;
        }


        public string InternalFillIn(ParticipantJourneySearchViewModel psm, int modalityId, IDictionary<string, string> SubmitFields, TemplateViewModel model, FormCollection formCollection)
        {
            using (var unitOfWork = CreateUnitOfWork())
            {
                var template = FindTemplate(model.TemplateID.Value, unitOfWork);

                using (var fillIn = new InternalFormFillIn(unitOfWork, psm, template.FormID, modalityId))
                {
                    return fillIn.FillIn(SubmitFields, template, formCollection);
                }
            }
        }

        private void copyPreRegistrationToParticipant(Participant participant, PreRegistration preRegistration)
        {
            if (preRegistration != null)
            {
                Util.CopyNonNullProperty(preRegistration, participant);
            }
            
        }


        public string UpdateParticipantJourneyModalityFromMSS(ICollection<ParticipantJourneyModalityCircleViewModel> newPtJourneyModalityItems)
        {
            using(var unitOfWork = CreateUnitOfWork())
            {
                Participant participant = unitOfWork.Participants.FindParticipant(newPtJourneyModalityItems.First().Nric);
                PHSEvent phsEvent = unitOfWork.Events.GetEvent(int.Parse(newPtJourneyModalityItems.First().EventId));

                IEnumerable<ParticipantJourneyModality> oldPJMItems = unitOfWork.ParticipantJourneyModalities.GetParticipantJourneyModalityByParticipantEvent(newPtJourneyModalityItems.First().Nric, int.Parse(newPtJourneyModalityItems.First().EventId)); 

                foreach (ParticipantJourneyModalityCircleViewModel newModalities in newPtJourneyModalityItems)
                {
                    Boolean oldModalityExists = false;
                    int oldPJMId = 0; 

                    foreach (ParticipantJourneyModality oldModalities in oldPJMItems)
                    {
                        if(newModalities.ModalityID == oldModalities.ModalityID)
                        {
                            oldModalityExists = true;
                            oldPJMId = oldModalities.ParticipantJourneyModalityID;
                        }
                    }

                    // to create new participantjourneymodality
                    if (newModalities.IsActive && !oldModalityExists)
                    {
                        Modality modality = unitOfWork.Modalities.Get(newModalities.ModalityID);

                        foreach(Form form in modality.Forms)
                        {
                            ParticipantJourneyModality newPJM = new ParticipantJourneyModality();
                            newPJM.Participant = participant;
                            newPJM.PHSEvent = phsEvent;
                            newPJM.Modality = unitOfWork.Modalities.Get(newModalities.ModalityID);
                            newPJM.Form = form; 
                            unitOfWork.ParticipantJourneyModalities.Add(newPJM);
                        }                        

                        using (TransactionScope scope = new TransactionScope())
                        {
                            unitOfWork.Complete();
                            scope.Complete();
                        }
                    }

                    // to delete old participantjourneymodality
                    // todo: before delete, to check if there is exsiting form submission 
                    if (!newModalities.IsActive && oldModalityExists)
                    {
                        Modality modality = unitOfWork.Modalities.Get(newModalities.ModalityID);

                        foreach (Form form in modality.Forms)
                        {
                            ParticipantJourneyModality toRemovePJM = unitOfWork.ParticipantJourneyModalities.GetParticipantJourneyModality(participant.Nric, phsEvent.PHSEventID, form.FormID, modality.ModalityID);
                            //toRemovePJM.Modality = null;
                            //toRemovePJM.Form = null;
                            //toRemovePJM.PHSEvent = null;
                            //toRemovePJM.Participant = null;

                            //using (TransactionScope scope = new TransactionScope())
                            //{
                            //    unitOfWork.Complete();
                            //    scope.Complete();
                            //}

                            unitOfWork.ParticipantJourneyModalities.Remove(toRemovePJM);

                            using (TransactionScope scope = new TransactionScope())
                            {
                                unitOfWork.Complete();
                                scope.Complete();
                            }
                        }                    
                        
                    }                
                    
                }
              

                return "Updated Successfully"; 
            }
        }
    }
}
