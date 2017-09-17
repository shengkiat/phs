using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PHS.Business.ViewModel.FormExport
{
    public class SortFieldViewModel
    {
        public string FieldLabel { get; set; }
        public string SortOrder { get; set; }

        public List<SelectListItem> SortFields { get; set; } // dropdown
    }
}
