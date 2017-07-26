using PHS.Common;
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
        public ParticipantJourneyModalityCircleViewModel(ParticipantJourneyViewModel participantJourneyViewModel, Modality modality, IList<ParticipantJourneyModality> ParticipantJourneyModalitites)
        {
            EventId = participantJourneyViewModel.EventId;
            Nric = participantJourneyViewModel.Nric;

            ModalityID = modality.ModalityID;
            Name = modality.Name;
            Position = modality.Position;
            IconPath = modality.IconPath;
            IsActive = modality.IsActive;
            IsVisible = modality.IsVisible;
            IsMandatory = modality.IsMandatory;
            Status = modality.Status;
            HasParent = modality.HasParent;
            Eligiblity = modality.Eligiblity;
            Labels = modality.Labels;

            modalityForms = new List<int>();

            foreach (var form in modality.Forms)
            {
                modalityForms.Add(form.FormID);
            }

            modalityCompletedForms = new List<int>();
            foreach(var pjm in ParticipantJourneyModalitites)
            {
                if (Constants.Internal_Form_Type_MegaSortingStation.Equals(pjm.Form.InternalFormType))
                {
                    modalityCompletedForms.Add(pjm.FormID);
                }

                else if (pjm.TemplateID.HasValue || pjm.EntryId != Guid.Empty)
                {
                    modalityCompletedForms.Add(pjm.FormID);
                }
            }
        }

        public string EventId { get; }
        public string Nric { get; }

        public int ModalityID { get; }
        public string Name { get; }
        public int Position { get; }
        public string IconPath { get; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }

        public string Status { get; set; }

        public bool IsMandatory { get; set; }
        public Nullable<bool> HasParent { get; }
        public string Eligiblity { get; }
        public Nullable<int> Labels { get; }

        public List<int> modalityForms { get; }
        public List<int> modalityCompletedForms { get; }

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
