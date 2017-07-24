using PHS.Business.ViewModel.ParticipantJourney;
using PHS.Business.ViewModel.PastParticipantJourney;
using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.Interface
{
    public interface IPastParticipantJourneyManager : IDisposable
    {
        IList<ParticipantJourneyViewModel> GetAllParticipantJourneyByNric(string nric, out string message);
    }
}
