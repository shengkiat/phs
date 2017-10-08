using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PHS.DB;
using PHS.Common;

namespace PHS.DB.ViewModels.Form
{
    public class TemplateFieldValueViewModel
    {
        public int TemplateFieldValueID { get; set; }
        public int TemplateFieldID { get; set; }
        public string EntryId { get; set; }
        public string Value { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid? UserId { get; set; }
        public Constants.TemplateFieldType FieldType { get; set; }
        public string FieldLabel { get; set; }
        public int FieldOrder { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public string UpdatedBy { get; set; }

        public TemplateFieldValueViewModel()
        {

        }

        public TemplateFieldValueViewModel(Constants.TemplateFieldType type, string value)
        {
            this.Value = value;
            this.FieldType = type;
        }

        public static TemplateFieldValueViewModel Initialize()
        {
            return new TemplateFieldValueViewModel();
        }

        public static TemplateFieldValueViewModel CreateFromObject(TemplateFieldValue model)
        {
           // model.FormFieldReference.Load();
            return new TemplateFieldValueViewModel
            {
                TemplateFieldValueID = model.TemplateFieldValueID,
                TemplateFieldID = model.TemplateFieldID,
                EntryId = model.EntryId.ToString(),
                Value = model.Value,
                DateAdded = model.DateAdded,                
                FieldType = (Constants.TemplateFieldType)Enum.Parse(typeof(Constants.TemplateFieldType), model.TemplateField.FieldType),
                FieldLabel = model.TemplateField.Label,
                FieldOrder = model.TemplateField.Order.Value
            };
        }
    }
}