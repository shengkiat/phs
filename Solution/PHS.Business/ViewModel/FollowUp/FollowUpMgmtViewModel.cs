using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.DB;

namespace PHS.Business.ViewModel.FollowUp
{
    public class FollowUpMgmtViewModel
    {
        public Participant Participant { get; set; }
        public Tuple<string, string> BMIvalueStdRefPair { get; set; }
        public Tuple<string, string> BPValueStdRefPair { get; set; }

        public string BloodTestResult;
        public string OverAllResult;
    }
}
