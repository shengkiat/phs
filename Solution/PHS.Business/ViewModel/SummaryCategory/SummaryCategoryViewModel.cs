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
            Summaries = new List<Summary>();
        }

        public void AddSummary(Summary summary)
        {
            Summaries.Add(summary);
        }

        public String SummaryCategoryName { get; }

        public List<Summary> Summaries { get; }
    }

}
