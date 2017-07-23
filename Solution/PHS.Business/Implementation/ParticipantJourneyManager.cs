﻿using PHS.Business.Common;
using PHS.Business.Implementation.FillIn;
using PHS.Business.Interface;
using PHS.Business.ViewModel;
using PHS.Business.ViewModel.ParticipantJourney;
using PHS.Common;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.Mvc;
using static PHS.Common.Constants;

namespace PHS.Business.Implementation
{
    public class ParticipantJourneyManager : BaseFormManager, IParticipantJourneyManager, IManagerFactoryBase<IParticipantJourneyManager>
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

            else if (!NricChecker.IsNRICValid(psm.Nric))
            {
                message = "Invalid Nric";
            }

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

                    PHSEvent phsEvent = unitOfWork.Events.GetAllActiveEvents().Where(e => e.PHSEventID == psm.PHSEventId).FirstOrDefault();

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
                                foreach(var form in modality.Forms)
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

                            unitOfWork.Complete();
                            scope.Complete();

                            result = "success";
                        }
                    }
                }
            }

            return result;
        }

        public ParticipantJourneyModality RetrieveParticipantJourneyModality(ParticipantJourneySearchViewModel psm, int formID, out string message)
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

            else if (!NricChecker.IsNRICValid(psm.Nric))
            {
                message = "Invalid Nric";
            }

            else
            {
                using (var unitOfWork = CreateUnitOfWork())
                {
                    result = unitOfWork.ParticipantJourneyModalities.GetParticipantJourneyModality(psm.Nric, psm.PHSEventId, formID);
                }
            }

            return result;
        }

        public string InternalFillIn(ParticipantJourneySearchViewModel psm, IDictionary<string, string> SubmitFields, TemplateViewModel model, FormCollection formCollection)
        {
            var template = FindTemplate(model.TemplateID.Value);

            using (var unitOfWork = CreateUnitOfWork())
            {
                using (var fillIn = new InternalFormFillIn(unitOfWork, psm, model.FormID))
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
    }
}
