using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.ViewModel.PatientJourney
{
    public class PatientEventModalityViewModel
    {

        public PatientEventModalityViewModel(PatientEventViewModel patientEvent, Modality modality)
        {
            this.EventId = patientEvent.EventId;
            this.Nric = patientEvent.Nric;

            this.ModalityID = modality.ID;
            this.Name = modality.Name;
            this.Position = modality.Position;
            this.IconPath = modality.IconPath;
            this.IsActive = modality.IsActive;
            this.IsVisible = modality.IsVisible;
            this.HasParent = modality.HasParent;
            this.Status = modality.Status;
            this.Eligiblity = modality.Eligiblity;
            this.Labels = modality.Labels;
        }

        public string EventId { get; }
        public string Nric { get; }

        public int ModalityID { get; }
        public string Name { get; }
        public int Position { get; }
        public string IconPath { get; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public Nullable<bool> HasParent { get; }
        public string Status { get; set; }
        public string Eligiblity { get; set; }
        public Nullable<int> Labels { get; set; }


    }
}
