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
        public Dictionary<string, string> CriteriaValue { get; set; }
        public List<CriteriaSubFieldViewModel> CriteriaSubFields { get; set; }

        public IEnumerable<SelectListItem> FieldLabels { get; set; } // dropdown
        public List<FormFieldViewModel> Fields { get; set; }
        public IEnumerable<IGrouping<string, FormFieldValueViewModel>> GroupedEntries { get; set; }

        public string getConvertedCriteriaValue()
        {
            Dictionary<string, string> mappedValues = new Dictionary<string, string>();
            mappedValues.Add("eq", "=");
            mappedValues.Add("neq", "<>");
            mappedValues.Add("gt", ">");
            mappedValues.Add("gte", ">=");
            mappedValues.Add("lt", "<");
            mappedValues.Add("lte", "<=");

            switch (CriteriaLogic)
            {
                case "startswith":
                    return string.Format("LIKE '{0}*'", mappedValues[CriteriaLogic], CriteriaValue[FieldLabel]);
                case "endswith":
                    return string.Format("LIKE '*{0}'", mappedValues[CriteriaLogic], CriteriaValue[FieldLabel]);
                case "contains":
                    return string.Format("LIKE '*{0}*'", mappedValues[CriteriaLogic], CriteriaValue[FieldLabel]);
                case "doesnotcontain":
                    return string.Format("NOT LIKE '*{0}*'", mappedValues[CriteriaLogic], CriteriaValue[FieldLabel]);
                case "in":
                    return string.Format("IN ({0})", mappedValues[CriteriaLogic], CriteriaValue[FieldLabel]);
                default:
                    return string.Format("{0} '{1}'", mappedValues[CriteriaLogic], CriteriaValue[FieldLabel]);
            }
            
        }
    }
}
