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
        public string Height;
        public string Weight;
        public string BMIValue;
        public string BMIStandardReferenceResult;
        public string BPValue;
        public string BPStandardReferenceResult;
        public string BloodTestResult;
        public string OverAllResult;
    }
}
