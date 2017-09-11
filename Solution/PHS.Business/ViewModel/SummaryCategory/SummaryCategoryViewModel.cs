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
        }

        public void AddEventSummary(Summary summary)
        {
            if(SummaryHelper.IsFieldNameAndCategoryFoundInEventSummaryMap(SummaryCategoryName, summary.Label))
            {
                EventSummaries.Add(summary);
            }
        }

        public void AddDoctorSummary(Summary summary)
        {
            if (SummaryHelper.IsFieldNameAndCategoryFoundInDoctorSummaryMap(SummaryCategoryName, summary.Label))
            {
                DoctorSummaries.Add(summary);
            }
        }

        public void AddPTSummary (Summary summary)
        {
            if (SummaryHelper.IsFieldNameAndCategoryFoundInDoctorSummaryMap(SummaryCategoryName, summary.Label))
            {
                PTSummaries.Add(summary);
            }
        }

        public String SummaryCategoryName { get; }

        public List<Summary> EventSummaries { get; }

        public List<Summary> DoctorSummaries { get; }

        public List<Summary> PTSummaries { get; }
    }

}
