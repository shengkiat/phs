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
    using System.ComponentModel;

    public partial class Person
    {
        public int Sid { get; set; }

        [DisplayName("User Id")]
        public string Username { get; set; }
        public string Password { get; set; }
        [DisplayName("Name")]
        public string FullName { get; set; }
        [DisplayName("Contact")]
        public String ContactNumber { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public string PasswordSalt { get; set; }
        public System.DateTime CreateDT { get; set; }
        public Nullable<System.DateTime> UpdateDT { get; set; }
        public Nullable<System.DateTime> DeleteDT { get; set; }
    }
}
