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
    
    public partial class ReferenceRange
    {
        public int ReferenceRangeID { get; set; }
        public string Title { get; set; }
        public double MinimumValue { get; set; }
        public double MaximumValue { get; set; }
        public string Result { get; set; }
        public int StandardReferenceID { get; set; }
    
        public virtual StandardReference StandardReference { get; set; }
    }
}
