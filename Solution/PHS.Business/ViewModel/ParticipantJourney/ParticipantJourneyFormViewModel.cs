using PHS.Business.Helpers;
using PHS.Business.ViewModel.SummaryCategory;
using PHS.Common;
using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.ViewModel.ParticipantJourney
{
    public class ParticipantJourneyFormViewModel
    {

        public ParticipantJourneyFormViewModel(Participant participant, int PHSEventId)
        {
            Participant = participant;
            Event = participant.PHSEvents.Where(e => e.PHSEventID == PHSEventId).FirstOrDefault();
        }

        private PHSEvent Event { get; }

        private Participant Participant { get; }

        public int SelectedModalityId { get; set; }

        public List<Form> GetModalityFormsForTabs()
        {
            List<Form> result = new List<Form>();

            foreach (var modality in Event.Modalities)
            {
                if (modality.ModalityID.Equals(SelectedModalityId))
                {
                    result = modality.Forms.ToList();
                }
            }

            return result;
        }

        public bool isSummarySelected()
        {
            bool result = false;
            foreach (var modality in Event.Modalities)
            {
                if (modality.ModalityID.Equals(SelectedModalityId))
                {
                    result = modality.Name.Equals("Summary");
                }
            }
            return result;
        }

        public List<SummaryCategoryViewModel> GetEventSummaryCategories()
        {
            List<SummaryCategoryViewModel> result = new List<SummaryCategoryViewModel>();

            foreach (var summaryCategoryName in SummaryHelper.GetEventSummaryCategoryNameList())
            {
                SummaryCategoryViewModel sumCategoryViewMode = new SummaryCategoryViewModel(summaryCategoryName);
                result.Add(sumCategoryViewMode);
            }

            foreach (var sumCategoryViewModel in result)
            {
                foreach (var summary in Participant.Summaries)
                {
                    if (summary != null && summary.PHSEventID.Equals(Event.PHSEventID)
                        && (summary.SummaryType.Equals(Constants.Summary_Type_Event) || summary.SummaryType.Equals(Constants.Summary_Type_All)))
                    {
                        if(SummaryHelper.IsFieldNameAndCategoryFoundInEventSummaryMap(sumCategoryViewModel.SummaryCategoryName, summary.Label))
                        {
                            sumCategoryViewModel.AddEventSummary(summary);
                        }    
                    }
                }
            }

            return result;
        }

        public List<SummaryCategoryViewModel> GetDoctorSummaryCategories()
        {
            List<SummaryCategoryViewModel> result = new List<SummaryCategoryViewModel>();

            foreach (var summaryCategoryName in SummaryHelper.GetDoctorSummaryCategoryNameList())
            {
                SummaryCategoryViewModel sumCategoryViewMode = new SummaryCategoryViewModel(summaryCategoryName);
                result.Add(sumCategoryViewMode);
            }

            foreach (var sumCategoryViewModel in result)
            {
                foreach (var summary in Participant.Summaries)
                {
                    if (summary != null && summary.PHSEventID.Equals(Event.PHSEventID)
                        && (summary.SummaryType.Equals(Constants.Summary_Type_Doctor) || summary.SummaryType.Equals(Constants.Summary_Type_All)))
                    {
                        if (SummaryHelper.IsFieldNameAndCategoryFoundInDoctorSummaryMap(sumCategoryViewModel.SummaryCategoryName, summary.Label))
                        {
                            sumCategoryViewModel.AddDoctorSummary(summary);
                        }
                    }
                }
            }

            return result;
        }
    }
}
