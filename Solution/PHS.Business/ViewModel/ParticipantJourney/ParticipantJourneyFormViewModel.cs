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
                    if (summary != null && summary.PHSEventID.Equals(Event.PHSEventID))
                    {
                        if(SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(SummaryHelper.EventSummaryLabelMap, sumCategoryViewModel.SummaryCategoryName, summary.Label))
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
                    if (summary != null && summary.PHSEventID.Equals(Event.PHSEventID))
                    {
                        if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(SummaryHelper.DoctorSummaryLabelMap, sumCategoryViewModel.SummaryCategoryName, summary.Label))
                        {
                            sumCategoryViewModel.AddDoctorSummary(summary);
                        }
                    }
                }
            }

            return result;
        }


        public List<SummaryCategoryViewModel> GetCog2SummaryCategories()
        {
            List<SummaryCategoryViewModel> result = new List<SummaryCategoryViewModel>();

            foreach (var summaryCategoryName in SummaryHelper.GetCog2SummaryCategoryNameList())
            {
                SummaryCategoryViewModel sumCategoryViewMode = new SummaryCategoryViewModel(summaryCategoryName);
                result.Add(sumCategoryViewMode);
            }

            foreach (var sumCategoryViewModel in result)
            {
                foreach (var summary in Participant.Summaries)
                {
                    if (summary != null && summary.PHSEventID.Equals(Event.PHSEventID))
                    {
                        if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(SummaryHelper.Cog2SummaryLabelMap, sumCategoryViewModel.SummaryCategoryName, summary.Label))
                        {
                            sumCategoryViewModel.AddCog2Summary(summary);
                        }
                    }
                }
            }

            return result;
        }

        public List<SummaryCategoryViewModel> GetPTSummaryCategories()
        {
            List<SummaryCategoryViewModel> result = new List<SummaryCategoryViewModel>();

            foreach (var summaryCategoryName in SummaryHelper.GetPTSummaryCategoryNameList())
            {
                SummaryCategoryViewModel sumCategoryViewMode = new SummaryCategoryViewModel(summaryCategoryName);
                result.Add(sumCategoryViewMode);
            }

            foreach (var sumCategoryViewModel in result)
            {
                foreach (var summary in Participant.Summaries)
                {
                    if (summary != null && summary.PHSEventID.Equals(Event.PHSEventID))
                    {
                        if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(SummaryHelper.PTConsultSummaryLabelMap, sumCategoryViewModel.SummaryCategoryName, summary.Label))
                        {
                            sumCategoryViewModel.AddPTSummary(summary);
                        }
                    }
                }
            }


            return result;
        }

        public List<SummaryCategoryViewModel> GetOTSummaryCategories()
        {
            List<SummaryCategoryViewModel> result = new List<SummaryCategoryViewModel>();

            foreach (var summaryCategoryName in SummaryHelper.GetOTSummaryCategoryNameList())
            {
                SummaryCategoryViewModel sumCategoryViewMode = new SummaryCategoryViewModel(summaryCategoryName);
                result.Add(sumCategoryViewMode);
            }

            foreach (var sumCategoryViewModel in result)
            {
                foreach (var summary in Participant.Summaries)
                {
                    if (summary != null && summary.PHSEventID.Equals(Event.PHSEventID))
                    {
                        if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(SummaryHelper.OTConsultSummaryLabelMap, sumCategoryViewModel.SummaryCategoryName, summary.Label))
                        {
                            sumCategoryViewModel.AddOTSummary(summary);
                        }
                    }
                }
            }
            return result;
        }

        public List<SummaryCategoryViewModel> GetExhibitionSummaryCategories()
        {
            List<SummaryCategoryViewModel> result = new List<SummaryCategoryViewModel>();

            foreach (var summaryCategoryName in SummaryHelper.GetExhibitionSummaryCategoryNameList())
            {
                SummaryCategoryViewModel sumCategoryViewMode = new SummaryCategoryViewModel(summaryCategoryName);
                result.Add(sumCategoryViewMode);
            }

            foreach (var sumCategoryViewModel in result)
            {
                foreach (var summary in Participant.Summaries)
                {
                    if (summary != null && summary.PHSEventID.Equals(Event.PHSEventID))
                    {
                        if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(SummaryHelper.ExhibitionSummaryLabelMap, sumCategoryViewModel.SummaryCategoryName, summary.Label))
                        {
                            sumCategoryViewModel.AddExhibitionSummary(summary);
                        }
                    }
                }
            }
            return result;
        }

        public List<SummaryCategoryViewModel> GetSocialSuppSummaryCategories()
        {
            List<SummaryCategoryViewModel> result = new List<SummaryCategoryViewModel>();

            foreach (var summaryCategoryName in SummaryHelper.GetSocialSuppSummaryCategoryNameList())
            {
                SummaryCategoryViewModel sumCategoryViewMode = new SummaryCategoryViewModel(summaryCategoryName);
                result.Add(sumCategoryViewMode);
            }

            foreach (var sumCategoryViewModel in result)
            {
                foreach (var summary in Participant.Summaries)
                {
                    if (summary != null && summary.PHSEventID.Equals(Event.PHSEventID))
                    {
                        if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(SummaryHelper.SocialSupportSummaryLabelMap, sumCategoryViewModel.SummaryCategoryName, summary.Label))
                        {
                            sumCategoryViewModel.AddSocialSuppSummary(summary);
                        }
                    }
                }
            }
            return result;
        }
    }
}
