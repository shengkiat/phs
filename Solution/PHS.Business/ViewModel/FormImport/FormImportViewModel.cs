using PHS.DB.ViewModels.Form;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHS.Business.ViewModel.FormImport
{
    public class FormImportViewModel
    {
        public int? EventID { get; set; }
        public int? FormID { get; set; }
        public List<FormViewModel> Forms { get; set; }

        public static FormImportViewModel Initialize(int eventId)
        {
            var view = new FormImportViewModel
            {
                EventID = eventId,
                Forms = Enumerable.Empty<FormViewModel>().ToList()
            };

            return view;
        }

        public static FormImportViewModel CreateFromObject(int eventId)
        {
            return FormImportViewModel.Initialize(eventId);
        }
    }
}
