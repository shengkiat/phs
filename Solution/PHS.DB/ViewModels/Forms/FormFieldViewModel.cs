using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections;
using PHS.Common;
using PHS.DB;


namespace PHS.DB.ViewModels.Forms
{
    public class FormFieldViewModel
    {
        #region Properties

        public int? Id { get; set; }
        public string Label { get; set; }
        public Constants.FieldType FieldType { get; set; }
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
        public string Validation { get; set; }
        public int Order { get; set; }
        public DateTime DateAdded { get; set; }
        public string Text { get; set; }
        public string HelpText { get; set; }
        public int DomId { get; set; }
        public int? MinimumAge { get; set; }
        public int? MaximumAge { get; set; }
        public Constants.FormFieldMode Mode { get; set; }
        public string Errors { get; set; }
        public string InputValue { get; set; }
        public int MaxFileSize { get; set; }
        public int MinFileSize { get; set; }
        public string ValidFileExtensions { get; set; }
        public string ImageBase64 { get; set; }
        public string MatrixRow { get; set; }
        public string MatrixColumn { get; set; }
        public string EntryId { get; set; }
        #endregion

        #region Public Members

        public static FormFieldViewModel Initialize()
        {
            return new FormFieldViewModel();
        }

        public static FormFieldViewModel CreateFromObject(form_fields field)
        {
            return CreateFromObject(field, Constants.FormFieldMode.EDIT);
        }

        public static FormFieldViewModel CreateFromObject(form_fields field, Constants.FormFieldMode mode)
        {
            if (field != null)
            {
                
                return new FormFieldViewModel
                {
                    DomId = field.DomId.Value,
                    Id = field.ID,
                    Label = field.Label.Equals("") ? "Click to edit" : field.Label,
                    Text = field.Text,
                    FieldType = (Constants.FieldType)Enum.Parse(typeof(Constants.FieldType), field.FieldType),
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
                    MatrixColumn = field.MatrixColumn


                };
            }

            return FormFieldViewModel.Initialize();
        }

        #endregion

        #region Private Members
        #endregion

        
    }
}