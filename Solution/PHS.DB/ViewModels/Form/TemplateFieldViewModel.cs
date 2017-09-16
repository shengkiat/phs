using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections;
using PHS.Common;
using PHS.DB;


namespace PHS.DB.ViewModels.Form
{
    public class TemplateFieldViewModel
    {
        #region Properties

        public int? TemplateFieldID { get; set; }
        public string Label { get; set; }
        public Constants.TemplateFieldType FieldType { get; set; }
        public bool IsRequired { get; set; }
        public int MaxCharacters { get; set; }
        public string HoverText { get; set; }
        public string Hint { get; set; }
        public string SubLabel { get; set; }
        public string Size { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public string Options { get; set; }
        public string SelectedOption { get; set; }
        public bool AddOthersOption { get; set; }
        public string OthersOption { get; set; }
        public string OthersPlaceHolder { get; set; }
        public string Validation { get; set; }
        public int Order { get; set; }
        public DateTime DateAdded { get; set; }
        public string Text { get; set; }
        public string HelpText { get; set; }
        public int DomId { get; set; }
        public int? MinimumAge { get; set; }
        public int? MaximumAge { get; set; }
        public Constants.TemplateFieldMode Mode { get; set; }
        public string Errors { get; set; }
        public string InputValue { get; set; }
        public int MaxFileSize { get; set; }
        public int MinFileSize { get; set; }
        public string ValidFileExtensions { get; set; }
        public string ImageBase64 { get; set; }
        public string MatrixRow { get; set; }
        public string MatrixColumn { get; set; }
        public string EntryId { get; set; }
        public string ParticipantNric { get; set; }
        public bool IsValueRequiredForRegistration { get; set; }
        public string PreRegistrationFieldName { get; set; }
        public string RegistrationFieldName { get; set; }
        public string SummaryFieldName { get; set; }
        public string SummaryType { get; set; }

        public int? ConditionTemplateFieldID { get; set; }
        public string ConditionCriteria { get; set; }
        public string ConditionOptions { get; set; }

        public int? StandardReferenceID { get; set; }

        #endregion

        #region Public Members

        public static TemplateFieldViewModel Initialize()
        {
            return new TemplateFieldViewModel();
        }

        public static TemplateFieldViewModel CreateFromObject(TemplateField field)
        {
            return CreateFromObject(field, Constants.TemplateFieldMode.EDIT);
        }

        public static TemplateFieldViewModel CreateFromObject(TemplateField field, Constants.TemplateFieldMode mode)
        {
            if (field != null)
            {
                
                return new TemplateFieldViewModel
                {
                    DomId = field.DomId.Value,
                    TemplateFieldID = field.TemplateFieldID,
                    Label = field.Label.Equals("") ? "Click to edit" : field.Label,
                    Text = field.Text,
                    FieldType = (Constants.TemplateFieldType)Enum.Parse(typeof(Constants.TemplateFieldType), field.FieldType),
                    IsRequired = field.IsRequired.Value,
                    MaxCharacters = field.MaxChars.Value,
                    HoverText = field.HoverText,
                    Hint = field.Hint,
                    SubLabel = field.SubLabel,
                    Size = field.Size,
                    Columns = field.Columns.Value,
                    Rows = field.Rows.Value,
                    Options = field.Options,
                    SelectedOption = field.SelectedOption,
                    AddOthersOption = field.AddOthersOption.Value,
                    OthersOption = field.OthersOption,
                    OthersPlaceHolder = string.IsNullOrEmpty(field.OthersPlaceHolder) ? "Others" : field.OthersPlaceHolder,
                    HelpText = field.HelpText,
                    Validation = field.Validation,
                    Order = field.Order.Value,
                    MinimumAge = field.MinimumAge,
                    MaximumAge = field.MaximumAge,
                    Mode = mode,
                    MaxFileSize = field.MaxFilesizeInKb ?? field.MaxFilesizeInKb.Value,
                    MinFileSize = field.MinFilesizeInKb ?? field.MinFilesizeInKb.Value,
                    ValidFileExtensions=field.ValidFileExtensions,
                    DateAdded = field.DateAdded,
                    ImageBase64 = field.ImageBase64,
                    MatrixRow = field.MatrixRow,
                    MatrixColumn = field.MatrixColumn,
                    PreRegistrationFieldName = field.PreRegistrationFieldName,
                    RegistrationFieldName = field.RegistrationFieldName,
                    SummaryFieldName = field.SummaryFieldName,
                    SummaryType = field.SummaryType,
                    ConditionCriteria = field.ConditionCriteria,
                    ConditionOptions = field.ConditionOptions,
                    ConditionTemplateFieldID = field.ConditionTemplateFieldID,
                    StandardReferenceID = field.StandardReferenceID
                };
            }

            return TemplateFieldViewModel.Initialize();
        }

        #endregion

        #region Private Members
        #endregion

        
    }
}