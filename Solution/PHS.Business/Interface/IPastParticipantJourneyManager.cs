﻿using PHS.Business.ViewModel.PastParticipantJourney;
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
        IList<PatientEventViewModel> GetPatientEventsByNric(string nric, out string message);
        PatientEventViewModel GetPatientEvent(string nric, string eventId, out string message);
    }
}