using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.DB.ViewModels
{
    public class ModalityForm
    {
        public String ModalityName { get; set; }

        public int ModalityID { get; set; }
        public String FormName { get; set; }

        public int FormID { get; set; }

        public Boolean IsSelected { get; set; }

        public String publicURL { get; set; }
    }
}
