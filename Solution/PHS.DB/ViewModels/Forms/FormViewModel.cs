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

        public int? Id { get; set; }
        public string Title { get; set; }
        public string NotificationEmail { get; set; }
        public string Slug { get; set; }
        public DateTime DateAdded { get; set; }
        public List<FormFieldViewModel> Fields { get; set; }
        public Constants.FormStatus Status { get; set; }
        public int TabOrder { get; set; }
        public string ConfirmationMessage { get; set; }
        public IList<FormFieldValueViewModel> Entries { get; set; }
        public IEnumerable<IGrouping<string, FormFieldValueViewModel>> GroupedEntries { get; set; }
        public string Theme { get; set; }
        public bool Embed { get; set; }
        public bool IsPublic { get; set; }
        public string PublicFormType { get; set; }
        public bool IsQuestion { get; set; }
        public List<SortFieldViewModel> SortFields { get; set; }
        public List<CriteriaFieldViewModel> CriteriaFields { get; set; }

        public bool HasTheme
        {
            get
            {
                return false;
                //return !Theme.IsNullOrEmpty(); 
            }
        }


        #endregion

        #region Public Members

        public static FormViewModel Initialize()
        {

            var formView = new FormViewModel
            {
                Title = "Registration",
                Status = Constants.FormStatus.DRAFT,
                TabOrder = 0,
                Theme = "",
                NotificationEmail = "",
                IsPublic = false,
                IsQuestion = false,
                Fields = Enumerable.Empty<FormFieldViewModel>().ToList(),
                SortFields = Enumerable.Empty<SortFieldViewModel>().ToList(),
                CriteriaFields = Enumerable.Empty<CriteriaFieldViewModel>().ToList()
            };

            return formView;
        }

        public static FormViewModel CreateFromObject(Form form1)
        {
            return CreateFromObject(form1, Constants.FormFieldMode.EDIT);
        }

        public static FormViewModel CreateFromObject(Form form1, Constants.FormFieldMode mode)
        {
            if (form1 != null)
            {

                var formView = CreateBasicFromObject(form1);

                if (form1.FormFields.Count() > 0)
                {
                    form1.FormFields.OrderBy(o => o.Order).Each((field, index) =>
                    {

                        formView.Fields.Add(FormFieldViewModel.CreateFromObject(field, mode));
                    });
                }

                return formView;
            }
            return FormViewModel.Initialize();
        }

        public static FormViewModel CreateBasicFromObject(Form form1)
        {

            var formView = new FormViewModel
            {
                Title = form1.Title,
                Id = form1.ID,
                DateAdded = form1.DateAdded.Value,
                ConfirmationMessage = form1.ConfirmationMessage,
                Fields = Enumerable.Empty<FormFieldViewModel>().ToList(),
                Slug = form1.Slug,
                Theme = form1.Theme,
                NotificationEmail = form1.NotificationEmail,
                IsPublic = form1.IsPublic,
                IsQuestion = form1.IsQuestion,
                PublicFormType = form1.PublicFormType,
                Status = (Constants.FormStatus)Enum.Parse(typeof(Constants.FormStatus), form1.Status),
                SortFields = Enumerable.Empty<SortFieldViewModel>().ToList(),
                CriteriaFields = Enumerable.Empty<CriteriaFieldViewModel>().ToList()
            };

            return formView;
        }

        public static FormViewModel CreateMock()
        {
            var formView = new FormViewModel
            {
                Title = "Test Form",
                Id = 1,
                DateAdded = DateTime.Now,
                ConfirmationMessage = "Thank you for filling this form",
                Fields = Enumerable.Empty<FormFieldViewModel>().ToList(),
                Slug = "test-form",
                IsPublic = false,
                IsQuestion = false,
                NotificationEmail = "",
                SortFields = Enumerable.Empty<SortFieldViewModel>().ToList(),
                CriteriaFields = Enumerable.Empty<CriteriaFieldViewModel>().ToList()
            };

            return formView;
        }

        #endregion
    }

    static class UtilityExtensions
    {
        public static void Each<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;

            foreach (var e in ie)
            {
                action(e, i++);
            }
        }
    }
}