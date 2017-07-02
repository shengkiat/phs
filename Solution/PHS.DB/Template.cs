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
    
    public partial class Template
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Template()
        {
            this.TemplateFields = new HashSet<TemplateField>();
        }
    
        public int TemplateID { get; set; }
        public int FormID { get; set; }
        public string Status { get; set; }
        public string ConfirmationMessage { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public string Theme { get; set; }
        public string NotificationEmail { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> EventID { get; set; }
        public bool IsPublic { get; set; }
        public string PublicFormType { get; set; }
        public bool IsQuestion { get; set; }
        public int Version { get; set; }
    
        public virtual Form Form { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TemplateField> TemplateFields { get; set; }
    }
}
