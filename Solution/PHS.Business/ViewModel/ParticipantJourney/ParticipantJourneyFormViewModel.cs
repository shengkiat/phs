using PHS.Business.Helpers;
using PHS.Business.Implementation;
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
            //foreach (var modality in Event.Modalities)
            //{
            //    if (modality.ModalityID.Equals(SelectedModalityId))
            //    {
            //        result = modality.Name.Equals("Summary");
            //    }
            //}
            return result;
        }

        public List<SummaryCategoryViewModel> GetSummaryCategories(string summaryType)
        {
            List<SummaryCategoryViewModel> result = new List<SummaryCategoryViewModel>();

            using (var summaryMappingManager = new SummaryMappingManager())
            {
                List<string> categoryNames = summaryMappingManager.GetAllCategoryNamesBySummaryType(summaryType);
                Dictionary<string, List<string>> summaryLabelMap = summaryMappingManager.GetSummaryLabelMapBySummaryType(summaryType);

                if(categoryNames == null)
                {
                    return result;
                }

                foreach (var summaryCategoryName in categoryNames)
                {
                    SummaryCategoryViewModel sumCategoryViewModel = new SummaryCategoryViewModel(summaryCategoryName);

                    foreach (var summary in Participant.Summaries)
                    {
                        if (summary != null && summary.PHSEventID.Equals(Event.PHSEventID))
                        {
                            if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(summaryLabelMap, summaryCategoryName, summary.Label))
                            {
                                sumCategoryViewModel.AddSummary(summary);
                            }
                        }
                    }

                    result.Add(sumCategoryViewModel);
                }
            }

            return result;
        }

    }
}
