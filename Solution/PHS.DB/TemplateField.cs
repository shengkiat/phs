//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PHS.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class TemplateField
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TemplateField()
        {
            this.TemplateFieldValues = new HashSet<TemplateFieldValue>();
            this.Templates = new HashSet<Template>();
        }
    
        public int TemplateFieldID { get; set; }
        public string Label { get; set; }
        public string Text { get; set; }
        public string FieldType { get; set; }
        public Nullable<bool> IsRequired { get; set; }
        public Nullable<int> MaxChars { get; set; }
        public string HoverText { get; set; }
        public string Hint { get; set; }
        public string SubLabel { get; set; }
        public string Size { get; set; }
        public string SelectedOption { get; set; }
        public Nullable<bool> AddOthersOption { get; set; }
        public string OthersOption { get; set; }
        public Nullable<int> Columns { get; set; }
        public Nullable<int> Rows { get; set; }
        public string Options { get; set; }
        public string Validation { get; set; }
        public Nullable<int> DomId { get; set; }
        public Nullable<int> Order { get; set; }
        public Nullable<int> MinimumAge { get; set; }
        public Nullable<int> MaximumAge { get; set; }
        public string HelpText { get; set; }
        public System.DateTime DateAdded { get; set; }
        public Nullable<int> MaxFilesizeInKb { get; set; }
        public string ValidFileExtensions { get; set; }
        public Nullable<int> MinFilesizeInKb { get; set; }
        public string ImageBase64 { get; set; }
        public string MatrixRow { get; set; }
        public string MatrixColumn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TemplateFieldValue> TemplateFieldValues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Template> Templates { get; set; }
    }
}