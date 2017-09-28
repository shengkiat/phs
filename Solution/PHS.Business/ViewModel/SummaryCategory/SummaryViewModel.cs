using PHS.Business.Helpers;
using PHS.DB;
using PHS.FormBuilder.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PHS.Business.ViewModel.SummaryCategory
{
    public class SummaryViewModel
    {

        public SummaryViewModel(Summary summary)
        {
            SummaryItem = summary;
            SummaryLabel = summary.SummaryLabel;
            SummaryValue = summary.SummaryValue;
            Result = "";
            Highlight = false;
        }

        public Summary SummaryItem { get; }

        public string SummaryLabel { get; }

        public string SummaryValue { get; set;  }

        public string Result { get; set; }

        public bool Highlight { get; set; }

    }
}
