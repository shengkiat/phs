using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PHS.Common;
using PHS.DB;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ExpressiveAnnotations.Attributes;
using PHS.DB.Attributes;

namespace PHS.DB
{
    [TrackChangesAttribute]
    [MetadataType(typeof(FormMetadata))]
    public partial class Form
    {
        public class FormMetadata
        {

            public int? FormID { get; set; }

            [Required(ErrorMessage = "Title is required")]
            [StringLength(50)]
            public string Title { get; set; }

            [StringLength(50)]
            [RequiredIf("IsPublic == true", ErrorMessage = "Slug is required.")]
            public string Slug { get; set; }

            [DisplayName("Is Public")]
            [Required(ErrorMessage = "Is Public is required")]
            public bool IsPublic { get; set; }

            [DisplayName("Public Form Type")]
            [RequiredIf("IsPublic == true", ErrorMessage = "Public Form Type is required.")]
            public string PublicFormType { get; set; }

            [DisplayName("Internal Form Type")]
            public string InternalFormType { get; set; }

            [ScaffoldColumn(false)]
            public DateTime DateAdded { get; set; }

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