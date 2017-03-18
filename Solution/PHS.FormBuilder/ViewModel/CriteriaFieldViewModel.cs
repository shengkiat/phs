using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PHS.FormBuilder.ViewModels
{
    public class CriteriaFieldViewModel
    {
        public string FieldLabel { get; set; }
        public string CriteriaLogic { get; set; }
        public string CriteriaValue { get; set; }

        public IEnumerable<SelectListItem> FieldLabels { get; set; } // dropdown
        public List<FormFieldViewModel> Fields { get; set; }
        public IEnumerable<IGrouping<string, FormFieldValueViewModel>> GroupedEntries { get; set; }
    }
}
