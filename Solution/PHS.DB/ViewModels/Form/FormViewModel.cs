using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PHS.Common;
using PHS.DB;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ExpressiveAnnotations.Attributes;
using PHS.DB.Attributes;

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
        public string InternalFormType { get; set; }

        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [SkipTrackingAttribute]
        public DateTime CreatedDateTime { get; set; }

        public string CreatedBy { get; set; }

        [Display(Name = "Updated Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "-")]
        [SkipTrackingAttribute]
        public DateTime? UpdatedDateTime { get; set; }

        public string UpdatedBy { get; set; }

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
                DateAdded = form1.DateAdded.Value,
                CreatedBy = form1.CreatedBy,
                UpdatedBy = form1.UpdatedBy,
            };

            if (form1.CreatedDateTime.HasValue)
            {
                formView.CreatedDateTime = form1.CreatedDateTime.Value;
            }

            if (form1.UpdatedDateTime.HasValue)
            {
                formView.UpdatedDateTime = form1.UpdatedDateTime.Value;
            }

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