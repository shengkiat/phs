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
        public @event Event { get; set; }
        public string EventId { get { return Event.ID.ToString(); } }

        public string Nric { get; set; }
        public string FullName { get; set; }
        public int Age { get { return 40; } }
        public string Language { get { return "English"; } }
        public string ContactNumber { get { return "12345678"; } }
        public string Gender { get { return "Male"; } }

        //public List<ModalityCircleViewModel> ModalityCircles { get; set; }

        public int SelectedModalityId { get; set; }

        public List<ModalityForm> GetModalityFormsForTabs() {
            List<ModalityForm> result = new List<ModalityForm>();

            foreach (var modality in Event.Modalities)
            {
                if (modality.ID.Equals(SelectedModalityId))
                {
                    result = modality.ModalityForms.ToList();
                }
            }
            
            return result;
        }

        public bool isMegaSortingModalitySelected()
        {
            bool result = false;
            foreach (var modality in Event.Modalities)
            {
                if (modality.ID.Equals(SelectedModalityId))
                {
                    result = modality.Name.Equals("Mega Sorting Station");
                }
            }
            return result;
        }
    }
}
