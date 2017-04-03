using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PHS.DB;
using PHS.Common;

namespace PHS.DB.ViewModels.Forms
{
    public class FormFieldValueViewModel
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

        public FormFieldValueViewModel()
        {

        }

        public FormFieldValueViewModel(Constants.FieldType type, string value)
        {
            this.Value = value;
            this.FieldType = type;
        }

        public static FormFieldValueViewModel Initialize()
        {
            return new FormFieldValueViewModel();
        }

        public static FormFieldValueViewModel CreateFromObject(form_field_values model)
        {
           // model.FormFieldReference.Load();
            return new FormFieldValueViewModel
            {
                Id = model.ID,
                FieldId = model.FieldId,
                EntryId = model.EntryId.ToString(),
                Value = model.Value,
                DateAdded = model.DateAdded,                
                FieldType = (Constants.FieldType)Enum.Parse(typeof(Constants.FieldType), model.form_fields.FieldType),
                FieldLabel = model.form_fields.Label,
                FieldOrder = model.form_fields.Order.Value
            };
        }
    }
}