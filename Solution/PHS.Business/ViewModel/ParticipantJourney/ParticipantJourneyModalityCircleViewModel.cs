using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.ViewModel.ParticipantJourney
{
    public class ParticipantJourneyModalityCircleViewModel
    {
        public ParticipantJourneyModalityCircleViewModel(ParticipantJourneyViewModel participantJourneyViewModel, Modality modality)
        {
            EventId = participantJourneyViewModel.EventId;
            Nric = participantJourneyViewModel.Nric;

            ModalityID = modality.ModalityID;
            Name = modality.Name;
            Position = modality.Position;
            IconPath = modality.IconPath;
            IsActive = modality.IsActive;
            IsVisible = modality.IsVisible;
            HasParent = modality.HasParent;
            Eligiblity = modality.Eligiblity;
            Labels = modality.Labels;

            modalityForms = new List<int>();

            foreach (var form in modality.Forms)
            {
                modalityForms.Add(form.FormID);
            }

            modalityCompletedForms = new List<int>();
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
