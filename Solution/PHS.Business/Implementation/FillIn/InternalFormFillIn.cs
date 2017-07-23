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
                if (participantJourneyModality.EntryId == null)
                {
                    UnitOfWork.FormRepository.InsertTemplateFieldValue(field, value, entryId);
                }

                else
                {
                    //update template field value
                }
            }
            
        }

        protected override void HandleAdditionalInsert(TemplateViewModel templateView, Template template, FormCollection formCollection, Guid entryId)
        {
            ParticipantJourneyModality participantJourneyModality = FindParticipantJourneyModality();
            if (participantJourneyModality != null)
            {
                if (participantJourneyModality.EntryId == null)
                {
                    participantJourneyModality.EntryId = entryId;
                }
            }
        }

        private ParticipantJourneyModality FindParticipantJourneyModality()
        {
            return UnitOfWork.ParticipantJourneyModalities.GetParticipantJourneyModality(psm.Nric, psm.PHSEventId, formId);
        }
    }
}
