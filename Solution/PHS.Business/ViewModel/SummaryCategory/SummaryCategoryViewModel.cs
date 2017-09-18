using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PHS.DB;
using PHS.Business.Helpers;

namespace PHS.Business.ViewModel.SummaryCategory
{
    public class SummaryCategoryViewModel
    {
        public SummaryCategoryViewModel(String summaryCategory)
        {
            SummaryCategoryName = summaryCategory;
            EventSummaries = new List<Summary>();
            DoctorSummaries = new List<Summary>();
            PTSummaries = new List<Summary>();
            OTSummaries = new List<Summary>();
            ExhibitionSummaries = new List<Summary>();
            SocialSupportSummaries = new List<Summary>(); 
            Cog2Summaries = new List<Summary>();
        }

        public void AddEventSummary(Summary summary)
        {
            if(SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(SummaryHelper.EventSummaryLabelMap, SummaryCategoryName, summary.Label))
            {
                EventSummaries.Add(summary);
            }
        }

        public void AddDoctorSummary(Summary summary)
        {
            if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(SummaryHelper.DoctorSummaryLabelMap, SummaryCategoryName, summary.Label))
            {
                DoctorSummaries.Add(summary);
            }
        }

        public void AddPTSummary (Summary summary)
        {
            if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(SummaryHelper.PTConsultSummaryLabelMap, SummaryCategoryName, summary.Label))
            {
                PTSummaries.Add(summary);
            }
        }

        public void AddOTSummary(Summary summary)
        {
            if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(SummaryHelper.OTConsultSummaryLabelMap, SummaryCategoryName, summary.Label))
            {
                OTSummaries.Add(summary);
            }
            if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(SummaryHelper.PTConsultSummaryLabelMap, SummaryCategoryName, summary.Label))
            {
                PTSummaries.Add(summary);
            }
        }


        public void AddCog2Summary(Summary summary)
        {
            if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(SummaryHelper.Cog2SummaryLabelMap, SummaryCategoryName, summary.Label))
            {
                Cog2Summaries.Add(summary);
            }
        }


        public void AddExhibitionSummary(Summary summary)
        {
            if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(SummaryHelper.ExhibitionSummaryLabelMap, SummaryCategoryName, summary.Label))
            {
                ExhibitionSummaries.Add(summary);
            }
        }

        public void AddSocialSuppSummary(Summary summary)
        {
            if (SummaryHelper.IsFieldNameAndCategoryFoundInSummaryMap(SummaryHelper.SocialSupportSummaryLabelMap, SummaryCategoryName, summary.Label))
            {
                SocialSupportSummaries.Add(summary);
            }
        }

        public String SummaryCategoryName { get; }

        public List<Summary> EventSummaries { get; }

        public List<Summary> DoctorSummaries { get; }

        public List<Summary> PTSummaries { get; }

        public List<Summary> OTSummaries { get; }

        public List<Summary> ExhibitionSummaries { get; }

        public List<Summary> SocialSupportSummaries { get; }

        public List<Summary> Cog2Summaries { get; }
    }

}
