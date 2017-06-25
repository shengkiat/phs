using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.DB.ViewModels.Forms
{
    public class CriteriaSubFieldViewModel
    {
        public string OperatorLogic { get; set; }
        public string CriteriaLogic { get; set; }
        public Dictionary<string, string> CriteriaValue { get; set; }

        public List<TemplateFieldViewModel> Fields { get; set; }
        public IEnumerable<IGrouping<string, TemplateFieldValueViewModel>> GroupedEntries { get; set; }
    }
}
