using PHS.DB;
using PHS.DB.ViewModels.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.ViewModel.FormExport
{
    public class FormExportViewModel
    {
        public int? FormID { get; set; }
        public string Title { get; set; }
        public List<FormViewModel> Forms { get; set; }
        public List<SortFieldViewModel> SortFields { get; set; }
        public List<CriteriaFieldViewModel> CriteriaFields { get; set; }


        public static FormExportViewModel Initialize()
        {
            var view = new FormExportViewModel
            {
                SortFields = Enumerable.Empty<SortFieldViewModel>().ToList(),
                CriteriaFields = Enumerable.Empty<CriteriaFieldViewModel>().ToList(),
                Forms = Enumerable.Empty<FormViewModel>().ToList()
            };

            return view;
        }

        public static FormExportViewModel CreateFromObject()
        {
            return FormExportViewModel.Initialize();
        }
    }
}
