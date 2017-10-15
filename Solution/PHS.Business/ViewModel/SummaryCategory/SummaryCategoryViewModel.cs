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
            Summaries = new List<SummaryViewModel>();
            Highlight = false;
        }

        public void AddSummary(SummaryViewModel summary)
        {
            Summaries.Add(summary);
        }

        public String SummaryCategoryName { get; }

        public List<SummaryViewModel> Summaries { get; }

        public bool Highlight { get; set; }
    }

}
