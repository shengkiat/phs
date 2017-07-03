using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PHS.Common;
using PHS.DB;


namespace PHS.DB.ViewModels.Forms
{
    public class FormViewModel
    {
        #region Properties

        public int? FormID { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public bool IsPublic { get; set; }
        public string PublicFormType { get; set; }
        public DateTime DateAdded { get; set; }

        #endregion

        #region Public Members

        public static FormViewModel Initialize()
        {

            var formView = new FormViewModel();

            return formView;
        }

        public static FormViewModel CreateFromObject(Form form1)
        {
            return CreateFromObject(form1, Constants.TemplateMode.EDIT);
        }

        public static FormViewModel CreateFromObject(Form form1, Constants.TemplateMode mode)
        {
            if (form1 != null)
            {

                var formView = CreateBasicFromObject(form1);
                return formView;
            }
            return FormViewModel.Initialize();
        }

        public static FormViewModel CreateBasicFromObject(Form form1)
        {

            var formView = new FormViewModel
            {
                Title = form1.Title,
                FormID = form1.FormID,
                Slug = form1.Slug,
                IsPublic = form1.IsPublic,
                PublicFormType = form1.PublicFormType,
                DateAdded = form1.DateAdded.Value
            };

            return formView;
        }

        public static FormViewModel CreateMock()
        {
            var formView = new FormViewModel
            {
                Title = "Test Form",
                FormID = 1,
                DateAdded = DateTime.Now,
            };

            return formView;
        }

        #endregion
    }
}