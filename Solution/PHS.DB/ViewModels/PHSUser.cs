using Foolproof;
using PHS.DB.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace PHS.DB
{
    [TrackChangesAttribute]
    [MetadataType(typeof(PHSUserMetadata))]
    public partial class PHSUser
    {
        public class PHSUserMetadata
        {
            [Required(ErrorMessage = "Please enter User Name")]
            [Display(Name = "User Name")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Please enter Full Name")]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "Please enter Role")]
            public string Role { get; set; }

            [Display(Name = "Active")]
            public bool IsActive { get; set; }

            [Display(Name = "Effective Start Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "-")]
            [SkipTrackingAttribute]
            public DateTime? EffectiveStartDate { get; set; }

            [Display(Name = "Effective End Date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "-")]
            [GreaterThanOrEqualTo("EffectiveStartDate")]
            [SkipTrackingAttribute]
            public DateTime? EffectiveEndDate { get; set; }

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
