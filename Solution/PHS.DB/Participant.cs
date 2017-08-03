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
    
    public partial class Participant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Participant()
        {
            this.ParticipantJourneyModalities = new HashSet<ParticipantJourneyModality>();
            this.PHSEvents = new HashSet<PHSEvent>();
        }
    
        public int ParticipantID { get; set; }
        public string Nric { get; set; }
        public string FullName { get; set; }
        public string Salutation { get; set; }
        public string HomeNumber { get; set; }
        public string MobileNumber { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Language { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Race { get; set; }
        public string Citizenship { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParticipantJourneyModality> ParticipantJourneyModalities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHSEvent> PHSEvents { get; set; }
    }
}
