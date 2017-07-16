using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHS.DB.ViewModels;

namespace PHS.Business.ViewModel.Event
{
    public class ModalityFormViewModel
    {
        public int EventID { get; set; }

        public String EventName { get; set; }
        public int ModalityID { get; set; }

        public String ModalityName { get; set; }

        public List<ModalityForm> ModalityFormList { get; set; }
    }
}
