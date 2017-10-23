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
        //public int PHSEventId { get; set; }
        //public IList<PHSEvent> PHSEvents { get; set; }
        public FollowUpGroup FollowUpGroup { get; set; }
        public IList<Participant> Participants { get; set; }
    }
}
