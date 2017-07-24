using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PHS.Common;
using PHS.DB;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ExpressiveAnnotations.Attributes;

namespace PHS.DB.ViewModels.Form
{
    public class FormViewModel
    {
        #region Properties

        [ScaffoldColumn(false)]
        public int? FormID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        [RequiredIf("IsPublic == true", ErrorMessage = "Slug is required.")]
        public string Slug { get; set; }

        [DisplayName("Is Public")]
        [Required(ErrorMessage = "Is Public is required")]
        public bool IsPublic { get; set; }

        [DisplayName("Public Form Type")]
        [RequiredIf("IsPublic == true", ErrorMessage = "Public Form Type is required.")]
        public string PublicFormType { get; set; }

        [DisplayName("Internal Form Type")]
        [RequiredIf("IsPublic == true", ErrorMessage = "Internal Form Type is required.")]
        public string InternalFormType { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DateAdded { get; set; }

        #endregion

        #region Public Members

        public static FormViewModel Initialize()
        {

            var formView = new FormViewModel();

            return formView;
        }

        public static FormViewModel CreateFromObject(DB.Form form1)
        {
            return CreateFromObject(form1, Constants.TemplateMode.EDIT);
        }

        public static FormViewModel CreateFromObject(DB.Form form1, Constants.TemplateMode mode)
        {
            if (form1 != null)
            {

                var formView = CreateBasicFromObject(form1);
                return formView;
            }
            return FormViewModel.Initialize();
        }

        public static FormViewModel CreateBasicFromObject(DB.Form form1)
        {

            var formView = new FormViewModel
            {
                Title = form1.Title,
                FormID = form1.FormID,
                Slug = form1.Slug,
                IsPublic = form1.IsPublic,
                PublicFormType = form1.PublicFormType,
                InternalFormType = form1.InternalFormType,
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