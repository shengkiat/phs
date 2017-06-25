using PHS.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.ViewModel.PatientJourney
{
    public class PatientEventViewModel
    {

        public PatientEventViewModel()
        {
        }

        public PatientEventViewModel(EventPatient eventPatient)
        {
            this.Event = eventPatient.PHSEvent;
            this.FullName = eventPatient.FullName;
            this.Nric = eventPatient.Nric;
            this.Language = eventPatient.Language;
            this.ContactNumber = eventPatient.ContactNumber;
            this.Gender = eventPatient.Gender;

            int now = int.Parse(eventPatient.PHSEvent.StartDT.ToString("yyyy"));
            int dob = int.Parse(eventPatient.DateOfBirth.Value.ToString("yyyy"));
            this.Age = (now - dob);
        }


        public PHSEvent Event { get; set; }
        public string EventId { get { return Event.PHSEventID.ToString(); } }

        public string Nric { get; set; }
        public string FullName { get; set; }
        public int Age { get; }
        public string Language { get; }
        public string ContactNumber { get; }
        public string Gender { get; }

        //public List<ModalityCircleViewModel> ModalityCircles { get; set; }

        public int SelectedModalityId { get; set; }

        public List<Template> GetModalityFormsForTabs() {
            List<Template> result = new List<Template>();

            foreach (var modality in Event.Modalities)
            {
                if (modality.ModalityID.Equals(SelectedModalityId))
                {
                    result = modality.Forms.ToList();
                }
            }
            
            return result;
        }

        public bool isSummarySelected()
        {
            bool result = false;
            foreach (var modality in Event.Modalities)
            {
                if (modality.ModalityID.Equals(SelectedModalityId))
                {
                    result = modality.Name.Equals("Summary");
                }
            }
            return result;
        }
    }
}
