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

            this.ModalityID = modality.ModalityID;
            this.Name = modality.Name;
            this.Position = modality.Position;
            this.IconPath = modality.IconPath;
            this.IsActive = modality.IsActive;
            this.IsVisible = modality.IsVisible;
            this.HasParent = modality.HasParent;
            this.Eligiblity = modality.Eligiblity;
            this.Labels = modality.Labels;

            this.modalityForms = new List<int>();

            foreach(var form in modality.Forms)
            {
                this.modalityForms.Add(form.ID);
            }

            this.modalityCompletedForms = new List<int>();
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
        public string Eligiblity { get; set; }
        public Nullable<int> Labels { get; set; }

        public List<int> modalityForms { get; set; }
        public List<int> modalityCompletedForms { get; set; }

        public string GetStatus()
        {
            if (modalityForms.Count == modalityCompletedForms.Count)
            {
                return "Completed";
            }

            else
            {
                return "Pending";
            }
        }

        public bool isAllFormsCompleted()
        {
            return (modalityForms.Count == modalityCompletedForms.Count);
        }

        public bool isModalityFormsContain(int formId)
        {
            return modalityForms.Contains(formId);
        }
    }
}
