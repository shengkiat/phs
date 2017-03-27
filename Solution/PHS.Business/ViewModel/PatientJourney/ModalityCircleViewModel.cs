using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.ViewModel.PatientJourney
{
    public class ModalityCircleViewModel
    {
        public string Name {get; set;} 
        public int Position { get; set; }
        public string IconPath { get; set; }
        public bool Active { get; set; }
        public bool Visible { get; set; }
        public bool HasParent { get; set; }
        public string Status { get; set; }

    }
}
