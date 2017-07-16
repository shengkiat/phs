using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PHS.Common;
using PHS.DB;
using static PHS.Common.Constants;

namespace PHS.DB.ViewModels.Form
{
    public class TemplateViewModel
    {

        #region Properties
        private Constants.TemplateMode mode;

        public int? TemplateID { get; set; }
        public int FormID { get; set; }
        public string Title { get; set; }
        public string NotificationEmail { get; set; }
        public DateTime DateAdded { get; set; }
        public List<TemplateFieldViewModel> Fields { get; set; }
        public Constants.TemplateStatus Status { get; set; }
        public int TabOrder { get; set; }
        public string ConfirmationMessage { get; set; }
        public IList<TemplateFieldValueViewModel> Entries { get; set; }
        public IEnumerable<IGrouping<string, TemplateFieldValueViewModel>> GroupedEntries { get; set; }
        public string Theme { get; set; }
        public bool Embed { get; set; }
        public bool IsPublic { get; set; }
        public string PublicFormType { get; set; }
        public string Slug { get; set; }
        public bool IsQuestion { get; set; }
        public int Version { get; set; }
        public Constants.TemplateMode Mode
        {
            get
            {
                return mode;
            }

            set
            {
                mode = value;

                if (mode == TemplateMode.READONLY)
                {
                    foreach (var field in Fields)
                    {
                        field.Mode = TemplateFieldMode.READONLY;
                    }
                }
               
            }
        }
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

        public static TemplateViewModel Initialize()
        {

            var templateView = new TemplateViewModel
            {
                Status = Constants.TemplateStatus.DRAFT,
                TabOrder = 0,
                Theme = "",
                NotificationEmail = "",
                IsPublic = false,
                IsQuestion = false,
                Fields = Enumerable.Empty<TemplateFieldViewModel>().ToList(),
                SortFields = Enumerable.Empty<SortFieldViewModel>().ToList(),
                CriteriaFields = Enumerable.Empty<CriteriaFieldViewModel>().ToList()
            };

            return templateView;
        }

        public static TemplateViewModel CreateFromObject(Template template1)
        {
            return CreateFromObject(template1, Constants.TemplateFieldMode.EDIT);
        }

        public static TemplateViewModel CreateFromObject(Template template1, Constants.TemplateFieldMode mode)
        {
            if (template1 != null)
            {

                var templateView = CreateBasicFromObject(template1);

                if (template1.TemplateFields.Count() > 0)
                {
                    template1.TemplateFields.OrderBy(o => o.Order).Each((field, index) =>
                    {

                        templateView.Fields.Add(TemplateFieldViewModel.CreateFromObject(field, mode));
                    });
                }

                return templateView;
            }
            return TemplateViewModel.Initialize();
        }

        public static TemplateViewModel CreateBasicFromObject(Template template1)
        {

            var templateView = new TemplateViewModel
            {
                
                TemplateID = template1.TemplateID,
                FormID = template1.FormID,
                DateAdded = template1.DateAdded.Value,
                ConfirmationMessage = template1.ConfirmationMessage,
                Fields = Enumerable.Empty<TemplateFieldViewModel>().ToList(),
                Theme = template1.Theme,
                NotificationEmail = template1.NotificationEmail,
                IsQuestion = template1.IsQuestion,
                Version = template1.Version,
                Status = (Constants.TemplateStatus)Enum.Parse(typeof(Constants.TemplateStatus), template1.Status),

                Title = template1.Form.Title,
                IsPublic = template1.Form.IsPublic,
                Slug = template1.Form.Slug,
                PublicFormType = template1.Form.PublicFormType,
                SortFields = Enumerable.Empty<SortFieldViewModel>().ToList(),
                CriteriaFields = Enumerable.Empty<CriteriaFieldViewModel>().ToList()
            };

            return templateView;
        }

        public static TemplateViewModel CreateMock()
        {
            var templateView = new TemplateViewModel
            {
                TemplateID = 1,
                FormID = 1,
                DateAdded = DateTime.Now,
                ConfirmationMessage = "Thank you for filling this form",
                Fields = Enumerable.Empty<TemplateFieldViewModel>().ToList(),
                IsPublic = false,
                IsQuestion = false,
                NotificationEmail = "",
                SortFields = Enumerable.Empty<SortFieldViewModel>().ToList(),
                CriteriaFields = Enumerable.Empty<CriteriaFieldViewModel>().ToList()
            };

            return templateView;
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