using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PHS.DB;
using PHS.Common;

namespace PHS.DB.ViewModels.Forms
{
    public class TemplateFieldValueViewModel
    {
        public int Id { get; set; }
        public int FieldId { get; set; }
        public string EntryId { get; set; }
        public string Value { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid? UserId { get; set; }
        public Constants.FieldType FieldType { get; set; }
        public string FieldLabel { get; set; }
        public int FieldOrder { get; set; }

        public TemplateFieldValueViewModel()
        {

        }

        public TemplateFieldValueViewModel(Constants.FieldType type, string value)
        {
            this.Value = value;
            this.FieldType = type;
        }

        public static TemplateFieldValueViewModel Initialize()
        {
            return new TemplateFieldValueViewModel();
        }

        public static TemplateFieldValueViewModel CreateFromObject(FormFieldValue model)
        {
           // model.FormFieldReference.Load();
            return new TemplateFieldValueViewModel
            {
                Id = model.ID,
                FieldId = model.FieldId,
                EntryId = model.EntryId.ToString(),
                Value = model.Value,
                DateAdded = model.DateAdded,                
                FieldType = (Constants.FieldType)Enum.Parse(typeof(Constants.FieldType), model.FormField.FieldType),
                FieldLabel = model.FormField.Label,
                FieldOrder = model.FormField.Order.Value
            };
        }
    }
}