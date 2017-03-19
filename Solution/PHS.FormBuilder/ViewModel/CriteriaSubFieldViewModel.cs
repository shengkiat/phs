using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.FormBuilder.ViewModels
{
    public class CriteriaSubFieldViewModel
    {
        public string OperatorLogic { get; set; }
        public string CriteriaLogic { get; set; }
        public Dictionary<string, string> CriteriaValue { get; set; }

        public List<FormFieldViewModel> Fields { get; set; }
        public IEnumerable<IGrouping<string, FormFieldValueViewModel>> GroupedEntries { get; set; }
    }
}
