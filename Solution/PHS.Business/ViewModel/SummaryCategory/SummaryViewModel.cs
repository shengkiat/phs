using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.ViewModel.SummaryCategory
{
    class SummaryViewModel
    {
        public SummaryViewModel(Summary summary)
        {
            Summary SummaryItem = summary;
        }

        public string Result { get; set; }

        public Summary SummaryItem { get; }
    }
}
