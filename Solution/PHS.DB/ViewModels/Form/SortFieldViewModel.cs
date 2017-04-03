using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PHS.FormBuilder.ViewModels
{
    public class SortFieldViewModel
    {
        public string FieldLabel { get; set; }
        public string SortOrder { get; set; }

        public IEnumerable<SelectListItem> SortFields { get; set; } // dropdown
    }
}
