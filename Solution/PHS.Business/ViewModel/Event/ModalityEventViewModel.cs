using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.ViewModel.Event
{
    public class ModalityEventViewModel
    {
        public string Name { get; set; }
        public int Position { get; set; }
        public string IconPath { get; set; }
        public bool IsActive { get; set; }
        public bool IsVisible { get; set; }
        public bool IsMandatory { get; set; }
        public bool HasParent { get; set; }
        public string Status { get; set; }
        public int EventID { get; set; }
    }
}
