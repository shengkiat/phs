using PHS.DB.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace PHS.DB
{
    [TrackChangesAttribute]
    [MetadataType(typeof(PersonMetadata))]
    public partial class Person
    {
        public class PersonMetadata
        {
            [Required(ErrorMessage = "Please enter User Name")]
            [Display(Name = "User Name")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Please enter Full Name")]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Display(Name = "Is Active")]
            public bool IsActive { get; set; }

            [Required(ErrorMessage = "Please enter Password")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "Contact Number")]
            public string ContactNumber { get; set; }

            [Display(Name = "Created Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            [SkipTrackingAttribute]
            public DateTime CreatedDateTime { get; set; }

            public int? CreatedBy { get; set; }

            [Display(Name = "Updated Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", NullDisplayText = "-")]
            [SkipTrackingAttribute]
            public DateTime? UpdatedDateTime { get; set; }

            public int? UpdatedBy { get; set; }
        }
    }
}
