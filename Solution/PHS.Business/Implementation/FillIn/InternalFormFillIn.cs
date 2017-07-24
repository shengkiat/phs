using PHS.Repository.Interface.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.DB;
using PHS.DB.ViewModels.Form;
using System.Web.Mvc;
using PHS.Business.ViewModel.ParticipantJourney;

using PHS.Business.Extensions;
using static PHS.Common.Constants;

namespace PHS.Business.Implementation.FillIn
{
    class InternalFormFillIn : BaseFormFillIn
    {
        private readonly ParticipantJourneySearchViewModel psm;
        private int formId;

        public InternalFormFillIn(IUnitOfWork unitOfWork, ParticipantJourneySearchViewModel psm, int formId) : base(unitOfWork)
        {
            this.psm = psm;
            this.formId = formId;
        }

        protected override void HandleTemplateFieldValue(TemplateFieldViewModel field, string value, Guid entryId)
        {
            ParticipantJourneyModality participantJourneyModality = FindParticipantJourneyModality();
            if (participantJourneyModality != null)
            {
                if (participantJourneyModality.EntryId == Guid.Empty)
                {
                    UnitOfWork.FormRepository.InsertTemplateFieldValue(field, value, entryId);
                }

                else
                {
                    //update template field value
                }
            }

            else
            {
                throw new Exception("No participantJourneyModality found");
            }
            
        }

        protected override void HandleAdditionalInsert(TemplateViewModel templateView, Template template, FormCollection formCollection, Guid entryId)
        {
            IDictionary<string, object> values = new Dictionary<string, object>();

            foreach (var field in templateView.Fields)
            {
                var value = field.SubmittedValue(formCollection);

                if (!string.IsNullOrEmpty(field.RegistrationFieldName))
                {
                    values.Add(field.RegistrationFieldName, value);
                }
            }

            if ("REG".Equals(template.Form.InternalFormType))
            {
                //update participant
                Participant participant = UnitOfWork.Participants.FindParticipant(psm.Nric, psm.PHSEventId);
                if (participant != null)
                {
                    participant.FullName = getStringValue(values, Registration_Field_Name_FullName);
                    participant.HomeNumber = getStringValue(values, Registration_Field_Name_HomeNumber);
                    participant.MobileNumber = getStringValue(values, Registration_Field_Name_MobileNumber);
                    participant.DateOfBirth = getDateTimeValue(values, Registration_Field_Name_DateOfBirth);
                    participant.Language = getStringValue(values, Registration_Field_Name_Language);
                    participant.Gender = getStringValue(values, Registration_Field_Name_Gender);

                    //Create ParticipantJourneyModality
                    /*ParticipantJourneyModality newParticipantJourneyModality = new ParticipantJourneyModality()
                    {
                        ParticipantID = participant.ParticipantID,
                        PHSEventID = psm.PHSEventId,
                        FormID = templateView.FormID,
                        ModalityID = modalityId,
                        EntryId = entryId
                    };*/

                    //unitOfWork.ParticipantJourneyModalities.Add(participantJourneyModality);

                    //participant.ParticipantJourneyModalities.Add(newParticipantJourneyModality);
                }
            }

            ParticipantJourneyModality participantJourneyModality = FindParticipantJourneyModality();
            if (participantJourneyModality != null)
            {
                if (participantJourneyModality.EntryId == Guid.Empty)
                {
                    UnitOfWork.ParticipantJourneyModalities.UpdateParticipantJourneyModalityEntryId(participantJourneyModality, entryId);
                }
            }

            else
            {
                throw new Exception("No participantJourneyModality found");
            }
        }

        private ParticipantJourneyModality FindParticipantJourneyModality()
        {
            return UnitOfWork.ParticipantJourneyModalities.GetParticipantJourneyModality(psm.Nric, psm.PHSEventId, formId);
        }
    }
}
