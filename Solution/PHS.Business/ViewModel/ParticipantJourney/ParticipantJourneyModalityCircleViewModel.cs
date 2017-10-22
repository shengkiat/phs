using PHS.Business.Extensions;
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
        public ParticipantJourneyModalityCircleViewModel(ParticipantJourneyViewModel participantJourneyViewModel, Modality modality, IList<ParticipantJourneyModality> ParticipantJourneyModalitites, string userRole)
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
            Role = modality.Role;

            UserRole = userRole;

            modalityForms = new HashSet<int>();

            foreach (var form in modality.Forms)
            {
                modalityForms.Add(form.FormID);
            }

            modalityCompletedForms = new HashSet<int>();
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
        public string Role { get; }

        private string UserRole { get; }

        public HashSet<int> modalityForms { get; }
        public HashSet<int> modalityCompletedForms { get; }

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

        public bool isModalityAllowToClick()
        {
            bool result = true;
            if (!string.IsNullOrEmpty(Role))
            {
                result = false;
                foreach (var role in Role.Split(Constants.Form_Option_Split_Concate))
                {
                    if (role.Equals(UserRole))
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
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
