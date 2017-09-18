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
using PHS.Common;

namespace PHS.Business.Implementation.FillIn
{
    class InternalFormFillIn : BaseFormFillIn
    {
        private readonly ParticipantJourneySearchViewModel psm;
        private int formId;
        private int modalityId;

        public InternalFormFillIn(IUnitOfWork unitOfWork, ParticipantJourneySearchViewModel psm, int formId, int modalityId) : base(unitOfWork)
        {
            this.psm = psm;
            this.formId = formId;
            this.modalityId = modalityId;
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
                    Guid existingEntryId = participantJourneyModality.EntryId;
                    TemplateFieldValue fieldValue = UnitOfWork.FormRepository.GetTemplateFieldValue(field, existingEntryId);
                    if (fieldValue != null)
                    {
                        UnitOfWork.FormRepository.UpdateTemplateFieldValue(fieldValue, field, value);
                    }
                    
                    else
                    {
                        UnitOfWork.FormRepository.InsertTemplateFieldValue(field, value, existingEntryId);
                    }
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

            if (Internal_Form_Type_Registration.Equals(template.Form.InternalFormType))
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
                    participant.Citizenship = getStringValue(values, Registration_Field_Name_Citizenship);
                    participant.Race = getStringValue(values, Registration_Field_Name_Race);
                    participant.Salutation = getStringValue(values, Registration_Field_Name_Salutation);

                    string addressValue = getStringValue(values, Registration_Field_Name_Address);

                    if (!string.IsNullOrEmpty(addressValue))
                    {
                        AddressViewModel address = addressValue.FromJson<AddressViewModel>();

                        participant.Address = address.ConvertToOneLineAddress();
                        participant.PostalCode = address.ZipCode;
                    }

                    else
                    {
                        participant.Address = "";
                        participant.PostalCode = "";
                    }
                }
            }

            ParticipantJourneyModality participantJourneyModality = FindParticipantJourneyModality();
            if (participantJourneyModality != null)
            {
                if (participantJourneyModality.EntryId == Guid.Empty)
                {
                    UnitOfWork.ParticipantJourneyModalities.UpdateParticipantJourneyModality(participantJourneyModality, templateView.TemplateID.Value, entryId);
                }
            }
            else
            {
                throw new Exception("No participantJourneyModality found");
            }

            foreach (var field in templateView.Fields)
            {
                if (field.SummaryFieldName != null && field.SummaryFieldName.Length >0
                        && field.SummaryType != null && field.SummaryType.Length > 0)
                {
                    Summary summary = null;
                    summary = UnitOfWork.Summaries.FindSummary(participantJourneyModality.PHSEventID,
                        participantJourneyModality.ParticipantID,
                        participantJourneyModality.ModalityID, (int)field.TemplateFieldID);
                    if (summary == null)
                    {
                        summary = new Summary();
                        summary.Label = field.SummaryFieldName;
                        summary.ModalityID = participantJourneyModality.ModalityID;
                        summary.ParticipantID = participantJourneyModality.ParticipantID;
                        summary.PHSEventID = participantJourneyModality.PHSEventID;
                        summary.SummaryType = field.SummaryType;
                        summary.SummaryLabel = field.Label;
                        summary.SummaryValue = field.InputValue;
                        summary.TemplateFieldID = (int)field.TemplateFieldID;
                        summary.StandardReferenceID = field.StandardReferenceID;

                        UnitOfWork.Summaries.Add(summary);
                    }
                    else
                    {
                        summary.Label = field.SummaryFieldName;
                        summary.SummaryType = field.SummaryType;
                        summary.SummaryLabel = field.Label;
                        summary.SummaryValue = field.InputValue;
                        summary.StandardReferenceID = field.StandardReferenceID;
                    }
                    
                }
            }
        }

        private String GetLabelTextBySummaryTypeAndSummaryLabel(String sType, String sLabel)
        {
            String labelText = null;

            Dictionary<string, List<string>> eventSummaryLabelMap = new Dictionary<string, List<string>> {
                { Constants.Summary_Category_Label_Name_CardiovascularHealth, new List<string> {
                    Constants.Summary_Field_Name_CurrentlySmoke,
                    Constants.Summary_Field_Name_FamilyHistory,
                    Constants.Summary_Field_Name_PastMedicalHistory
                }},
                { Constants.Summary_Category_Label_Name_Obesity, new List<string> {
                    Constants.Summary_Field_Name_PastMedicalHistory,
                    Constants.Summary_Field_Name_FamilyHistory
                }},
                { Constants.Summary_Category_Label_Name_LifestyleChoices, new List<string> {
                    Constants.Summary_Field_Name_CurrentlySmoke
                }},
                { Constants.Summary_Category_Label_Name_Cancers, new List<string> {
                    Constants.Summary_Field_Name_PersonalCancerHistory,
                    Constants.Summary_Field_Name_FamilyHistory
                }}
            };

            if(sType.Equals(Constants.Summary_Type_Event) || sType.Equals(Constants.Summary_Type_All))
            {
                //labelText = 

            }



            return labelText;
        }

        private ParticipantJourneyModality FindParticipantJourneyModality()
        {
            return UnitOfWork.ParticipantJourneyModalities.GetParticipantJourneyModality(psm.Nric, psm.PHSEventId, formId, modalityId);
        }
    }
}
