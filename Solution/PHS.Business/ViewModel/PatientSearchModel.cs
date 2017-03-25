using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.ViewModel
{
    public class PatientSearchModel
    {
        public string SearchBy { get; set; }
        public string Content { get; set; }
        //public IList<Person> persons { get; set; }
        public string SearchButton { get; set; }
    }
}
