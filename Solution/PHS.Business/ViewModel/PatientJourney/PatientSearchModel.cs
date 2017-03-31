﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.DB;

namespace PHS.Business.ViewModel.PatientJourney
{
    public class PatientSearchModel
    {
        public string IcFirstDigit { get; set; }
        public string IcNumber { get; set; }
        public string IcLastDigit { get; set; }
        public IList<PatientEventViewModel> PatientEvents { get; set; }
    }
}